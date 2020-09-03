using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public GameObject HistoryNumber;
    public GameObject Toke;
    public GameObject Dice;
    public GameObject ComeBox;
    public GameObject DontCome;
    public GameObject DontPass;
    public GameObject Field;
    public GameObject PassLine;
    public GameObject PlaceBox;
    public GameObject HistoryLabel;
    public GameObject CrapsTable;
    public GameObject PointLabel;
    public GameObject ComeOutLabel;
    public GameObject BankrollLabel;
    public GameObject BetWonLabel;
    public GameObject BetLostLabel;
}
