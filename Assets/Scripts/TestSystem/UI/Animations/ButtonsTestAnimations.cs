using System.Collections.Generic;
using UnityEngine;

namespace TestSystem.UI.Animations
{
    public class ButtonsTestAnimations : MonoBehaviour
    {
        [SerializeField] private Color correctButtonColor = Color.green;
        [SerializeField] private Color incorrectButtonColor = Color.red;
        [SerializeField] private Color defaultButtonColor = Color.white;
        
        [SerializeField] private List<ButtonTestAnimation> buttonTestAnimations;

        public ButtonTestAnimation clickedButton;

        public void SetColors(bool isCorrect)
        {
            foreach (var button in buttonTestAnimations)
            {
                if (button == clickedButton)
                {
                    button.SetColor(isCorrect ? correctButtonColor : incorrectButtonColor);
                }
                else
                {
                    button.SetColor(defaultButtonColor);
                }
            }
        }
    }
}