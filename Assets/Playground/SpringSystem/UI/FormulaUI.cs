using UnityEngine;
using UnityEngine.UI;


public class FormulaUI : MonoBehaviour
{
    [SerializeField] private Text text;

    private string f = "F";

    public void CalculateFormula(int index)
    {
        string completeString = "";
        
        Debug.Log(index);

        for (int i = 1; i <= index; i++)
        {
            completeString +=  f + i.ToString();

            if (i < index) completeString += "+";
        }

        text.text = completeString;
    }
}
