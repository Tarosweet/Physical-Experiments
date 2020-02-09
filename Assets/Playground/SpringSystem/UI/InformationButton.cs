using UnityEngine;


public class InformationButton : MonoBehaviour
{
    [SerializeField] private GameObject informationGameObject;

    public void ActiveSelf()
    {
        informationGameObject.SetActive(!informationGameObject.activeSelf);
    }
}