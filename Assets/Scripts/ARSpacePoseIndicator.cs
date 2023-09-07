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
