using System;
using UnityEngine;

namespace TestSystem
{
    [Serializable]
    public abstract class Answer : MonoBehaviour
    {
        [SerializeField] protected Test test;

        public abstract bool CheckAnswer();
    }
}