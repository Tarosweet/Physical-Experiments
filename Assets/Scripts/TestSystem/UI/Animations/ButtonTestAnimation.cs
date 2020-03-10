using UnityEngine;
using UnityEngine.UI;

namespace TestSystem.UI.Animations
{
    public class ButtonTestAnimation : MonoBehaviour
    {
        [SerializeField] private Image buttonImage;

        [SerializeField] private ButtonsTestAnimations buttonsTestAnimations;

        public void SetColor(Color color)
        {
            buttonImage.color = color;
        }

        public void OnClick()
        {
            buttonsTestAnimations.clickedButton = this;
        }
    }
}
