using System;
using Helpers;
using UnityEngine;

namespace User_Interface
{
    [RequireComponent(typeof(Raycaster))]
    public class ExplanatoryRaycastTrigger : MonoBehaviour
    {
        private Raycaster _raycaster;

        private ExplanatoryObject _explanatoryObject;

        private bool _isShowed;

        private void Start()
        {
            _raycaster = GetComponent<Raycaster>();
        }

        private void Update()
        {
            if (_raycaster.ThrowRaycast(out var hit))
            {
                var hitUserInterface = hit.collider.GetComponent<ExplanatoryObject>();

                if (hitUserInterface && !_isShowed)
                {
                    Show(hitUserInterface);
                }
            }
            else
            {
                Hide();
            }
        }

        private void Show(ExplanatoryObject explanatoryObject)
        {
            _explanatoryObject = explanatoryObject;
            _explanatoryObject.Toggle();
            _isShowed = true;
        }

        private void Hide()
        {
            if (_explanatoryObject == null)
                return;
            
            _explanatoryObject.Toggle();
            _explanatoryObject = null;
            _isShowed = false;
        }
    }
}
