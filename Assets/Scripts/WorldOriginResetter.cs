// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using HoloInteractive.XR.ImageTrackingRelocalization.iOS;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    public class WorldOriginResetter : MonoBehaviour
    {
        private ARKitNativeProvider m_ARKitNativeProvider;

        private void Start()
        {
            m_ARKitNativeProvider = new();
        }

        private void OnDestroy()
        {
            m_ARKitNativeProvider.Dispose();
        }

        public void OnTrackedImageStablized(Vector3 position, Quaternion rotation)
        {
            Quaternion finalRotation = Quaternion.Euler(0f, (rotation * Quaternion.Euler(90f, 0f, 0f)).eulerAngles.y, 0f);
            m_ARKitNativeProvider.ResetOrigin(position, finalRotation);
        }
    }
}
