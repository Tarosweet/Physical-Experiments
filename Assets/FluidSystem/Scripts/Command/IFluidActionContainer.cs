using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFluidActionContainer
{
    void Add(IFluidAction action);
    void Remove(IFluidAction action);
}
