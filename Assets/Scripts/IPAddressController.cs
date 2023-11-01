using UnityEngine;
using Unity.Netcode.Transports.UTP;

public class IPAddressController : MonoBehaviour
{
    [SerializeField] private UnityTransport m_UnityTransport;

    public void OnIPAddressChanged(string ipAddress)
    {
        m_UnityTransport.SetConnectionData(ipAddress, (ushort)7777);
    }
}
