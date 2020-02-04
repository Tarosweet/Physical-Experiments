using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MassMarker : MonoBehaviour
{

    [SerializeField] private GameObject markerCanvas;
    
    [SerializeField] private Text massText;
    [SerializeField] private Text formulaText;

    private string formula;

    [SerializeField] private HookJoint root;

    private float mass;
    
    
    
    public void CalculateGeneralMass()
    {
        mass = GetMass();
        ShowOnUI();
    }

    private float GetMass()
    {
        Rigidbody connectedBody = root.connectedBody;
        float newMass = 0;
        string newFormula = "R = ";
        int i = 0;
        while (connectedBody)
        {
            newMass += connectedBody.mass;

            connectedBody = connectedBody.transform.Find("Hook").GetComponent<HookJoint>().connectedBody;

            i++;
            newFormula += GetFormula(i);
        }

        formula = newFormula;

        return newMass;
    }

    private void ShowOnUI()
    {
        if (mass > 0)
            markerCanvas.SetActive(true);
        else markerCanvas.SetActive(false);
        
        massText.text = (mass / 0.102f).ToString() + "H";
        formulaText.text = formula;
    }

    private string GetFormula(int index)
    {
        string f = "F";
        string newFormula = "";

        if (index >= 2)
            newFormula += "+";
        newFormula += f + index.ToString();
         return newFormula;
    }
}
