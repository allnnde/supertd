public interface IEcomonyController
{
    int ActualBalance { get; }

    void UpdateCoins(int balance);
}