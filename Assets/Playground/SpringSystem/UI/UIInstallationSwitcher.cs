using UnityEngine;

public class UIInstallationSwitcher : MonoBehaviour
{
    [SerializeField] private Hook _hook;
    private void OnEnable()
    {
        _hook.onHook += Connected;
        _hook.onDisconnectHook += Disconnected;
    }

    private void OnDisable()
    {
        _hook.onHook -= Connected;
        _hook.onDisconnectHook -= Disconnected;
    }

    private void SetUI(JointsContainer container, bool value)
    {
        if (container.IsChainExist())
        {
            container.weightsChain.SetUI(value);
            return;
        }
        
        container.GetComponent<InstallationUI>().SetActive(value);
    }

    private void Connected(JointsContainer container)
    {
        SetUI(container,true);
    }

    private void Disconnected(JointsContainer container)
    {
        SetUI(container,false);
    }
}
