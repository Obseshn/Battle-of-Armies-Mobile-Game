using UnityEngine;
using Photon.Pun;

public class Airplane : Vehicles
{
    [SerializeField] private BombDropChecker bombDropChecker;
    [SerializeField] private Rigidbody Projectile;

    public override float Durability { get => durability; set => durability = value; }
    public static float durability = 300;
    private float currentDurability;

    protected override float Speed { get => speed; set => speed = value; }
    public static float speed = 0.06f;
    private float currentSpeed;
    
    public readonly static float projectileDmg = 500f;

    private bool hasDropedBomb = false;

    private string TAG_TowerProjectile = "Tower Projectile";

    private MoneyManager moneyManager;
    [SerializeField] private string TAG_MoneyManager;


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
        currentSpeed = speed;

        attackRadar.targetFounded += SetTarget;
        attackRadar.lostTarget += RemoveTarget;
        bombDropChecker.getCloseToBuild += DoAttack;

        moneyManager = GameObject.FindGameObjectWithTag(TAG_MoneyManager).GetComponent<MoneyManager>();
    }
    
    public override void DestroyYourself()
    {
        MakeCreateVehicleEvent();
        CreateExplosion(); 
        Destroy(gameObject);
        Debug.Log("Airplane was blowed up!");
        moneyManager.AddMoney(moneyManager.airplanePrice); // When vehicle has destroyed, add money to money manager
    } 

    public override void DoAttack()
    {
        Instantiate(Projectile, transform.position, transform.rotation);
        hasDropedBomb = true;
        Debug.Log("AirPlane droped bombs");
    }

    public override void TakeDamage(float damage)
    {
        FindObjectOfType<AudioManager>().PlaySound("Hit2");
        if (damage >= currentDurability)
        {
            DestroyYourself();
            return;
        }
        currentDurability -= damage;
        HealthBar.SetHealth(currentDurability);
    }

    private void FixedUpdate()
    {
        if (attackTarget != null && !hasDropedBomb)
        {
            RotateToTarget(attackTarget);
        }
        Drive();
    }

    private void OnMouseEnter()
    {
        TakeDamage(30f);
        Debug.Log("Player hits airplane!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_TowerProjectile))
        {
            TakeDamage(170f);
            Destroy(other);
        }
    }

}
