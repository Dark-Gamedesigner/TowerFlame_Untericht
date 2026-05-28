using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Bank : MonoBehaviour
{
    public static UnityEvent<int> AddMoney = new();
    public static UnityEvent<int> SpendMoney = new();
    public static UnityEvent<int> MoneyChanged = new();

    public static Bank Instance{ private set; get; } = null;

    public int Balance{ get; private set;} = 100;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null){
            Instance = this;
        }
        else{
            Debug.Log("A second Bank has been instantiated");
            Destroy(this);
            return;
        }
        AddMoney.AddListener(OnAddMoney);
        SpendMoney.AddListener(OnSpendMoney);
    }

    private void OnAddMoney(int amount){
        Balance += amount;
        MoneyChanged.Invoke(Balance);
    }

    private void OnSpendMoney(int amount){
        Balance -= amount;
        MoneyChanged.Invoke(Balance);
    }

    private void Update(){
        if (Keyboard.current.ctrlKey.isPressed){
            if (Keyboard.current.rKey.wasPressedThisFrame){
                AddMoney.Invoke(100);
            }

            if (Keyboard.current.shiftKey.wasPressedThisFrame){
                SpendMoney.Invoke(100);
            }
        }
    }
}
