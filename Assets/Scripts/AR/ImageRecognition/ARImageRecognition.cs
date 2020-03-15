using Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace AR.ImageRecognition
{
    // ReSharper disable once InconsistentNaming
    public class ARImageRecognition : MonoBehaviour
    {
        [SerializeField] private ARTrackedImageManager arTrackedImageManager;

        [SerializeField] private StringEvent onImageChanged;

        private void OnEnable()
        {
            arTrackedImageManager.trackedImagesChanged += OnImageChanged;
        }

        private void OnDisable()
        {
            arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
        }

        private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach (var trackedImage in args.added)
            {
                onImageChanged?.Invoke(trackedImage.name);
            }
        }
    }
}
