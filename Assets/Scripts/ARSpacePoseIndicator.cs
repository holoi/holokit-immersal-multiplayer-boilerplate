// SPDX-FileCopyrightText: Copyright 2023 Holo Interactive <dev@holoi.com>
// SPDX-FileContributor: Yuchen Zhang <yuchenz27@outlook.com>
// SPDX-License-Identifier: MIT

using UnityEngine;
using TMPro;

public class ARSpacePoseIndicator : MonoBehaviour
{
    [SerializeField] Transform m_ARSpace;

    [SerializeField] TMP_Text m_PoseText;

    private void Update()
    {
        m_PoseText.text = $"Position: {m_ARSpace.position:F4}\nRotation: {m_ARSpace.rotation:F4}";
    }
}
