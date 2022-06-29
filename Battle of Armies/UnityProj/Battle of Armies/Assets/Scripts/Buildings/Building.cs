using UnityEngine;
using System.Collections;
public abstract class Building : MonoBehaviour, ITakeDamager, ISelfDestroyer
{
    [SerializeField] protected HealthBar HealthBar;
    [SerializeField] private ParticleSystem UndestroyableEffect;
    public abstract float Health { get; set; }

    protected bool hasTarget = false;

    private bool isUndesroyable = false;
    [SerializeField] private Vector3 effectOFFset = new Vector3(0, 2f, 0);

    public void MakeUnDestroyable(int boostTime = 5)
    {
        Debug.Log("Building are undestroyable! For " + boostTime + " sec");
        CreateUndestroyableEffect();
        StartCoroutine(UndestroyableCD(boostTime));
    }
        
    public void TakeDamage(float damage) 
    {
        if (!isUndesroyable)
        {
            if (damage >= Health)
            {
                DestroyYourself();
                return;
            }
            Health -= damage;
            HealthBar.SetHealth(Health);
            Debug.Log("Dmg has taken!!!");
        }
        
    }
    public virtual void DestroyYourself() 
    {
        Destroy(gameObject);
        Debug.Log(gameObject.name.ToString() + "has been destroyed!");
    }
  
    IEnumerator UndestroyableCD(float time)
    {
        
        isUndesroyable = true;
        yield return new WaitForSeconds(time);
        isUndesroyable = false;
    }

    public void CreateUndestroyableEffect()
    {
        Instantiate(UndestroyableEffect, transform.position + effectOFFset, transform.rotation);
    }
}

