using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HistoryLabel : MonoBehaviour
{
    private List<GameObject> Numbers;
    private float Spacing_L = 0.23f;
    private float Spacing_S = 0.4f;
    private Vector3 Offset = new Vector3(0, 0, 0);
    private Vector3 LastClearOffset;
    private int LastClearIndex;
    private float NextOrigin;
    
    // Start is called before the first frame update
    void Start()
    {
        Numbers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 GetNewOffset()
    {
        var newOffset = Offset;
        if (Numbers.Count == 0)
        {
            return newOffset;
        }

        newOffset.x += Spacing_S;
        if (Numbers.Last().GetComponent<HistoryNumber>().Number > 9)
        {
            newOffset.x += Spacing_L;
        }

        return newOffset;
    }


    public void RolledNumber(int number, bool win, bool lose, bool isNewPoint)
    {
        Color color = win ? HistoryNumber.Green : lose ? HistoryNumber.Red : isNewPoint ? HistoryNumber.Blue : HistoryNumber.White;
        var newOffset = GetNewOffset();
        Offset = newOffset;
        var newNum = HistoryNumber.Create(number, Offset, gameObject.transform, color);
        Numbers.Add(newNum);

        if(Camera.main.WorldToScreenPoint(newNum.transform.position).x > Screen.width)
        {
            Clear();
        }

        if (isNewPoint || lose)
        {
            LastClearOffset = Offset;
            LastClearIndex = Numbers.Count;
        }
    }

    public void Clear()
    {
        if (LastClearIndex > 0)
        {
            for (int i = 0; i < LastClearIndex - 1; i++)
            {
                Destroy(Numbers.First());
                Numbers.RemoveAt(0);
            }
        }
        if (Numbers.Count > 0)
        {
            foreach (var number in Numbers)
            {
                number.transform.position -= LastClearOffset;
            }
            Offset -= LastClearOffset;
        } else
        {
            Offset.x = 0f;
        }

        LastClearOffset.x = 0f;
        LastClearIndex = 0;

    }
}
