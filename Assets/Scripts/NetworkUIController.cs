using UnityEngine;
using Unity.Netcode;
using HoloInteractive.XR.HoloKit;
using System;

public class NetworkUIController : MonoBehaviour
{
    [SerializeField] GameObject m_StartHostButton;

    [SerializeField] GameObject m_StartClientButton;

    [SerializeField] GameObject m_ShutdownButton;

    private void Start()
    {
        FindObjectOfType<HoloKitCameraManager>().OnScreenRenderModeChanged += OnScreenRenderModeChanged;
    }

    private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
    {
        gameObject.SetActive(renderMode == ScreenRenderMode.Mono);
    }

    private void Update()
    {
        if (NetworkManager.Singleton.IsConnectedClient)
        {
            m_StartHostButton.SetActive(false);
            m_StartClientButton.SetActive(false);
            m_ShutdownButton.SetActive(true);
        }
        else
        {
            m_StartHostButton.SetActive(true);
            m_StartClientButton.SetActive(true);
            m_ShutdownButton.SetActive(false);
        }
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void Shutdown()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
