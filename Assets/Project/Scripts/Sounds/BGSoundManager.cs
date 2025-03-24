using UnityEngine;

public class BGSoundManager : MonoBehaviour
{
  public AudioClip[] bgSounds;

  private AudioSource audioSource;

  private void Awake()
  {
    audioSource = GetComponent<AudioSource>();
  }

  private void Start()
  {
    GameManager.Instance.GameStateChanged += OnGameStateChanged;
  }

  private void OnGameStateChanged(GameManager.GameState gameState)
  {
    switch (gameState)
    {
      case GameManager.GameState.Menu:
        audioSource.resource = bgSounds[0];
        audioSource.Play();
        break;
      case GameManager.GameState.Gaming:
        audioSource.resource = bgSounds[1];
        audioSource.Play();
        break;
      case GameManager.GameState.LevelPreparation:
        audioSource.resource = bgSounds[2];
        audioSource.Play();
        break;
      case GameManager.GameState.GameOver:
        // audioSource.resource = bgSounds[3];
        // audioSource.Play();
        break;
      default:
        break;
    }
  }
}
