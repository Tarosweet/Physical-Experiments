using UnityEngine;

namespace AR
{
    public class MakeAppearContentSetter : MonoBehaviour
    {
        [SerializeField] private Transform content;
        private void Start()
        {
            MakeAppearOnPlane.Instance.content = content;
        }
    }
}
