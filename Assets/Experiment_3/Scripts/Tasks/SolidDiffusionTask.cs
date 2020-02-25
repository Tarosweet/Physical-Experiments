using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidDiffusionTask : ITask
{
    private QuestionObserverSolids _observer;
    private SolidDiffusion _solidDiffusion;
    private bool _isEnd;
    public SolidDiffusionTask(QuestionObserverSolids observer, SolidDiffusion diffusion)
    {
        _isEnd = false;
        _observer = observer;
        _solidDiffusion = diffusion;
        _solidDiffusion.OnEndDiffusion += EndAnimationDiffusion;
    }
    
    public void Start()
    {
        _solidDiffusion.StartDiffusion();
    }

    public void Remove()
    {
        _solidDiffusion.OnEndDiffusion -= EndAnimationDiffusion; 
    }

    private void EndAnimationDiffusion()
    {
        _isEnd = true;
        Remove();
        ITask task = new FinishTask(_observer);
        _observer._diffusionIsEnd = true;
        _observer.ChangeTask(task);
    }
}
