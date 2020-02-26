using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace User_Interface
{
    public class ConstantInformation : MonoBehaviour
    {
        [SerializeField] private Text text;

        [TextArea]
        [SerializeField] private List<string> information = new List<string>();

        private void Start()
        {
            SetInformation(0);
        }

        public void SetInformation(int id)
        {
            text.text = information[id];
        }
    }
}
