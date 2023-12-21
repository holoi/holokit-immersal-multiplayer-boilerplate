// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using Unity.Netcode;
using TMPro;
using HoloInteractive.XR.ImageTrackingRelocalization.iOS;

namespace HoloInteractive.XR.MultiplayerARBoilerplates
{
    public class ImageTrackingRelocalizationUIController : MonoBehaviour
    {
        [SerializeField] private GameObject m_ToggleMarkerButton;

        [SerializeField] private GameObject m_AlignmentMarkerPanel;

        [SerializeField] private TMP_Text m_StatusText;

        private NetworkImageTrackingStablizer m_NetworkImageTrackingStablizer; 

        private void Start()
        {
            m_ToggleMarkerButton.SetActive(false);
            m_AlignmentMarkerPanel.SetActive(false);
            m_StatusText.gameObject.SetActive(false);

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }

        private void OnDestroy()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                m_StatusText.gameObject.SetActive(true);
                m_StatusText.text = "Syncing Status: Syncing timestamp";
            }
        }

        public void OnTimestampSynced()
        {
            m_StatusText.text = "Syncing Status: Tracking marker";
        }

        public void OnPoseSynced()
        {
            m_AlignmentMarkerPanel.SetActive(true);
            m_StatusText.text = "Syncing Status: Validating pose";
        }

        public void OnAlignmentMarkerAccepted()
        {
            m_AlignmentMarkerPanel.SetActive(false);
        }

        public void OnAlignmentMarkerDenied()
        {
            m_AlignmentMarkerPanel.SetActive(false);
            m_StatusText.text = "Syncing Status: Tracking marker";
        }

        public void OnHostStarted()
        {
            m_ToggleMarkerButton.SetActive(true);
        }

        public void OnShutdown()
        {
            m_ToggleMarkerButton.SetActive(false);
            m_AlignmentMarkerPanel.SetActive(false);
            m_StatusText.gameObject.SetActive(false);
        }

        public void OnVisibilityChanged(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void ToggleMarker()
        {
            if (m_NetworkImageTrackingStablizer.IsDisplayingMarker)
                m_NetworkImageTrackingStablizer.StopDisplayingMarker();
            else
                m_NetworkImageTrackingStablizer.StartDisplayingMarker();
        }

        public void AcceptMarker()
        {
            m_NetworkImageTrackingStablizer.AcceptAlignmentMarker();
        }

        public void DenyMarker()
        {
            m_NetworkImageTrackingStablizer.DenyAlignmentMarker();
        }
    }
}
