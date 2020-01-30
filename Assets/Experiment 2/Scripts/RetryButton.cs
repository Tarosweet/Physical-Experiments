using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
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
}
