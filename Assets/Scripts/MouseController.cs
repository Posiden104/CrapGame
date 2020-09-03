using UnityEditor;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RollTheDice()
    {
        GameController.Instance.RollTheDice();
    }

    public void ClearDice()
    {
        GameController.Instance.ClearDice();
    }

    public void ClearNumbers()
    {
        GameController.Instance.ClearNumbers();
    }
}
