using TMPro;
using UnityEngine;

public class BallanceUIElementController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Bank.MoneyChanged.AddListener(UpdateBalance);
       UpdateBalance(Bank.Instance.Balance);
    }

    void UpdateBalance(int newBalance){
        GetComponent<TextMeshProUGUI>().text = newBalance.ToString();
    }
}
