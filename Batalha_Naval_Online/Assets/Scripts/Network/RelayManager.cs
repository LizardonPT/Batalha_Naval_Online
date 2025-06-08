using UnityEngine;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using System.Threading.Tasks;

public class RelayManager : MonoBehaviour
{
    public static string joinCode; // Código partilhado com outros jogadores

    public static async Task<string> CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1); // max 1 client

            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Relay Join Code: " + joinCode);

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            transport.SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData,
                (byte[])System.Text.Encoding.UTF8.GetBytes("dtls")
            );

            NetworkManager.Singleton.StartHost(); // Iniciar como host

            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Erro ao criar Relay: " + e);
            return null;
        }
    }

    public static async Task JoinRelay(string joinCode)
{
    try
    {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        Debug.Log("Ligado ao Relay com o código: " + joinCode);

        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        transport.SetRelayServerData(
            allocation.RelayServer.IpV4,
            (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes,
            allocation.Key,
            allocation.ConnectionData,
            allocation.HostConnectionData, // apenas no join
            true 
        );

        NetworkManager.Singleton.StartClient(); // Iniciar como client
    }
    catch (RelayServiceException e)
    {
        Debug.LogError("Erro ao ligar ao Relay: " + e);
    }
}
}
