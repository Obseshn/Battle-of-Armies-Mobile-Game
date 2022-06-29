using System.Collections;
using UnityEngine;
using Photon.Pun;

public abstract class Vehicles : MonoBehaviour, ITakeDamager, ISelfDestroyer, IAttacker, IPunPrefabPool
{
    [SerializeField] protected abstract float Speed { get; set; }
    [SerializeField] protected AttackRadar attackRadar;
    [SerializeField] protected Transform attackTarget;
    [SerializeField] private float rotateSpeed;
    [SerializeField] protected HealthBar HealthBar;
    [SerializeField] protected bool hasRotated = false;

    public ParticleSystem explosionEffect;
    public abstract float Durability { get; set; }
    public abstract void TakeDamage(float damage);
    public abstract void DestroyYourself();

    protected PhotonView photonView;

    public static event VehicleCreated newVehicleHasCreated;
    public delegate void VehicleCreated();

    public static event VehicleDestroyed vehicleHasDestroyed;
    public delegate void VehicleDestroyed();

    protected void MakeCreateVehicleEvent()
    {
        newVehicleHasCreated?.Invoke();
    }

    protected void MakeDestroyVehicleEvent()
    {
        vehicleHasDestroyed?.Invoke();
    }
    protected Transform AttackTarget
    {
        set => attackTarget = value;
    }

    protected void RotateToTarget(Transform destination)
    {
        Vector3 dir = (destination.position - transform.position);

        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);

        rot.x = rot.z = 0;

        transform.rotation = rot;
    }

    protected void RotateToForwardDir()
    {
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.identity, rotateSpeed * Time.deltaTime);

        transform.rotation = rot;
    }

    protected void SetTarget(Transform target)
    {
        Debug.Log("Target: " + target.name);
        attackTarget = target;
        StartCoroutine(RotateTimer(8));
    }

    protected void RemoveTarget()
    {
        Debug.Log(gameObject.name + "lost target!");
        attackTarget = null;
    }

    protected void Drive()
    {
        transform.Translate(Vector3.forward * Speed);
    }

    public abstract void DoAttack();
   
    protected IEnumerator RotateTimer(float time = 8)
    {
        hasRotated = false;
        yield return new WaitForSeconds(time);
        hasRotated = true;
    }

    protected IEnumerator LevelChangeCD(bool isLevelChanged)
    {
        isLevelChanged = true;
        yield return new WaitForSeconds(0.5f);
        isLevelChanged = false;
    }

    protected void CreateExplosion()
    {
        Instantiate(explosionEffect, gameObject.transform.position, transform.rotation);
        Debug.Log("Explosion effect had played");
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public void Destroy(GameObject gameObject)
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
