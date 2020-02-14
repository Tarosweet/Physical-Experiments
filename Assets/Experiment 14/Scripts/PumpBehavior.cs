using System;
using UnityEngine;

namespace Experiment_14.Scripts
{
    public abstract class PumpBehavior
{
    public abstract void Pump(IPumped pumped);
    }
}