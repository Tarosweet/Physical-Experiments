using System;
using UnityEngine;

namespace TestSystem.AnswerTypes
{
    [Serializable]
    public class AnswerChose : Answer
    {
        public override bool CheckAnswer()
        {
            return this == test.correctAnswer;
        }
    }
}