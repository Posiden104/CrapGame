using Assets;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class CrapsTable : MonoBehaviour
{
    public int Point { get; private set; }

    private int amtWon;
    private int amtLost;

    public GameObject GetChildOfName(string name)
    {
        foreach (Transform child in transform)
        {
            if(child.name == name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void DiceRoll(int rolledTotal, out int won, out int lost)
    {
        amtWon = 0;
        amtLost = 0;
        if (Point == 0)
        {
            // Come Out
            if (IsPointable(rolledTotal))
            {
                EstablishPoint(rolledTotal);
            }
            else if(IsCraps(rolledTotal))
            {
                CrapOut();
            } 
            else if (IsComeOutWinner(rolledTotal))
            {
                ComeOutWinner();
            }

            // working bets

            //if (IsPointable(rolledTotal))
            //{
            //    AddWinnings(GetChildOfName($"PlaceBox_{rolledTotal}").GetComponent<Betable>().Won());
            //}
        }
        else 
        {
            // Point Established 
            if (rolledTotal == 7) {
                DevilPopsUp();
            } 
            else
            {
                if (rolledTotal == Point)
                {
                    PointMade();
                }
                if (IsPointable(rolledTotal))
                {
                    AddWinnings(GetChildOfName($"PlaceBox_{rolledTotal}").GetComponent<Betable>().Won());
                }
            }
        }
        SingleRollBets(rolledTotal);

        won = amtWon;
        lost = amtLost;
    }

    public static bool IsPointable(int rolledTotal)
    {
        return rolledTotal == 4 || rolledTotal == 5 || rolledTotal == 6 || rolledTotal == 8 || rolledTotal == 9 || rolledTotal == 10;
    }

    public static bool IsCraps(int num)
    {
        return num == 2 || num == 3 || num == 12;
    }

    public static bool IsComeOutWinner(int num)
    {
        return num == 7 || num == 11;
    }

    public void SingleRollBets(int rolledTotal)
    {
        var winnings = GetChildOfName("Field").GetComponent<Field>().Rolled(rolledTotal);
        if(winnings > 0)
        {
            AddWinnings(winnings);
        } else if(winnings < 0)
        {
            AddLoss(-winnings);
        }

        foreach (ISingleRollBet srb in GetComponentsInChildren<ISingleRollBet>())
        {
            winnings = srb.Rolled(rolledTotal);
            if (winnings > 0)
            {
                AddWinnings(winnings);
            }
            else if (winnings < 0)
            {
                AddLoss(-winnings);
            }
        } 
    }

    private void EstablishPoint(int newPoint)
    {
        Point = newPoint;
        GameController.Instance.PointLabel_data.SetPoint(Point);
        GameController.Instance.ComeOutLabel_data.SetPoint();
    }

    private void ComeOutWinner()
    {
        var winnings = GetChildOfName("PassLine").GetComponent<Betable>().Won();
        ClearPoint();
        AddWinnings(winnings);
    }

    private void CrapOut()
    {
        var loss = GetChildOfName("PassLine").GetComponent<Betable>().Lost();
        ClearPoint();
        AddLoss(loss);
    }

    public void PointMade()
    {
        var winnings = GetChildOfName("PassLine").GetComponent<Betable>().Won();
        ClearPoint();
        AddWinnings(winnings);
    }

    private void DevilPopsUp()
    {
        var loss = 0;
        foreach (Transform child in transform)
        {
            var betable = child.GetComponent<Betable>();
            if(betable != null && betable.IsDont == false)
            {
                loss += betable.Lost();
            }
        }
        ClearPoint();
        AddLoss(loss);
    }

    public void ClearPoint()
    {
        Point = 0;
        GameController.Instance.PointLabel_data.ClearPoint();
        GameController.Instance.ComeOutLabel_data.ClearPoint();
    }

    public void AddWinnings(int amtToAdd)
    {
        amtWon += amtToAdd;
    }
    public void AddLoss(int amtToAdd)
    {
        amtLost += amtToAdd;
    }
}
