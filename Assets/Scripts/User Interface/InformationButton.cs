using UnityEngine;

namespace User_Interface
{
    public class InformationButton : ExplanatoryUserInterface
    {
        [SerializeField] private bool isAutoFindTargets;

        private void Start()
        {
            if (isAutoFindTargets)
                AddAllInformationUIs();
        }

        private void AddAllInformationUIs()
        {
            var uis = FindObjectsOfType<ExplanatoryObject>();
            
            foreach (var ui in uis) {
                onTriggered.AddListener(ui.Toggle);
                ui.gameObject.SetActive(false);
            }
        }
    }
}
