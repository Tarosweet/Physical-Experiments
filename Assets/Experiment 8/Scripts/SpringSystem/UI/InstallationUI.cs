using UnityEngine;


public abstract class InstallationUI : MonoBehaviour
{

    [SerializeField] private GameObject UI;

    public void SetActive(bool value)
    {
        UI.SetActive(value);
    }

    public abstract void Initialize(ref int index);

}