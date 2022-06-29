using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyText;
    [SerializeField] private TextMeshProUGUI IncomeText;
    [SerializeField] private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("Money Manager").GetComponent<MoneyManager>();
    }
    private void Update()
    {
        MoneyText.text = "$ " + moneyManager.Money.ToString("0");
        IncomeText.text = moneyManager.IncomeIndex.ToString("0.0") + "/sec";

    }
}
