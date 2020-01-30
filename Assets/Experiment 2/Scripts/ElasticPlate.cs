using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticPlate : MonoBehaviour
{
    private Animator animator;
    private static readonly int Unbend = Animator.StringToHash("Unbend");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayUnbendAnimation()
    {
        animator.SetTrigger(Unbend);
    }
}
