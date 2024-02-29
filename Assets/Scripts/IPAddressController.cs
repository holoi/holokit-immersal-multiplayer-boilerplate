// SPDX-FileCopyrightText: Copyright 2023 Reality Design Lab <dev@reality.design>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode.Transports.UTP;

namespace HoloKit.ColocatedMultiplayerBoilerplate
{
    public class IPAddressController : MonoBehaviour
    {
        [SerializeField] private UnityTransport m_UnityTransport;

        public void OnIPAddressChanged(string ipAddress)
        {
            m_UnityTransport.SetConnectionData(ipAddress, (ushort)7777);
        }
    }
}
