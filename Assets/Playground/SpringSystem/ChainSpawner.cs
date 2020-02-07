using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChainSpawner 
{
    public static WeightsChain CreateChain(List<JointsContainer> jointsContainers)
    {
        GameObject go = new GameObject("Chain");

        var createdChain = go.AddComponent<WeightsChain>();

        createdChain.containers = jointsContainers;

        SetParentsToJointsContainers(go.transform, jointsContainers, createdChain);
        
        return createdChain;
    }

    private static void SetParentsToJointsContainers(Transform parent,List<JointsContainer> jointsContainers, WeightsChain chain)
    {
        foreach (var jointContainer in jointsContainers)
        {
            jointContainer.transform.SetParent(parent);
            jointContainer._weightsChain = chain;
        }
    }
}
