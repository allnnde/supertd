using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EconomyService : IEcomonyService
{
    private readonly IEcomonyController _ecomonyController;

    public EconomyService(IEcomonyController ecomonyController)
    {
        _ecomonyController = ecomonyController;
    }

    public int GetBalance()
    {
        return _ecomonyController.ActualBalance;
    }

    public void UpdateCoins(int balance)
    {
        var newBalance = _ecomonyController.ActualBalance + balance;
        _ecomonyController.UpdateCoins(newBalance);
    }
}