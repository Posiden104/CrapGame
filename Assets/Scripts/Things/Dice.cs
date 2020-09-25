using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int Value = 1;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Roll()
    {
        Value = Random.Range(1, 7);
        SpriteHelper.SetSprite(SpriteHelper.SpriteSheet.CasinoSprites, $"Red{Value}", gameObject.GetComponent<SpriteRenderer>());

        return Value;
    }

    public void OverrideDiceFace(int value)
    {
        if (GameController.Instance.RollOverride == false)
            return;

        Value = value;
        SpriteHelper.SetSprite(SpriteHelper.SpriteSheet.CasinoSprites, $"Red{Value}", gameObject.GetComponent<SpriteRenderer>());
    }
}
