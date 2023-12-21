// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using TMPro;
using Unity.Netcode;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    public class NetworkStatsController : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_StatusText;

        [SerializeField] private TMP_Text m_PingText;

        [SerializeField] private TMP_Text m_ConnectedDeviceCountText;

        public void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;

            m_ConnectedDeviceCountText.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
                NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
            }
        }

        public void OnHostStarted()
        {
            m_StatusText.text = "Status: Hosting";
            m_ConnectedDeviceCountText.text = "Connected Device Count: 1";
            m_ConnectedDeviceCountText.gameObject.SetActive(true);
        }

        public void OnClientStarted()
        {
            m_StatusText.text = "Status: Connecting";
        }

        public void OnShutdown()
        {
            m_StatusText.text = "Status: None";
            m_PingText.text = "0ms";
            m_ConnectedDeviceCountText.gameObject.SetActive(false);
        }

        public void OnVisibilityChanged(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void OnReceivedRtt(int rtt)
        {
            m_PingText.text = $"{rtt}ms";
        }

        private void OnClientConnected(ulong clientId)
        {
            if (NetworkManager.Singleton.IsHost)
                m_ConnectedDeviceCountText.text = $"Connected Device Count: {NetworkManager.Singleton.ConnectedClients.Count}";
            else
                m_StatusText.text = "Status: Connected";
        }

        private void OnClientDisconnect(ulong clientId)
        {
            if (NetworkManager.Singleton.IsHost)
                m_ConnectedDeviceCountText.text = $"Connected Device Count: {NetworkManager.Singleton.ConnectedClients.Count}";
        }
    }
}
