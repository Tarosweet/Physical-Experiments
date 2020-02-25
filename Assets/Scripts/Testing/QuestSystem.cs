using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestSystem : MonoBehaviour
{
    public UnityEvent endExperimentEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            endExperimentEvent?.Invoke();
    }
}
