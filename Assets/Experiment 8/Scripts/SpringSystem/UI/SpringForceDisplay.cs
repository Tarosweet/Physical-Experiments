using System;
using UnityEngine;
using UnityEngine.UI;

public class SpringForceDisplay : InstallationUI
{
    [SerializeField] private Text resultantForceText;
    
    [SerializeField] private StretchBetweenPoints stretchBetweenPoints;
    
    private JointsContainer _jointsContainer;

    private void Start()
    {
        resultantForceText.text = "R";
        _jointsContainer = GetComponent<JointsContainer>();
        
        stretchBetweenPoints.firstPointTransform = _jointsContainer.hook.transform;
        stretchBetweenPoints.secondPointTransform = _jointsContainer.mount.transform;
    }

    public override void Initialize(ref int index)
    {
        return;
    }
}
