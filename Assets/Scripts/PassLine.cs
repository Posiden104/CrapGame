using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

namespace Assets
{
    public class PassLine : MonoBehaviour
    {
        Betable BetScript;
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
            BetScript.TokePlacement = transform.GetChild(0).gameObject;
        }
    }
}