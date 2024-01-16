// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode;
using HoloInteractive.XR.HoloKit;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    [RequireComponent(typeof(HoloKitMarkManager))]
    public class PlayerPoseSynchronizer_ImageTrackingRelocalization : NetworkBehaviour
    {
        private Transform m_CenterEyePose;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                var holokitCameraManager = FindFirstObjectByType<HoloKitCameraManager>();
                if (holokitCameraManager == null)
                {
                    Debug.LogWarning("[PlayerPoseSynchronizer_ImageTrackingRelocalization] Failed to find HoloKitCameraManager in the scene");
                }
                m_CenterEyePose = holokitCameraManager.CenterEyePose;
            }
        }

        private void Update()
        {
            if (IsSpawned && IsOwner && m_CenterEyePose != null)
                transform.SetPositionAndRotation(m_CenterEyePose.position, m_CenterEyePose.rotation);
        }
    }
}
