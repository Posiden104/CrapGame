using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Betable BetScript;
    public Clickable ClickScript;
    public List<SpriteRenderer> srList;

    void Awake()
    {
        BetScript = GetComponent<Betable>();
        ClickScript = GetComponent<Clickable>();
        ClickScript.RegisterBetCallback(BetScript.PlaceBet);
        ClickScript.RegisterRemoveBetCallback(BetScript.RemoveBet);
        BetScript.PayoutPerUnit = 5;
        BetScript.UnitBet = 5;
        srList = new List<SpriteRenderer>();
        BetScript.TokePlacement = transform.GetChild(0).gameObject;
    }

    public bool IsValidBet(int bet)
    {
        return bet >= 1;
    }

    public int Rolled(int rolledTotal)
    {
        if(rolledTotal == 3 || rolledTotal == 4 || rolledTotal == 9 || rolledTotal == 10 || rolledTotal == 11)
        {
            return BetScript.Won();
        } 
        else if(rolledTotal == 2 || rolledTotal == 12)
        {
            return BetScript.Won(2);
        } 
        else
        {
            return -BetScript.Lost();
        }
    }
}
