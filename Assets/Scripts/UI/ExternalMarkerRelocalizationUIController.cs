// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using UnityEngine.UI;
using HoloInteractive.XR.ImageTrackingRelocalization;
using HoloInteractive.XR.HoloKit;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    public class ExternalMarkerRelocalizationUIController : MonoBehaviour
    {
        [SerializeField] private ImageTrackingStablizer m_ImageTrackingStablizer;

        [SerializeField] private GameObject m_MonoButton;

        [SerializeField] private Text m_MonoButtonText;

        [SerializeField] private GameObject m_StarButton;

        [SerializeField] private Text m_StarButtonText;

        private HoloKitCameraManager m_HoloKitCameraManager;

        private void Start()
        {
            m_HoloKitCameraManager = FindFirstObjectByType<HoloKitCameraManager>();
            m_HoloKitCameraManager.OnScreenRenderModeChanged += OnScreenRenderModeChanged;
            m_MonoButton.SetActive(true);
            m_StarButton.SetActive(false);
        }

        private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
        {
            if (renderMode == ScreenRenderMode.Mono)
            {
                m_MonoButton.SetActive(true);
                m_StarButton.SetActive(false);
            }
            else
            {
                m_MonoButton.SetActive(false);
                m_StarButton.SetActive(true);
            }
        }

        public void ToggleTrackingMarker()
        {
            m_ImageTrackingStablizer.IsRelocalizing = !m_ImageTrackingStablizer.IsRelocalizing;
        }

        private void Update()
        {
            if (m_ImageTrackingStablizer.IsRelocalizing)
            {
                m_MonoButtonText.text = "Stop Tracking";
                m_StarButtonText.text = "Stop Tracking";

            }
            else
            {
                m_MonoButtonText.text = "Start Tracking";
                m_StarButtonText.text = "Start Tracking";
            }
        }
    }
}
