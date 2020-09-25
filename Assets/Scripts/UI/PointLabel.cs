using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLabel : MonoBehaviour
{
    //private GameObject Number_go;
    private HistoryNumber Number_data;

    // Start is called before the first frame update
    void Start()
    {
        //Number_go = transform.GetChild(0).gameObject;
        Number_data = GetComponentInChildren<HistoryNumber>();

        ClearPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPoint(int num)
    {
        Number_data.SetNumber(num, HistoryNumber.White);
        gameObject.SetActive(true);
        //Number_go.SetActive(true);
    }

    public void ClearPoint()
    {
        gameObject.SetActive(false);
        //Number_go.SetActive(false);
    }
}
