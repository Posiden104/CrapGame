using Assets.Scripts;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlaceBox : ClickableBet
{
    public int Number;

    private void Start()
    {
        if (Number == 4 || Number == 10)
        {
            BetScript.PayoutPerUnit = 9;
            BetScript.UnitBet = 5;
        }
        else if (Number == 5 || Number == 9)
        {
            BetScript.PayoutPerUnit = 7;
            BetScript.UnitBet = 5;
        }
        else if (Number == 6 || Number == 8)
        {
            BetScript.PayoutPerUnit = 7;
            BetScript.UnitBet = 6;
        }

        ClickScript.RegisterHoverCallback(Hover);
    }

    public bool IsValidBet(int bet)
    {
        if (Number == 4 || Number == 5 || Number == 9 || Number == 10)
        {
            return bet % 5 == 0;
        }
        else if (Number == 6 || Number == 8)
        {
            return bet % 6 == 0;
        }

        Debug.LogError("IsValidBet - Number is not 4, 5, 6, 8, 9, or 10");
        return false;
    }

    public void Hover()
    {
        // 6 & 8 bet modifier
        if (transform.name == "PlaceBox_6" || transform.name == "PlaceBox_8")
        {
            if (gc.BetAmount % 5 == 0 && gc.BetChanged == false)
            {
                gc.BetAmountSaver = GameController.Instance.BetAmount;
                int addToBet = GameController.Instance.BetAmount / 5;
                gc.BetChanged = true;
                GameController.Instance.BetAmount = gc.BetAmountSaver + addToBet;
            }
        }
    }
}
