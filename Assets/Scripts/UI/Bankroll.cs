using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bankroll : MonoBehaviour
{
    public int StartingMoney;
    public int Money { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        AddMoney(StartingMoney);
    }

    public bool IsValidBet(int bet)
    {
        if(Money >= bet)
        {
            return true;
        }
        else
        {
            Debug.LogError("Bankroll IsValidBet - tried to bet money on credit!");
            return false;
        }
    }

    public void RemoveMoney(int amtToRemove)
    {
        Money -= amtToRemove;
        UpdateMoneyLabel();
    }

    public void AddMoney(int amtToAdd)
    {
        Money += amtToAdd;
        UpdateMoneyLabel();
    }

    void UpdateMoneyLabel()
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().SetText($"${String.Format("{0:n0}", Money)}");
    }

}
