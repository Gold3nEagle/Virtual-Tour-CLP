using UnityEngine;
using UnityEngine.Events;

public class CurrencySystem : MonoBehaviour
{

    public static CurrencySystem instance;//currency instance


    private int currentMoney = 0; //current money value accessed inside the class

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

    public void SubtractMoney(int amount)
    {
        currentMoney -= amount;
        InvokeMoneyEvent();
    }

    private void InvokeMoneyEvent()
    {
        onMoneyChanged.Invoke();
    }
}



