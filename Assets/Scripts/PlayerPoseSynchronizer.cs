using UnityEngine;
using Unity.Netcode;
using Immersal.AR;
using HoloInteractive.XR.HoloKit;

public class PlayerPoseSynchronizer : NetworkBehaviour
{
    [SerializeField] private Vector3 m_Offset = new(0f, 0f, 0.5f);

    [SerializeField] private float m_PositionLerpSpeed = 5f;

    [SerializeField] private float m_RotationLerpSpeed = 5f;

    [SerializeField] private HoloKitMarkController m_HoloKitMarkPrefab;

    private ARSpace m_ARSpace;

    private Transform m_CenterEyePose;

    private NetworkVariable<Vector3> m_RelativePosition = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private NetworkVariable<Quaternion> m_RelativeRotation = new(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private HoloKitMarkController m_HoloKitMark;

    private void Start()
    {
        m_HoloKitMark = Instantiate(m_HoloKitMarkPrefab);
        m_HoloKitMark.PlayerPoseSynchronizer = transform;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        if (m_HoloKitMark)
            Destroy(m_HoloKitMark.gameObject);
    }

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
