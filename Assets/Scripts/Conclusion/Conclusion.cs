using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conclusion : MonoBehaviour
{
    [SerializeField] private GameObject conclusionPanel;

    [SerializeField] private GameObject conclusionButton;

    public void CloseConclusion()
    {
        conclusionPanel.SetActive(false);
        conclusionButton.SetActive(true);
    }

    public void OpenConclusion()
    {
        conclusionPanel.SetActive(true);
        conclusionButton.SetActive(false);
    }
}
