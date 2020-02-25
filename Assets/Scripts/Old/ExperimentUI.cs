using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentUI : MonoBehaviour
{
    public void Retry()
    {
        SpawnManager experimentSpawner = GameObject.Find("Experiment Manager").GetComponent<SpawnManager>();
        SpawnManager stageSpawner = FindObjectOfType<SpawnManager>();
        
        int indexExperiment = experimentSpawner.currentIndex;

        stageSpawner.DestroyPrefab();
        experimentSpawner.DestroyPrefab();
        
        experimentSpawner.SpawnPrefab(indexExperiment);
    }

    public void BackToExperimentSelection()
    {
        SpawnManager experimentSpawner = GameObject.Find("Experiment Manager").GetComponent<SpawnManager>();
        SpawnManager stageSpawner = FindObjectOfType<SpawnManager>();
        
        stageSpawner.DestroyPrefab();
        experimentSpawner.DestroyPrefab();
        
        FindObjectOfType<UI>().SetUI(true);
    }
}
