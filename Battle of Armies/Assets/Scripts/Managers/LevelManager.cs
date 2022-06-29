using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private int currentLevel;
    public static float levelUpMultiplyIndex = 1.3f;
    private int maxLevel = 3;

    private void Start()
    {
        currentLevel = 1;
    }
    public void UpArmyLevel()
    {
        if (currentLevel < maxLevel && moneyManager.HaveUpgradeMoney())
        {
            moneyManager.DeductMoney(moneyManager.UpgradeLvlPrice);

            moneyManager.IncreaseLevelUpPrice(); // Should be after DeductMoney method
            
            Tank.durability *= levelUpMultiplyIndex;
            Tank.shootCD /= levelUpMultiplyIndex;

            Airplane.durability *= levelUpMultiplyIndex;
            Airplane.speed *= levelUpMultiplyIndex;

            ArmoredLorry.speed *= levelUpMultiplyIndex;
            ArmoredLorry.durability *= levelUpMultiplyIndex;

            DefenceTower.health *= levelUpMultiplyIndex;
            MainTower.health *= levelUpMultiplyIndex;

            Debug.Log("Level has been increased!!!");
            currentLevel++;
        }
        else
        {
            Debug.Log("You already have maximum level!");
        }
        
    }

    
}
