using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
  public static Menu Instance;

  public GameObject menuPanel;
  public GameObject gamingPanel;
  public GameObject levelPreparationPanel;
  public GameObject gameOverPanel;
  public TextMeshProUGUI currentLevelText;

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

  public void StartGame()
  {
    GameManager.Instance.ChangeState(GameManager.GameState.Gaming);
  }

  public void QuitGame()
  {
    GameManager.Instance.ChangeState(GameManager.GameState.Menu);
  }

  public void RestartGame()
  {
    GameManager.Instance.ChangeState(GameManager.GameState.Menu);
  }

  public void EnablePanel(string panelName)
  {
    menuPanel.SetActive(false);
    gamingPanel.SetActive(false);
    levelPreparationPanel.SetActive(false);
    gameOverPanel.SetActive(false);
    switch (panelName)
    {
      case "menu":
        menuPanel.SetActive(true);
        break;
      case "gaming":
        currentLevelText.text = "Level " + LevelManager.Instance.currentLevel;
        gamingPanel.SetActive(true);
        break;
      case "levelPreparation":
        levelPreparationPanel.SetActive(true);
        break;
      case "gameOver":
        gameOverPanel.SetActive(true);
        break;
    }
  }
}
