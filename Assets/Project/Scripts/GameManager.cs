using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  public delegate void GameStateChangedEventHandler(GameState newState);
  public event GameStateChangedEventHandler GameStateChanged;

  public enum GameState
  {
    Menu,
    Gaming,
    LevelPreparation,
    GameOver,
  }

  public GameState currentState;

  private void Awake()
  {
    if (!Instance)
    {
      Instance = this;
    }
  }

  private void Start()
  {
    GameStateChanged += OnGameStateChanged;
    ChangeState(GameState.Menu);
  }

  private void OnDestroy()
  {
    GameStateChanged -= OnGameStateChanged;
  }

  public void ChangeState(GameState newState)
  {
    currentState = newState;
    GameStateChanged?.Invoke(newState);
  }

  public void OnGameStateChanged(GameState gameState)
  {
    switch (gameState)
    {
      case GameState.Menu:
        print("Menu");
        Menu.Instance.EnablePanel("menu");
        break;
      case GameState.Gaming:
        print("Gaming");
        Menu.Instance.EnablePanel("gaming");
        break;
      case GameState.LevelPreparation:
        print("Level Preparation");
        Unit.KillAll();
        Menu.Instance.EnablePanel("levelPreparation");
        break;
      case GameState.GameOver:
        print("Game Over");
        Unit.KillAll();
        Menu.Instance.EnablePanel("gameOver");
        break;
      default:
        break;
    }
  }
}
