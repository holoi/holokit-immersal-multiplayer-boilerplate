// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode;
using Immersal.AR;
using HoloInteractive.XR.HoloKit;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    /// <summary>
    /// We use this script to synchronize device pose with Immersal SDK.
    /// Immersal SDK moves the ARSpace instead of the camera to achieve AR map relocalization.
    /// Thus, we need to calculate the player pose according to the ARSpace pose in the reverse order.
    /// </summary>
    [RequireComponent(typeof(HoloKitMarkManager))]
    public class PlayerPoseSynchronizer : NetworkBehaviour
    {
        [SerializeField] private Vector3 m_Offset = new(0f, 0f, 0.5f);

        [SerializeField] private float m_PositionLerpSpeed = 5f;

        [SerializeField] private float m_RotationLerpSpeed = 5f;

        private ARSpace m_ARSpace;

        private Transform m_CenterEyePose;

        private NetworkVariable<Vector3> m_RelativePosition = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        private NetworkVariable<Quaternion> m_RelativeRotation = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                //transform.SetParent(FindObjectOfType<ARMap>().transform);
                m_CenterEyePose = FindObjectOfType<HoloKitCameraManager>().CenterEyePose;
            }
            m_ARSpace = FindObjectOfType<ARSpace>();
        }

        private void Update()
        {
            if (IsOwner)
            {
                m_RelativePosition.Value = m_ARSpace.transform.InverseTransformPoint(m_CenterEyePose.position + m_CenterEyePose.TransformVector(m_Offset));
                m_RelativeRotation.Value = Quaternion.Inverse(m_ARSpace.transform.rotation) * m_CenterEyePose.rotation;
            }
        }

        private void LateUpdate()
        {
            Vector3 targetPosition = m_ARSpace.transform.TransformPoint(m_RelativePosition.Value);
            Quaternion targetRotation = m_ARSpace.transform.rotation * m_RelativeRotation.Value;

            // Interpolate the position
            transform.position = Vector3.Lerp(transform.position, targetPosition, m_PositionLerpSpeed * Time.deltaTime);
            // Interpolate the rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_RotationLerpSpeed * Time.deltaTime);
        }
    }
}
