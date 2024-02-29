// SPDX-FileCopyrightText: Copyright 2023 Reality Design Lab <dev@reality.design>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;

namespace HoloKit.ColocatedMultiplayerBoilerplate
{
    public class HoloKitMarkManager : MonoBehaviour
    {
        [SerializeField] private HoloKitMarkController m_HoloKitMarkPrefab;

        private HoloKitMarkController m_HoloKitMark;

        private void Start()
        {
            m_HoloKitMark = Instantiate(m_HoloKitMarkPrefab);
            m_HoloKitMark.PlayerPoseSynchronizer = transform;
        }

        public void OnDestroy()
        {
            if (m_HoloKitMark)
                Destroy(m_HoloKitMark.gameObject);
        }
    }
}
