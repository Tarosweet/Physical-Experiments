using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AR
{
    // ReSharper disable once InconsistentNaming
    public class ARSpawnRaycast : MonoBehaviour
    {
        [SerializeField] private GameObject placementIndicator;
        
        private ARRaycastManager _arRaycastManager;
        
        private Pose _placementPose;
        private bool _placementPoseIsValid = false;
        
        private Vector3 center = new Vector3(0.5f,0.5f);

        private void Start()
        {
            _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        }

        private void Update()
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
        }

        private void UpdatePlacementIndicator()
        {
            placementIndicator.SetActive(_placementPoseIsValid);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }

        private void UpdatePlacementPose()
        {
            var screenCenter = Camera.current.ViewportToScreenPoint(center);
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            _placementPoseIsValid = hits.Count > 0;
            if (_placementPoseIsValid)
            {
                _placementPose = hits[0].pose;
            }
        }
    }
}
