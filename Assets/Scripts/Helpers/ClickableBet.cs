using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBet : MonoBehaviour
{
    internal Betable BetScript;
    Clickable ClickScript;
    List<SpriteRenderer> srList;

    private void Awake()
    {
        BetScript = GetComponent<Betable>();
        ClickScript = GetComponent<Clickable>();
        ClickScript.RegisterBetCallback(BetScript.PlaceBet);
        ClickScript.RegisterRemoveBetCallback(BetScript.RemoveBet);
        BetScript.PayoutPerUnit = 5;
        BetScript.UnitBet = 5;
        srList = new List<SpriteRenderer>();
    }
}
