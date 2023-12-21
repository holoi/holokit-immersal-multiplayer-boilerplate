// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode;
using HoloInteractive.XR.HoloKit;
using Immersal.AR;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    public enum RelocalizationMode
    {
        ImageTrackingRelocalizatioin = 0,
        Immersal = 1
    }

    public class NetworkBulletManager : NetworkBehaviour
    {
        [SerializeField] private RelocalizationMode m_RelocalizationMode;

        [SerializeField] private NetworkBulletController m_BulletPrefab;

        [SerializeField] private Vector3 m_SpawnOffset = new(0f, 0f, 0.3f);

        private Transform m_CenterEyePose;

        private ARSpace m_ARSpace;

        private void Start()
        {
            m_CenterEyePose = FindFirstObjectByType<HoloKitCameraManager>().CenterEyePose;

            if (m_RelocalizationMode == RelocalizationMode.Immersal)
            {
                m_ARSpace = FindFirstObjectByType<ARSpace>();
            }
        }

        public void SpawnBullet()
        {
            SpawnBulletServerRpc(m_CenterEyePose.position, m_CenterEyePose.rotation);
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpawnBulletServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams serverRpcParams = default)
        {
            if (m_RelocalizationMode == RelocalizationMode.ImageTrackingRelocalizatioin)
            {
                var bullet = Instantiate(m_BulletPrefab, position + rotation * m_SpawnOffset, rotation);
                bullet.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
                return;
            }

            if (m_RelocalizationMode == RelocalizationMode.Immersal)
            {
                Vector3 absolutePosition = position + rotation * m_SpawnOffset;
                Quaternion absoluteRotation = rotation;
                Vector3 relativePosition = m_ARSpace.transform.InverseTransformPoint(absolutePosition);
                Quaternion relativeRotation = Quaternion.Inverse(m_ARSpace.transform.rotation) * absoluteRotation;

                var bullet = Instantiate(m_BulletPrefab, m_ARSpace.transform);
                bullet.transform.localPosition = relativePosition;
                bullet.transform.localRotation = relativeRotation;
                bullet.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
                return;
            }
        }
    }
}
