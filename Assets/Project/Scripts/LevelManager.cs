using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public static LevelManager Instance;

  public GameObject enemySpawner;
  public int currentLevel = 1;

  private void Awake()
  {
    if (!Instance)
    {
      Instance = this;
    }
    else
    {
      Destroy(this);
    }
  }

  private void Start()
  {
    GameManager.Instance.GameStateChanged += OnGameStateChanged;
  }

  private void OnGameStateChanged(GameManager.GameState gameState)
  {
    switch (gameState)
    {
      case GameManager.GameState.LevelPreparation:
        enemySpawner.SetActive(false);
        currentLevel++;
        break;
      case GameManager.GameState.Gaming:
        enemySpawner.SetActive(true);
        break;
      default:
        currentLevel = 1;
        enemySpawner.SetActive(false);
        break;
    }
  }
}
