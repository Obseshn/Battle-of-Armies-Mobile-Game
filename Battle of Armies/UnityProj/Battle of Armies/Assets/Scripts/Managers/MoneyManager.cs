using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public float IncomeIndex = 1f;
    public float UpgradeLvlPrice = 120f;

    public readonly float airplanePrice = 20f;
    public readonly float armoredLorryPrice = 15f;
    public readonly float tankPrice = 10f;
    private float UpgradeMultiplyIndex = 1.3f;

    public float Money { get => money; }
    private float money;

    private void Start()
    {
        money = 50f;
    }

    private void Update()
    {
        money += IncomeIndex * Time.deltaTime;
    }

    public void AddMoney(float moneyToAdd)
    {
        money += moneyToAdd;
    }

    public bool HaveEnoughMoney(float moneyToComare)
    {
        if (moneyToComare > money)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeductMoney(float moneyToDeduct)
    {
        if (HaveEnoughMoney(moneyToDeduct))
        {
            money -= moneyToDeduct;
            IncomeIndex += moneyToDeduct / 500f;
        }
        
    }

    public bool HaveMoneyForVehicles(int index)
    {
        if (money < tankPrice)
        {
            
            return false;
        }
        if (index == (int)VehiclesIndex.Airplane && money >= airplanePrice)
        {
            DeductMoney(airplanePrice);
            return true;
        }
        if (index == (int)VehiclesIndex.ArmoredLorry && money >= armoredLorryPrice)
        {
            DeductMoney(armoredLorryPrice);
            return true;
        }
        if (index == (int)VehiclesIndex.Tank && money >= tankPrice)
        {
            DeductMoney(tankPrice);
            return true;
        }
        return false;
    }

    public void IncreaseLevelUpPrice()
    {
        UpgradeLvlPrice *= UpgradeMultiplyIndex;
    }

    public bool HaveUpgradeMoney()
    {
        if (money >= UpgradeLvlPrice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
