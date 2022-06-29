using UnityEngine;
using Photon.Pun;

public class Tank : Vehicles
{
    [SerializeField] private Rigidbody Projectile;
    [SerializeField] private Transform FireStartPos;
    [SerializeField] private float shootCounter;
    [SerializeField] private string TAG_MoneyManager;
    
    public static float shootCD = 2f;
    private float projSpeed = 10f;

    protected override float Speed { get => speed; set => speed = value; }
    private static float speed = 0.02f;
    private float currentSpeed;

    public override float Durability
    {
        get => durability;
        set => durability = value;
    }
    public static float durability = 1000;
    private float currentDurability;

    private MoneyManager moneyManager;

    
    private void Start()
    {
        MakeCreateVehicleEvent();
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            gameObject.SetActive(false);
        }

        

        ColorChanger.ChangeColor(gameObject, ColorChanger.GenerateRedGreenOrBlueColor());

        currentDurability = durability;
        HealthBar.SetMaxHealth(durability);

        currentSpeed = speed;

        shootCounter = shootCD;

        attackRadar = transform.GetComponentInChildren<AttackRadar>();
        attackRadar.targetFounded += SetTarget;
        attackRadar.lostTarget += RemoveTarget;

        moneyManager = GameObject.FindGameObjectWithTag(TAG_MoneyManager).GetComponent<MoneyManager>();
    }

    private void Update()
    {
        if (shootCounter >= 0)
        {
            shootCounter -= Time.deltaTime;
        }
        if (attackTarget != null && shootCounter <= 0)
        {
            DoAttack();
        }
    }

    private void FixedUpdate()
    {
        if (attackTarget != null)
        {
            RotateToTarget(attackTarget);
            return;
        }
        else
        {
            RotateToForwardDir();
        }
        Drive();

    }

    public override void DestroyYourself()
    {
        MakeCreateVehicleEvent();

        CreateExplosion();

        moneyManager.AddMoney(moneyManager.tankPrice); // When vehicle has destroyed, add money to money manager
        

        PhotonNetwork.Destroy(photonView);
    }

    public override void TakeDamage(float damage)
    {
        FindObjectOfType<AudioManager>().PlaySound("Hit1");
        
        if (damage > currentDurability)
        {
            
            DestroyYourself();
            return;
        }
        
        currentDurability -= damage;
        HealthBar.SetHealth(currentDurability);
    }

    public override void DoAttack()
    {
        Rigidbody rbProj = Instantiate(Projectile, FireStartPos.position, transform.rotation);

        FindObjectOfType<AudioManager>().PlaySound("TankShoot");
        rbProj.velocity = projSpeed * FireStartPos.forward;

        shootCounter = shootCD;
    }

    private void OnMouseDown()
    {
        TakeDamage(200f);
        Debug.Log("Player hits enemy!");
    }
}
