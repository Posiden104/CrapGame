﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Betable : MonoBehaviour
{
    public int UnitBet;
    public int PayoutPerUnit;
    public int Bet;
    public int MinBet = 1;
    public List<GameObject> TokeList;
    public bool IsDont = false;
    public GameObject TokePlacement;

    // Start is called before the first frame update
    void Awake()
    {
        TokeList = new List<GameObject>();
        if(transform.childCount > 0) {
            TokePlacement = transform.GetChild(0).gameObject;
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void PlaceBet(int amtToBet)
    {
        if (IsValidBet(Bet + amtToBet))
        {
            Bet += amtToBet;
            RefreshTokeList();
            GameController.Instance.BankrollLabel_data.RemoveMoney(amtToBet);
        }
        else
        {
            Debug.LogError($"PlaceBet - {amtToBet} is not a valid bet for {transform.name}");
        }
    }

    public void RemoveBet(int amtToRemove)
    {
        if (Bet >= amtToRemove && IsValidBet(Bet - amtToRemove))
        {
            Bet -= amtToRemove;
            RefreshTokeList();
            GameController.Instance.BankrollLabel_data.AddMoney(amtToRemove);
        }
        else
            Debug.LogError($"RemoveBet - Cannot remove {amtToRemove} from the {transform.name}. Bet is only {Bet}");
    }

    public int Won(int multiplier = 1)
    {
        var units = Bet / UnitBet;
        var winnings = units * PayoutPerUnit;
        GameController.Instance.BankrollLabel_data.AddMoney(winnings * multiplier);
        return winnings * multiplier;
    }

    public int WonWithDifferentPayoutPerUnit(int payoutPerUnit)
    {
        var units = Bet / UnitBet;
        var winnings = units * payoutPerUnit;
        GameController.Instance.BankrollLabel_data.AddMoney(winnings);
        return winnings;
    }

    public int Lost()
    {
        var loss = Bet;
        Bet = 0;
        ClearBetTokes();
        return loss;
    }

    public List<GameObject> RefreshTokeList()
    {
        ClearBetTokes();
        int workingBet = Bet;
        TokeList = new List<GameObject>();
        foreach (int value in Toke.TokeValueList)
        {
            if((workingBet / value) > 0)
            {
                var multiple = workingBet / value;
                for (int i = 0; i < multiple; i++)
                {
                    TokeList.Add(Toke.Create(value, TokePlacement.transform));
                    workingBet -= value;
                }
            }
        }
        for (int i = 0; i < TokeList.Count; i++)
        {
            TokeList[i].transform.position = TokePlacement.transform.position;
            TokeList[i].transform.position += new Vector3(0, 0.05f * i, -0.0001f * i);
        }
        return TokeList;
    }

    public void ClearBetTokes()
    {
        while(TokeList.Count > 0)
        {
            Destroy(TokeList[0]);
            TokeList.RemoveAt(0);
        }
    }

    public bool IsValidBet(int bet)
    {
        if (
               (bet % UnitBet == 0)
            && (GameController.Instance.BankrollLabel_data.IsValidBet(bet) == true)
            && (bet >= MinBet))
        {
            return true;
        }

        if(bet == 0)
        {
            return true;
        }

        return false;
    }
}