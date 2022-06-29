using UnityEngine;
using TMPro;

public class UpgradeTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private MoneyManager moneyManager;

    private void OnEnable()
    {
        priceText.text = moneyManager.UpgradeLvlPrice.ToString("0");
    }
}
