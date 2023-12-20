// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using System.Collections;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PingManager : NetworkBehaviour
{
    [SerializeField] private float m_Interval = 1f;

    [SerializeField] private TMP_Text m_PingText;

    public override void OnNetworkSpawn()
    {
        if (!IsHost)
        {
            StartCoroutine(StartPing());
        }
    }

    private IEnumerator StartPing()
    {
        while (true)
        {
            PingServerRpc(Time.time);
            yield return new WaitForSeconds(m_Interval);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void PingServerRpc(float timestamp, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };
        PongClientRpc(timestamp, clientRpcParams);
    }

    [ClientRpc]
    private void PongClientRpc(float timestamp, ClientRpcParams clientRpcParams = default)
    {
        float rtt = Mathf.FloorToInt((Time.time - timestamp) * 1000f);
        m_PingText.text = $"Ping: {rtt}ms";
    }
}
