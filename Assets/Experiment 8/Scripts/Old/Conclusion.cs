using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conclusion : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void ChangePanelState()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
