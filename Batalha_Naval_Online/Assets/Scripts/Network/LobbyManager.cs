using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LobbyManager : MonoBehaviour
{
    private Lobby currentLobby;

    private void Start()
    {
        InvokeRepeating("UpdateLobby", 0f, 5f); // Atualiza o lobby a cada 5 segundos
    }

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
                    { "GameMode", new DataObject(DataObject.VisibilityOptions.Public, "Classic") }
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
    }

    public async void UpdateLobby()
    {
        if (currentLobby != null)
        {
            try
            {
                currentLobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
                Debug.Log("Lobby atualizado!");
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError("Erro ao atualizar lobby: " + e);
            }
        }
    }

    public async void LeaveLobby()
    {
        if (currentLobby != null)
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, AuthenticationService.Instance.PlayerId);
                currentLobby = null;
                Debug.Log("Saiu do lobby!");
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError("Erro ao sair do lobby: " + e);
            }
        }
    }

    public async void StartGame()
    {
        if (currentLobby != null && currentLobby.Players.Count == 2) // Verifica se tem jogadores suficientes
        {
            try
            {
                await LobbyService.Instance.UpdateLobbyAsync(currentLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                    {
                        { "GameStarted", new DataObject(DataObject.VisibilityOptions.Public, "true") }
                    }
                });

                Debug.Log("Jogo iniciado!");
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError("Erro ao iniciar jogo: " + e);
            }
        }
    }
}
