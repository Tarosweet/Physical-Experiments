using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipSluiceTask
{
    void Start();
    void Stop();
    void SetNext(IShipSluiceTask task);
    void StartNext();
}
