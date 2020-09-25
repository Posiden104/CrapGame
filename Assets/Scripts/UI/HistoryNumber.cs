using UnityEngine;
using TMPro;
using System.Security.Policy;

public class HistoryNumber : MonoBehaviour
{
    public int Number { get; private set; }
    public static Color White = new Color(255, 255, 255);
    public static Color Green = new Color(0, 255, 0);
    public static Color Red = new Color(255, 0, 0);
    public static Color Blue = new Color(0, 0, 255);

    private TextMeshPro textMesh;

    public static GameObject Create(int number, Vector3 offset, Transform parent, Color color)
    {
        GameObject historyNumber = Instantiate(GameAssets.i.HistoryNumber.gameObject, parent);
        HistoryNumber hn = historyNumber.GetComponent<HistoryNumber>();
        hn.SetNumber(number, color);
        historyNumber.transform.position += offset;
        return historyNumber;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetNumber(int _num, Color color)
    {
        Number = _num;
        textMesh.SetText($" {_num}");
        textMesh.color = color;
    }
}
