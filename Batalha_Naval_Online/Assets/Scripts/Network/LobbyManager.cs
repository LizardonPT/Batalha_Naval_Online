using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LobbyManager : MonoBehaviour
{
    private Lobby currentLobby;

    public async void CreateLobby()
    {
        string lobbyName = "BatalhaNaval";
        int maxPlayers = 2;

        try
        {
            CreateLobbyOptions options = new CreateLobbyOptions
            {
                IsPrivate = false,
                Player = new Player(id: AuthenticationService.Instance.PlayerId),
                Data = new Dictionary<string, DataObject>
                {
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, "Classic") },
                    { "RelayCode", new DataObject(DataObject.VisibilityOptions.Member, RelayManager.joinCode) }
                }
            };

            currentLobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);
            Debug.Log("Lobby criado com sucesso! ID: " + currentLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError("Erro ao criar lobby: " + e);
        }
    }

    public async void QuickJoinLobby()
    {
        try
        {
            currentLobby = await LobbyService.Instance.QuickJoinLobbyAsync();
            Debug.Log("Entrou no lobby com sucesso! ID: " + currentLobby.Id);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError("Erro ao juntar-se ao lobby: " + e);
        }

        string relayCode = currentLobby.Data["RelayCode"].Value;
        await RelayManager.JoinRelay(relayCode);
    }
}
