using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IEcomonyService
{
    void UpdateCoins(int balance);

    int GetBalance();
}