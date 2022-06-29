using System.Collections;
using UnityEngine;
using Photon.Pun;

public class ArmoredLorry : Vehicles
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private bool isPlayingSound = false;
    [SerializeField] private string TAG_MoneyManager;
    private MoneyManager moneyManager;

    public override float Durability { get => durability; set => durability = value; }
    public static float durability = 2000f;
    private float currentDurability;

    protected override float Speed { get => speed; set => speed = value; }
    public static float speed = 0.01f;

    private string TAG_TowerProjectile = "Tower Projectile";
    private string TAG_Building = "Building";



    private void Start()
    {
        MakeCreateVehicleEvent();
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            gameObject.SetActive(false);
        }

        ColorChanger.ChangeColor(gameObject, ColorChanger.GenerateRedGreenOrBlueColor());

        HealthBar.SetMaxHealth(durability);
        currentDurability = durability;

        speed = Speed;
        currentSpeed = speed;

        attackRadar = transform.GetComponentInChildren<AttackRadar>();
        attackRadar.targetFounded += SetTarget;
        attackRadar.lostTarget += RemoveTarget;

        moneyManager = GameObject.FindGameObjectWithTag(TAG_MoneyManager).GetComponent<MoneyManager>();
    }

    private void FixedUpdate()
    {
        if (attackTarget != null)
        {
            RotateToTarget(attackTarget);
        }
        Drive();
    }

    public override void DestroyYourself()
    {
        MakeCreateVehicleEvent();
        CreateExplosion();
        FindObjectOfType<AudioManager>().PlaySound("Boom");
        print("Big boom!");
        moneyManager.AddMoney(moneyManager.armoredLorryPrice); // When vehicle has destroyed, add money to money manager
        Destroy(gameObject);
    }

    public override void DoAttack()
    {
        
        // Blow up
        DestroyYourself();
    }

    public override void TakeDamage(float damage)
    {
        if (!isPlayingSound)
        {
            FindObjectOfType<AudioManager>().PlaySound("LorryTakeDamage");
            StartCoroutine(DmgSoundCD(0.12f));
        }
        if (damage > currentDurability)
        {
            DestroyYourself();
            return;
        }
        currentDurability -= damage;
        HealthBar.SetHealth(currentDurability);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TAG_Building))
        {
            Debug.Log("Collide with building");
            collider.GetComponent<Building>().TakeDamage(200f);
            DoAttack();
        }

        if (collider.CompareTag(TAG_TowerProjectile))
        {
            TakeDamage(350f);
            Destroy(collider);
        }
    }

    private void OnMouseDrag()
    {
        TakeDamage(700f * Time.deltaTime);
    }

    IEnumerator DmgSoundCD(float time)
    {
        isPlayingSound = true;
        
        yield return new WaitForSeconds(time);
        isPlayingSound = false;
    }
}
