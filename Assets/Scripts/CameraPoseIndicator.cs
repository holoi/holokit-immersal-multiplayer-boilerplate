// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using TMPro;

public class CameraPoseIndicator : MonoBehaviour
{
    [SerializeField] Transform m_Camera;

    [SerializeField] TMP_Text m_CameraPoseText;

    private void Update()
    {
        m_CameraPoseText.text = $"Camera position: {m_Camera.position}\nCamera rotation: {m_Camera.rotation}";
    }
}
