// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using HoloInteractive.XR.HoloKit.iOS;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    public class TrackedImagePoseTransformer : MonoBehaviour
    {
        [SerializeField] private WorldOriginResetter m_WorldOriginResetter;

        public void OnTrackedImageStablized(Vector3 position, Quaternion rotation)
        {
            rotation = rotation * Quaternion.Euler(90f, 0f, 0f);

            var r = Matrix4x4.Rotate(rotation);
            var a = r.m00 + r.m22;
            var b = -r.m20 + r.m02;
            float thetaInDeg = Mathf.Atan2(b, a) / Mathf.Deg2Rad;

            m_WorldOriginResetter.ResetWorldOrigin(position, Quaternion.AngleAxis(thetaInDeg, Vector3.up));
        }
    }
}
