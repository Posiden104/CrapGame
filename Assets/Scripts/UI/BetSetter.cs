using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetSetter : MonoBehaviour
{
    public int betAmount;
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        GameController.Instance.BetAmount = betAmount;
    }
}
