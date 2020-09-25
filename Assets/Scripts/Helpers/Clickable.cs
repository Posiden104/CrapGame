using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Clickable : MonoBehaviour
{
    RectTransform Rect;
    Action<int> cbBet;
    Action<int> cbRemoveBet;
    Action cbHover;
    Vector2 MousePos;

    private void Start()
    {
        Rect = GetComponent<RectTransform>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // hover
        if (CheckMouseOver(MousePos))
        {
            // some kind of highlight

            if (cbHover != null) {
                cbHover();
            }

        } 
        else if(GameController.Instance.BetChanged == true)
        {
            GameController.Instance.ResetBetSaver();
        }

        // left click
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RectTransform rect = GetComponent<RectTransform>();
            if (rect != null && RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos, Camera.main))
            {
                BetClicked(GameController.Instance.BetAmount);
            } 
            else
            {
                PolygonCollider2D poly = GetComponent<PolygonCollider2D>();

                if(poly != null && ((Vector2)poly.ClosestPoint(mousePosWorld)) == mousePosWorld)
                {
                    BetClicked(GameController.Instance.BetAmount);
                }
            }
        }

        // right click
        if (Input.GetMouseButtonUp(1))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RectTransform rect = GetComponent<RectTransform>();
            if (rect != null && RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos, Camera.main))
            {
                BetRightClicked(GameController.Instance.BetAmount);
            }
            else
            {
                PolygonCollider2D poly = GetComponent<PolygonCollider2D>();

                if (poly != null && ((Vector2)poly.ClosestPoint(mousePosWorld)) == mousePosWorld)
                {
                    BetRightClicked(GameController.Instance.BetAmount);
                }
            }
        }
    }

    public void BetClicked(int bet)
    {
        cbBet(bet);
    }

    public void BetRightClicked(int bet)
    {
        cbRemoveBet(bet);
    }

    public void RegisterBetCallback(Action<int> callback)
    {
        cbBet += callback;
    }

    public void UnRegisterBetCallback(Action<int> callback)
    {
        cbBet -= callback;
    }

    public void RegisterRemoveBetCallback(Action<int> callback)
    {
        cbRemoveBet += callback;
    }

    public void UnRegisterRemoveBetCallback(Action<int> callback)
    {
        cbRemoveBet -= callback;
    }

    public void RegisterHoverCallback(Action callback)
    {
        cbHover += callback;
    }

    public void UnRegisterHoverCallback(Action callback)
    {
        cbHover -= callback;
    }

    public bool CheckMouseOver(Vector2 mousePos)
    {
        return Rect != null && Rect.rect.Contains(Rect.InverseTransformPoint(mousePos));
    }
}
