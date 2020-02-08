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
            firstJointsContainer.weightsChain = firstJointsContainer.weightsChain
                .CombineChains(secondJointContainer.weightsChain);
            return firstJointsContainer.weightsChain;
        }

        if (firstJointsContainer.IsChainExist())
        {
            firstJointsContainer.weightsChain.Add(secondJointContainer);
            return firstJointsContainer.weightsChain;
        }

        if (secondJointContainer.IsChainExist())
        {
            secondJointContainer.weightsChain.Add(firstJointsContainer);
            return secondJointContainer.weightsChain;
        }

        List<JointsContainer> jointsContainers = new List<JointsContainer> {firstJointsContainer,secondJointContainer };

        return ChainSpawner.CreateChain(jointsContainers);
    }
}
