using UnityEngine;

namespace User_Interface.MainMenu
{
    public class MainMenuUserInterface : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject experimentCanvas;

        public void ChangeState()
        {
            mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
            experimentCanvas.SetActive(!experimentCanvas.activeSelf);
        }
    }
}
