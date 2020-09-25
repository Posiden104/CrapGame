using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

namespace Assets
{
    public class PassLine : ClickableBet
    {
        private void Start()
        {
            BetScript.PayoutPerUnit = 5;
            BetScript.UnitBet = 5;
        }
    }
}