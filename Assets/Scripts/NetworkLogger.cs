using UnityEngine;
using Unity.Netcode;

public class NetworkLogger : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"OnClientConnected {clientId}");
    }

    private void OnClientDisconnect(ulong clientId)
    {
        Debug.Log($"OnClientDisconnect {clientId}");
    }
}
