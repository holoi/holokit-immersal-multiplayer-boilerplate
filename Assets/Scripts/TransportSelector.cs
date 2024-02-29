// SPDX-FileCopyrightText: Copyright 2023 Reality Design Lab <dev@reality.design>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Netcode.Transports.MultipeerConnectivity;
using Unity.Netcode.Transports.UTP;

namespace HoloKit.ColocatedMultiplayerBoilerplate
{
    public enum AvailableTransport
    {
        AirDrop = 0,
        Router = 1
    }

    public class TransportSelector : MonoBehaviour
    {
        [SerializeField] private MultipeerConnectivityTransport m_AirDropTransport;

        [SerializeField] private UnityTransport m_UnityTransport;

        [SerializeField] private Toggle m_AirDropToggle;

        [SerializeField] private Toggle m_RouterToggle;

        [SerializeField] private AvailableTransport m_DefaultTransport = AvailableTransport.AirDrop;

        public AvailableTransport CurrentTransport => m_CurrentTransport;

        private bool m_IsToggling = false;

        private AvailableTransport m_CurrentTransport;

        private void Start()
        {
            OnAirDropToggled(true);
        }

        public void OnAirDropToggled(bool value)
        {
            if (m_IsToggling) return;
            if (!value)
            {
                m_IsToggling = true;
                m_AirDropToggle.isOn = true;
                m_IsToggling = false;
            }

            m_IsToggling = true;
            NetworkManager.Singleton.NetworkConfig.NetworkTransport = m_AirDropTransport;
            m_CurrentTransport = AvailableTransport.AirDrop;
            m_RouterToggle.isOn = false;
            m_IsToggling = false;
        }

        public void OnRouterToggled(bool value)
        {
            if (m_IsToggling) return;
            if (!value)
            {
                m_IsToggling = true;
                m_RouterToggle.isOn = true;
                m_IsToggling = false;
            }

            m_IsToggling = true;
            NetworkManager.Singleton.NetworkConfig.NetworkTransport = m_UnityTransport;
            m_CurrentTransport = AvailableTransport.Router;
            m_AirDropToggle.isOn = false;
            m_IsToggling = false;
        }
    }
}
