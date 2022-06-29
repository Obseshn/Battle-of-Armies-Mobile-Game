using System.Collections;
using UnityEngine;
using Photon.Pun;

public class MainTower : Building
{
    [SerializeField] private GameObject UpgradeMenu;
    [SerializeField] private Connection PhotonManager;
    public override float Health
    {
        get => health;
        set => health = value;
    }
    public static float health = 3000f;
    [SerializeField] private GameObject tower1;
    [SerializeField] private GameObject tower2;

    // ATTACK ARMY PROJECTILES TAGS
    private string TAG_TankProjectile = "Tank Projectile";
    private string TAG_AirplaneBomb = "Bomb";
    

    private void Start()
    {
        Color color = ColorChanger.GenerateRedGreenOrBlueColor();
        ColorChanger.ChangeColor(gameObject, color);
        ColorChanger.ChangeColor(tower1, color);
        ColorChanger.ChangeColor(tower2, color);

        HealthBar.SetMaxHealth(health);
    }

    public override void DestroyYourself()
    {
        PhotonManager.LeaveRoomButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TAG_TankProjectile))
        {
            TakeDamage(100f);
            Destroy(other.gameObject);
            Debug.Log("Tank proj has came");
        }

        if (other.CompareTag(TAG_AirplaneBomb))
        {
            TakeDamage(300f);
            Destroy(other.gameObject);
            Debug.Log("Airplane bomb has came");
        }
    }

    private void OnMouseDown()
    {
        if (!UpgradeMenu.activeSelf)
        {
            StartCoroutine(ShowUpgradeMenu(3f));
        }
        
    }

    IEnumerator ShowUpgradeMenu(float timeToShow)
    {
        UpgradeMenu.SetActive(true);
        yield return new WaitForSeconds(timeToShow);
        UpgradeMenu.SetActive(false);
    }

}
