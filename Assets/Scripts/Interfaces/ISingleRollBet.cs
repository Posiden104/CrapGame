
public interface ISingleRollBet
{
    void PlaceBet(int bet);

    void RemoveBet(int bet);

    int Rolled(int rolledTotal);
}
