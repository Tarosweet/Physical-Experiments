using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Experiment_6.Scripts
{
    public class DistanceMarkerController : MonoBehaviour
    {
        [Serializable]
        private class DistanceMarkerSlidersData
        {
            public DistanceMarker distanceMarker;
            public Image handle;
            public Image fill;
            public GameObject slider;

            public void PaintSlider(Color color)
            {
                fill.color = color;
                handle.color = color;
            }

            public void SetActiveAll(bool value)
            {
                handle.gameObject.SetActive(value);
                fill.gameObject.SetActive(value);
                slider.gameObject.SetActive(value);
            }
        }

        [SerializeField] private List<DistanceMarkerSlidersData> markers = new List<DistanceMarkerSlidersData>();
        
        [SerializeField] private List<Color> colors = new List<Color>();

        private readonly Dictionary<int, DistanceMarker> _distanceMarkersById = new Dictionary<int, DistanceMarker>();

        private int _currentId;
        
        public void OnBarrierSelected(int id)
        {
            if (!_distanceMarkersById.ContainsKey(id))
                _distanceMarkersById.Add(id,CreateMarker(id));

            EnableOneMarker(id);
            _currentId = id;
        }

        public void ShowCurrentMarker()
        {
            markers[_currentId].SetActiveAll(true);
        }

        private void Start()
        {
            OnBarrierSelected(0);
        }

        private void Update()
        {
            SortBySliderValues();
        }

        private void SortBySliderValues()
        {
            var index = 0;
            foreach (var marker in _distanceMarkersById.OrderByDescending(value => value.Value.Distance))  
            {  
                markers[marker.Key].slider.transform.SetSiblingIndex(index);
                index++;
            }  
        }

        private DistanceMarker CreateMarker(int id)
        {
            markers[id].PaintSlider(colors[id]);

            return markers[id].distanceMarker;
        }

        private void EnableOneMarker(int id)
        {
            foreach (var marker in _distanceMarkersById)
            {
                marker.Value.isActive = marker.Key == id;
            }
        }
    }
}
