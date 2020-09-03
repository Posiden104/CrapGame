using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toke : MonoBehaviour
{
    public int Value = 1;
    private SpriteRenderer sr;
    public static List<int> TokeValueList = new List<int> { 25, 5, 1 };
    private float SpriteScale = 0.6f;

    public static GameObject Create(int value, Transform parent)
    {
        if (!TokeValueList.Contains(value))
        {
            Debug.LogError($"Toke Creation - {value} is not a valid value for Tokes.");
            return null;
        }

        var go = Instantiate(GameAssets.i.Toke, parent);
        go.GetComponent<Toke>().SetValue(value);
        return go;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(sr == null)
            sr = gameObject.AddComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int value)
    {
        if(sr == null)
        {
            sr = gameObject.AddComponent<SpriteRenderer>();
        }

        Value = value;
        switch (Value)
        {
            case 1:
                SpriteHelper.SetSprite(SpriteHelper.SpriteSheet.CasinoSprites, "FlatWhiteToke", sr);
                break;
            case 5:
                SpriteHelper.SetSprite(SpriteHelper.SpriteSheet.CasinoSprites, "FlatRedToke", sr);
                break;
            case 25:
                SpriteHelper.SetSprite(SpriteHelper.SpriteSheet.CasinoSprites, "FlatGreenToke", sr);
                break;
            default:
                Debug.Log($"Toke Start - {Value} is not a valid value.");
                break;
        }
        transform.localScale = new Vector3(SpriteScale, SpriteScale, SpriteScale);
    }
}
