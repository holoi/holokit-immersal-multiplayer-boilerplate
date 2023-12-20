// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode;
using HoloInteractive.XR.HoloKit;

public class NetworkBulletManager : NetworkBehaviour
{
    [SerializeField] private NetworkBulletController m_BulletPrefab;

    [SerializeField] private Vector3 m_SpawnOffset = new(0f, 0f, 0.3f);

    private Transform m_CenterEyePose;

    private void Start()
    {
        m_CenterEyePose = FindFirstObjectByType<HoloKitCameraManager>().CenterEyePose;
    }

    public void SpawnBullet()
    {
        SpawnBulletServerRpc(m_CenterEyePose.position, m_CenterEyePose.rotation);
    }

    [ServerRpc]
    public void SpawnBulletServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams serverRpcParams = default)
    {
        var bullet = Instantiate(m_BulletPrefab, position + rotation * m_SpawnOffset, rotation);
        bullet.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
    }
}
