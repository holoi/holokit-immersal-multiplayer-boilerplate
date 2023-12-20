// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode;
using HoloInteractive.XR.HoloKit;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Net.Sockets;
using TMPro;

public class NetworkUIController : MonoBehaviour
{
    [SerializeField] GameObject m_StartHostButton;

    [SerializeField] GameObject m_StartClientButton;

    [SerializeField] GameObject m_ShutdownButton;

    [SerializeField] GameObject m_FireButton;

    [SerializeField] GameObject m_IPInputField;

    [SerializeField] TransportSelector m_TransportSelector;

    [SerializeField] TMP_Text m_HostIPAddress;

    [SerializeField] GameObject m_Ping;

    [SerializeField] TMP_Text m_ConnectedPlayerCount;

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
            m_FireButton.SetActive(true);

            m_IPInputField.SetActive(false);

            m_TransportSelector.gameObject.SetActive(false);
            m_HostIPAddress.gameObject.SetActive(NetworkManager.Singleton.IsHost && m_TransportSelector.CurrentTransport == AvailableTransport.Router);
            m_Ping.SetActive(true);

            m_ConnectedPlayerCount.gameObject.SetActive(NetworkManager.Singleton.IsHost);
            if (m_ConnectedPlayerCount.gameObject.activeSelf)
                m_ConnectedPlayerCount.text = $"Connected Player Count: {NetworkManager.Singleton.ConnectedClients.Count}";
        }
        else
        {
            m_StartHostButton.SetActive(true);
            m_StartClientButton.SetActive(true);
            m_ShutdownButton.SetActive(false);
            m_FireButton.SetActive(false);

            m_IPInputField.SetActive(m_TransportSelector.CurrentTransport == AvailableTransport.Router);

            m_TransportSelector.gameObject.SetActive(true);
            m_HostIPAddress.gameObject.SetActive(false);
            m_Ping.SetActive(false);

            m_ConnectedPlayerCount.gameObject.SetActive(false);
        }
    }

    public void StartHost()
    {
        string localIPAddress = GetLocalIPAddress();
        //Debug.Log($"Local IP Address: {localIPAddress}");
        var unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        unityTransport.SetConnectionData(localIPAddress, (ushort)7777);
        unityTransport.ConnectionData.ServerListenAddress = "0.0.0.0";
        m_HostIPAddress.text = $"Host IP Address: {localIPAddress}";

        NetworkManager.Singleton.StartHost();
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
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
