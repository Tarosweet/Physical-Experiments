using UnityEngine;
using UnityEngine.UI;

public class WeightForceDisplay : InstallationUI
{
    [SerializeField] private Text forceText;

    private string Force
    {
        get => forceText.text;
        set => forceText.text = value;
    }

    public override void Initialize(ref int index)
    {
        Force = "F" + index;
        index++;
    }
}
