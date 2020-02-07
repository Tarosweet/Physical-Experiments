using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChainResolver
{
    public static WeightsChain Resolve(JointsContainer firstJointsContainer, JointsContainer secondJointContainer)
    {
        if (firstJointsContainer.GetComponent<StaticMount>() || secondJointContainer.GetComponent<StaticMount>())
            return null;
        
        if (firstJointsContainer.IsChainExist() && secondJointContainer.IsChainExist())
        {
            Debug.Log("CONCAT");
            firstJointsContainer._weightsChain = firstJointsContainer._weightsChain
                .CombineChains(secondJointContainer._weightsChain);
            return firstJointsContainer._weightsChain;
        }

        if (firstJointsContainer.IsChainExist())
        {
            firstJointsContainer._weightsChain.Add(secondJointContainer);
            return firstJointsContainer._weightsChain;
        }

        if (secondJointContainer.IsChainExist())
        {
            secondJointContainer._weightsChain.Add(firstJointsContainer);
            return secondJointContainer._weightsChain;
        }

        List<JointsContainer> jointsContainers = new List<JointsContainer> {firstJointsContainer,secondJointContainer };

        return ChainSpawner.CreateChain(jointsContainers);
    }
}
