using UnityEngine;

public class DefenceTower : Building, IAttacker
{
    [SerializeField] protected AttackRadar attackRadar;
    [SerializeField] protected Transform attackTarget;
    [SerializeField] private float currentHealth;
    [SerializeField] private Rigidbody Projectile;
    [SerializeField] private Transform FireStartPos;

    public static float health = 1000;
    public override float Health
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    private float projSpeed = 2f;
    private float reloadTime = 2f;
    private float reloadCounter;

    // ATTACK ARMY PROJECTILES TAGS
    private string TAG_TankProjectile = "Tank Projectile";
    private string TAG_AirplaneBomb = "Bomb";
    

    private void Start()
    {
        HealthBar.SetMaxHealth(health);
        reloadCounter = reloadTime;
        currentHealth = health;


        attackRadar = transform.GetComponentInChildren<AttackRadar>();
        attackRadar.targetFounded += SetTarget;
        attackRadar.lostTarget += RemoveTarget;
    }

    private void Update()
    {
        if (reloadCounter > 0)
        {
            reloadCounter -= Time.deltaTime;
        }

        if (attackTarget != null && reloadCounter <= 0)
        {
            DoAttack();
        }
    }

    public void DoAttack()
    {
        Rigidbody rbProj = Instantiate(Projectile, FireStartPos.position, transform.rotation);

        rbProj.velocity = projSpeed * (attackTarget.position - FireStartPos.position);

        reloadCounter = reloadTime;
    }

    private void SetTarget(Transform target)
    {
        Debug.Log("Target: " + target.name);
        attackTarget = target;
        hasTarget = true;
    }

    private void RemoveTarget()
    {
        Debug.Log(gameObject.name + "lost target!");
        attackTarget = null;
        hasTarget = false;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_AirplaneBomb))
        {
            TakeDamage(Airplane.projectileDmg);
            Destroy(other.gameObject);
            Debug.Log("Airplane bomb has came");
        }
        if (other.CompareTag(TAG_TankProjectile))
        {
            TakeDamage(100f);
            Destroy(other.gameObject);
            Debug.Log("Tank proj has came");
        }
    }

}
