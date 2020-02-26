using System.Collections.Generic;
using UnityEngine;

namespace Systems.ExperimentReload
{
    public class SceneGameObjectsReload : ExperimentReloader
    {
        private class TransformData
        {
            public readonly Vector3 Position;
            public readonly Quaternion Rotation;
            public readonly Vector3 Scale;

            public TransformData(Vector3 transformPosition, Quaternion transformRotation, Vector3 transformLocalScale)
            {
                Position = transformPosition;
                Rotation = transformRotation;
                Scale = transformLocalScale;
            }
        }
        
        [SerializeField] protected List<Transform> transforms = new List<Transform>();

        [SerializeField] private List<TransformData> transformsData = new List<TransformData>();
        
        public override void Reload()
        {
            Load();
        }
        
        private void Start()
        {
            Save();
        }

        private void Save()
        {
            foreach (var transform in transforms)
            {
                transformsData.Add(new TransformData(transform.position, transform.rotation,transform.localScale));
            }
        }

        protected void Load()
        {
            for (var index = 0; index < transformsData.Count; index++)
            {
                var transformData = transformsData[index];

                transforms[index].position = transformData.Position;
                transforms[index].rotation = transformData.Rotation;
                transforms[index].localScale = transformData.Scale;
            }
        }
    }
}
