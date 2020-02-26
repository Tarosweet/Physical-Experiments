using System.Linq;
using Systems.ExperimentReload;
using UnityEngine;

namespace Experiment_6.Scripts
{
    public class RigidbodySceneReloader : SceneGameObjectsReload
    {
        public override void Reload()
        {
            Load();
            PutRigidbodysToSleep();
        }

        private void PutRigidbodysToSleep()
        {
            foreach (var rb in transforms.Select(transform => transform.GetComponent<Rigidbody>()))
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.isKinematic = false;
            }
        }
    }
}
