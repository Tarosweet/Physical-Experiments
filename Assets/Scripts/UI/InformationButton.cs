using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Experiments.UI
{
    public class InformationButton : MonoBehaviour
    {
        public UnityEvent onInformationButtonClick;

        [SerializeField] private bool isAutoFindTargets;

        private void Start()
        {
            if (isAutoFindTargets)
                AddAllInformationUIs();
        }

        public void OnClick()
        {
            onInformationButtonClick?.Invoke();
        }

        private void AddAllInformationUIs()
        {
            var uis = FindObjectsOfType<InformationUI>();
            
            foreach (var ui in uis) {
                onInformationButtonClick.AddListener(ui.Toggle);
                ui.gameObject.SetActive(false);
            }
        }
    }
}
