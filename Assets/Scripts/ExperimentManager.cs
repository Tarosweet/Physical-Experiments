using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    [SerializeField] private GameObject[] experimentPrefabs;

    private GameObject currentExperiment;
    
    public void SpawnExperiment(int index)
    {
        if (currentExperiment)
            DestroyExperiment();
        
        currentExperiment = Instantiate(experimentPrefabs[index]);
    }

    private void DestroyExperiment()
    {
        Destroy(currentExperiment);
    }
}
