using System.Collections.Generic;

public class CAndEBet : ClickableBet, ISingleRollBet
{
    readonly List<int> crapsNumbers = new List<int> { 2, 3, 12 };
    readonly int CPayout = 3;
    readonly int EPayout = 7;

    public void Start()
    {
        ClickScript.UnRegisterBetCallback(BetScript.PlaceBet);
        ClickScript.UnRegisterRemoveBetCallback(BetScript.RemoveBet);

        ClickScript.RegisterBetCallback(PlaceBet);
        ClickScript.RegisterRemoveBetCallback(RemoveBet);

        BetScript.UnitBet = 2;
    }

    public void PlaceBet(int bet)
    {
        if(bet % 2 == 1)
        {
            // betscript will reject bet of 1
            BetScript.PlaceBet(bet + 1);
        }
    }

    public void RemoveBet(int bet)
    {
        if (bet % 2 == 1)
        {
            // betscript will reject bet removal of 1
            BetScript.RemoveBet(bet + 1);
        }
    }

    public int Rolled(int rolledTotal)
    {
        if (crapsNumbers.Contains(rolledTotal))
        {
            // C won
            return CPayout * BetScript.Bet;
        } 
        else if(rolledTotal == 11)
        {
            // E won
            return EPayout * BetScript.Bet;
        }
        else
        {
            // lost
            return BetScript.Lost() * -1;
        }
    }

}
