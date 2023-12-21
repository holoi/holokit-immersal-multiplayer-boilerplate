// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    public class TransportUIController : MonoBehaviour
    {
        [SerializeField] TransportSelector m_TransportSelector;

        [SerializeField] TMP_Text m_HostIPAddress;

        [SerializeField] GameObject m_IPInputField;

        private void Update()
        {
            if (NetworkManager.Singleton.IsConnectedClient)
            {
                m_IPInputField.SetActive(false);
                m_HostIPAddress.gameObject.SetActive(NetworkManager.Singleton.IsHost && m_TransportSelector.CurrentTransport == AvailableTransport.Router);
            }
            else
            {
                m_IPInputField.SetActive(m_TransportSelector.CurrentTransport == AvailableTransport.Router);
                m_HostIPAddress.gameObject.SetActive(false);
            }
        }

        public void OnBeforeHostStarted()
        {
            var unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            if (unityTransport != null)
            {
                string localIPAddress = GetLocalIPAddress();
                unityTransport.SetConnectionData(localIPAddress, (ushort)7777);
                unityTransport.ConnectionData.ServerListenAddress = "0.0.0.0";
                m_HostIPAddress.text = $"Host IP Address: {localIPAddress}";
            }
        }

        public void OnHostStarted()
        {
            m_TransportSelector.gameObject.SetActive(false);
        }

        public void OnClientStarted()
        {
            m_TransportSelector.gameObject.SetActive(false);
        }

        public void OnShutdown()
        {
            m_TransportSelector.gameObject.SetActive(true);
        }

        public void OnVisibilityChanged(bool visible)
        {
            gameObject.SetActive(visible);
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
    }
}
