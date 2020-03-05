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

        public Pose PlacementPose
        {
            get => _placementPose;
            set => _placementPose = value;
        }

        public bool placementPoseIsValid { get; private set; }
        
        private Vector3 center = new Vector3(0.5f,0.5f);
        private Pose _placementPose;


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
            placementIndicator.SetActive(placementPoseIsValid);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }

        private void UpdatePlacementPose()
        {
            var screenCenter = Camera.current.ViewportToScreenPoint(center);
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                PlacementPose = hits[0].pose;

                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
    }
}
