using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConclusionButton : MonoBehaviour
{
    [SerializeField] private Conclusion _conclusion;
    
    public void SetButtonState(bool value)
    {
        gameObject.SetActive(value);
    }

    public void OnConclusionButtonClick()
    {
        _conclusion.ChangePanelState();
    }
}
