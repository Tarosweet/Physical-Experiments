using UnityEngine;
using UnityEngine.UI;


public class ScalesUI : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private Text text;

    public void EnableUI(float mass)
    {
        if (mass <= 0.00001)  //TODO refactor
        {
            UI.SetActive(false);
            return;
        }

        text.text = Mathf.Floor((mass / 0.102f)).ToString() + "H";
        UI.SetActive(true);
    }

    public void DisableUI()
    {
        UI.SetActive(false);
    }
}
