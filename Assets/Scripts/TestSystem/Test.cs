using UnityEngine;
using UnityEngine.Events;

namespace TestSystem
{
    public class Test : MonoBehaviour
    {
        public Answer correctAnswer;

        [SerializeField] private UnityEvent onGiveCorrectAnswer;
        [SerializeField] private UnityEvent onGiveIncorrectAnswer;
        
        public void GiveAnAnswer(Answer answer)
        {
            if (answer.CheckAnswer())
            {
                onGiveCorrectAnswer?.Invoke();
                Debug.Log("Correct!");
                return;
            }
            
            onGiveIncorrectAnswer?.Invoke();
        }
    }
}
