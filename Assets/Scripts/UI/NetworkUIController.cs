// SPDX-FileCopyrightText: Copyright 2023 Reality Design Lab <dev@reality.design>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;
using HoloKit;

namespace HoloKit.ColocatedMultiplayerBoilerplate
{
    public class NetworkUIController : MonoBehaviour
    {
        [SerializeField] GameObject m_StartHostButton;

        [SerializeField] GameObject m_StartClientButton;

        [SerializeField] GameObject m_ShutdownButton;

        [SerializeField] GameObject m_FireButton;

        public UnityEvent OnBeforeHostStarted;

        public UnityEvent OnHostStarted;

        public UnityEvent OnClientStarted;

        public UnityEvent OnShutdown;

        public UnityEvent<bool> OnVisibilityChanged;

        private void Start()
        {
            FindObjectOfType<HoloKitCameraManager>().OnScreenRenderModeChanged += OnScreenRenderModeChanged;
        }

        private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
        {
            gameObject.SetActive(renderMode == ScreenRenderMode.Mono);
            OnVisibilityChanged?.Invoke(renderMode == ScreenRenderMode.Mono);
        }

        private void Update()
        {
            if (NetworkManager.Singleton.IsConnectedClient)
            {
                m_FireButton.SetActive(true);
            }
            else
            {
                m_FireButton.SetActive(false);
            }
        }

        public void StartHost()
        {
            OnBeforeHostStarted?.Invoke();
            NetworkManager.Singleton.StartHost();
            OnHostStarted?.Invoke();

            m_StartHostButton.SetActive(false);
            m_StartClientButton.SetActive(false);
            m_ShutdownButton.SetActive(true);
        }

        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
            OnClientStarted?.Invoke();

            m_StartHostButton.SetActive(false);
            m_StartClientButton.SetActive(false);
            m_ShutdownButton.SetActive(true);
        }

        public void Shutdown()
        {
            NetworkManager.Singleton.Shutdown();
            OnShutdown?.Invoke();

            m_StartHostButton.SetActive(true);
            m_StartClientButton.SetActive(true);
            m_ShutdownButton.SetActive(false);
        }
    }
}
