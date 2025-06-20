using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState CurrentGameState;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.BoardSetup);
    }

    public void UpdateGameState(GameState newState)
    {
        CurrentGameState = newState;
        Debug.Log($"Game state changed to: {CurrentGameState}");

        switch (newState)
        {
            case GameState.BoardSetup:
                // Initialize board setup logic
                GridManager.Instance.GenerateGrid();
                ShipManager.Instance.InitializeShips();
                break;
            case GameState.Player1Turn:
                // Logic for Player 1's turn
                break;
            case GameState.Player2Turn:
                // Logic for Player 2's turn
                break;
            case GameState.VictoryScreen:
                // Show victory screen logic
                break;
            case GameState.LooseScreen:
                // Show loose screen logic
                break;
            default:
                Debug.LogWarning("Unhandled game state: " + newState);
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
    
    public enum GameState
    {
        BoardSetup,
        Player1Turn,
        Player2Turn,
        VictoryScreen,
        LooseScreen
    }
}
