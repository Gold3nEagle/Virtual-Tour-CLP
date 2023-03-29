using UnityEngine;
using UnityEngine.Events;
using System;

public class CurrencySystem : MonoBehaviour, ISaveable
{
    public static CurrencySystem instance;//currency instance

    public int currentMoney = 0; //current money value accessed inside the class
    public UnityEvent onMoneyChanged;// unity event to invoke when the money is changed

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

    // === === === === ===
    // Saving/ Loading related stuff
    // === === === === ===

    public object CaptureState() => new SaveData { money = currentMoney };

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
