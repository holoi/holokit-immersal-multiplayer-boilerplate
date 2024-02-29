// SPDX-FileCopyrightText: Copyright 2023 Reality Design Lab <dev@reality.design>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-FileContributor: Botao Amber Hu <botao.a.hu@gmail.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode;
using HoloKit;

namespace HoloKit.ColocatedMultiplayerBoilerplate
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
