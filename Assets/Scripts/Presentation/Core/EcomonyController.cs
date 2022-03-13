using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EcomonyController : MonoBehaviour, IEcomonyController
{
    public TextMeshProUGUI textcoin;

    private IEcomonyService _ecomonyService;

    public int ActualBalance { get; private set; }

    private void Awake()
    {
        ActualBalance = 100;
    }

    public void Configuration(IEcomonyService ecomonyService)
    {
        _ecomonyService = ecomonyService;
    }

    private void Start()
    {
        _ecomonyService.UpdateCoins(0);
    }

    void IEcomonyController.UpdateCoins(int balance)
    {
        ActualBalance = balance;
        textcoin.text = $"coins {ActualBalance}";
    }
}