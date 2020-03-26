using System;
using TestSystem.AnswerTypes;
using UnityEngine;
using UnityEngine.UI;

namespace TestSystem.UI
{
    public class AnswerWriteInput : MonoBehaviour
    {
        [SerializeField] private AnswerWrite answerWrite;

        [SerializeField] private InputField inputField;

        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(UpdateAnswer);
        }

        private void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(UpdateAnswer);
        }

        private void UpdateAnswer(string answer)
        {
            answerWrite.answer = answer;
        }
    }
}
