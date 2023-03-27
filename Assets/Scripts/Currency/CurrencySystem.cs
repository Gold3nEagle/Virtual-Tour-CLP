using UnityEngine;
using UnityEngine.Events;
using System;

public class CurrencySystem : MonoBehaviour, ISaveable
{

    public static CurrencySystem instance;//currency instance


    public int currentMoney = 0; //current money value accessed inside the class

    public UnityEvent onMoneyChanged;// unity event to invoke when the money is changed

    public int CurrentMoney { get => currentMoney; }// to access currenct money value outside of the currency class

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InvokeMoneyEvent();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        InvokeMoneyEvent();
    }

    public void DeductMoney(int amount)
    {
        currentMoney -= amount;
        InvokeMoneyEvent();
    }

    public void ResetValues(int amount)
    {
        currentMoney = amount;
    }

    private void InvokeMoneyEvent()
    {
        onMoneyChanged.Invoke();
    }

    public object CaptureState()
    {
        return new SaveData
        {
            money = currentMoney
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        currentMoney = saveData.money;
    }
    [Serializable]
    public struct SaveData
    {
        public int money;
    }
    
}



