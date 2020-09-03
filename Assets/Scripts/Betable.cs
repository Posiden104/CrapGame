using System;
using System.Collections.Generic;
using UnityEngine;

public class Betable : MonoBehaviour
{
    public int UnitBet;
    public int PayoutPerUnit;
    public int Bet;
    public List<GameObject> TokeList;
    public bool IsDont = false;
    public GameObject TokePlacement;

    // Start is called before the first frame update
    void Start()
    {
        TokeList = new List<GameObject>();
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
            Debug.LogError($"RemoveBet - Cannot remove {amtToRemove} from the {transform.name}");
    }

    public int Won(int multiplier = 1)
    {
        var units = Bet / UnitBet;
        var winnings = units * PayoutPerUnit;
        GameController.Instance.BankrollLabel_data.AddMoney(winnings * multiplier);
        return winnings * multiplier;
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
        return (bet % UnitBet == 0) && (GameController.Instance.BankrollLabel_data.IsValidBet(bet));
    }
}
