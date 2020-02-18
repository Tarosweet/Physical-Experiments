using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class DisplayingEnergy : MonoBehaviour
{
    [SerializeField] private Text potentialEnergyText;
    [SerializeField] private Text kineticEnergyText;

    [SerializeField] private string potentialEnergyTextPrefix = "Потенциальная энергия: ";
    [SerializeField] private string kineticEnergyTextPrefix = "Кинетическая энергия: ";

    [SerializeField] private PhysicalMovingBody physicalMovingBody;

    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        potentialEnergyText.text = potentialEnergyTextPrefix + physicalMovingBody.PotentialEnergy.ToString("F");
        kineticEnergyText.text = kineticEnergyTextPrefix + physicalMovingBody.KineticEnergy.ToString("F");
    }
}
