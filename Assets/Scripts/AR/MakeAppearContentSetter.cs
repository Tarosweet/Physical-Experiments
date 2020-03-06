using UnityEngine;

namespace AR
{
    public class MakeAppearContentSetter : MonoBehaviour
    {
        [SerializeField] private Transform content;
        private void Start()
        {
            if (MakeAppearOnPlane.Instance == null)
                return;
            
            MakeAppearOnPlane.Instance.content = content;
        }
    }
}
