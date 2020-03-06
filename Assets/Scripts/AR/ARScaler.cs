using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace AR
{
    public class ARScaler : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Text text;

        [SerializeField] private ARSessionOrigin sessionOrigin;

        [SerializeField] private MakeAppearOnPlane makeAppearOnPlane;

        [SerializeField] private float minScaleFactor = .1f, maxScaleFactor = 10f;

        private void Start()
        {
            Initialize();
        }

        private float Scale
        {
            get => sessionOrigin.transform.localScale.x;
            set => sessionOrigin.transform.localScale = Vector3.one * value;
        }

        public void OnSliderValueChanged()
        {
            Scale = slider.value;
            text.text = Scale.ToString();
        }

        private void Initialize()
        {
            slider.minValue = minScaleFactor;
            slider.maxValue = maxScaleFactor;
            
            makeAppearOnPlane.MakeAppear();
        }
    }
}
