
public interface ISingleRollBet
{
    void Start();

    void PlaceBet(int bet);

    void RemoveBet(int bet);

    int Rolled(int rolledTotal);
}
