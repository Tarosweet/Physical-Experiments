using System;

namespace TestSystem.AnswerTypes
{
    [Serializable]
    public class AnswerWrite : Answer
    {
        public string answer;
        
        public override bool CheckAnswer()
        {
            var correctAnswer = test.GetComponent<AnswerWrite>();

            if (correctAnswer == null)
                return false;
                
            return correctAnswer.answer == answer;
        }
    }
}