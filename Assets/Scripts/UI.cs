using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject startUI;

    public void SetUI(bool condition)
    {
        startUI.SetActive(condition);
    }
}
