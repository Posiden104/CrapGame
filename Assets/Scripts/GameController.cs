﻿using Assets.Scripts;
using System;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject CrapsTable_go;
    public CrapsTable CrapsTable_data;
    public bool RollOverride = false;
    public int OverrideRollTotal;
    public int BetAmount;

    private GameObject d1, d2;
    private Dice d1Dice, d2Dice;
    private int amtWon = 0, amtLost = 0;
    public HistoryLabel HistoryLabel_data { get; private set; }
    public PointLabel PointLabel_data { get; private set; }
    public ComeOutLabel ComeOutLabel_data { get; private set; }
    public Bankroll BankrollLabel_data { get; private set; }
    public GameObject BetWonLabel_go { get; private set; }
    public GameObject BetLostLabel_go { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        d1 = Instantiate(GameAssets.i.Dice);
        d1.SetActive(true);
        d1.name = "Dice 1";
        d1Dice = d1.GetComponent<Dice>();

        d2 = Instantiate(GameAssets.i.Dice);
        d1.SetActive(true);
        d2.transform.position += new Vector3(1, 0, 0);
        d2.name = "Dice 2";
        d2Dice = d2.GetComponent<Dice>();

        d1.SetActive(false);
        d2.SetActive(false);

        CrapsTable_data = CrapsTable_go.GetComponent<CrapsTable>();
        HistoryLabel_data = Instantiate(GameAssets.i.HistoryLabel).GetComponent<HistoryLabel>();
        PointLabel_data = Instantiate(GameAssets.i.PointLabel).GetComponent<PointLabel>();
        ComeOutLabel_data = Instantiate(GameAssets.i.ComeOutLabel).GetComponent<ComeOutLabel>();
        BankrollLabel_data = Instantiate(GameAssets.i.BankrollLabel).GetComponent<Bankroll>();
        BetWonLabel_go = Instantiate(GameAssets.i.BetWonLabel);
        BetLostLabel_go = Instantiate(GameAssets.i.BetLostLabel);
        BetWonLabel_go.transform.GetChild(0).GetComponent<TextMeshPro>().SetText($"${String.Format("{0:n0}", 0)}");
        BetLostLabel_go.transform.GetChild(0).GetComponent<TextMeshPro>().SetText($"${String.Format("{0:n0}", 0)}");

        BetWonLabel_go.SetActive(false);
        BetLostLabel_go.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RollTheDice()
    {
        d1.SetActive(true);
        d2.SetActive(true);
        int rolledTotal = d1Dice.Roll();
        rolledTotal += d2Dice.Roll();

        if (RollOverride)
        {
            rolledTotal = OverrideRollTotal;
            d1Dice.OverrideDiceFace(Mathf.RoundToInt(rolledTotal / 2.1f));
            d2Dice.OverrideDiceFace(Mathf.RoundToInt(rolledTotal / 1.9f));
        }

        int point = CrapsTable_data.Point;
        bool win = (point == 0 && CrapsTable.IsComeOutWinner(rolledTotal)) || rolledTotal == point;
        bool lose = (point != 0 && rolledTotal == 7) || (point == 0 && CrapsTable.IsCraps(rolledTotal));
        bool isNewPoint = point == 0 && CrapsTable.IsPointable(rolledTotal);

        if(win && lose)
        {
            Debug.LogError("RollTheDice - Can't win AND Lose");
        }

        HistoryLabel_data.RolledNumber(rolledTotal, win, lose, isNewPoint);
        CrapsTable_data.DiceRoll(rolledTotal, out amtWon, out amtLost);
        if(amtWon > 0)
        {
            Debug.LogError("won " + amtWon);
            BetWonLabel_go.SetActive(true);
            BetWonLabel_go.transform.GetChild(0).GetComponent<TextMeshPro>().SetText($"${String.Format("{0:n0}", amtWon)}");
            BetWonLabel_go.transform.GetChild(0).GetComponent<TextMeshPro>().ForceMeshUpdate();
        }
        else
        {
            BetWonLabel_go.SetActive(false);
        }
        if(amtLost > 0)
        {
            Debug.LogError("lost "+amtLost);
            BetLostLabel_go.SetActive(true);
            BetLostLabel_go.transform.GetChild(0).GetComponent<TextMeshPro>().SetText($"${String.Format("{0:n0}", amtLost)}");
            BetLostLabel_go.transform.GetChild(0).GetComponent<TextMeshPro>().ForceMeshUpdate();
        }
        else
        {
            BetLostLabel_go.SetActive(false);
        }
    }

    public void ClearDice()
    {
        d1.SetActive(false);
        d2.SetActive(false);
    }

    public void ClearNumbers()
    {
        HistoryLabel_data.Clear();
    }

}
