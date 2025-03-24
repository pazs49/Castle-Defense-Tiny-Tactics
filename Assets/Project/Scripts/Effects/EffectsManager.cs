using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
  public static EffectsManager Instance;
  public Effect[] effects;

  public List<Effect> currentEffects = new List<Effect>();

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    GameManager.Instance.GameStateChanged += OnGameStateChanged;
  }

  public void OnGameStateChanged(GameManager.GameState state)
  {
    switch (state)
    {
      case GameManager.GameState.Gaming:
        break;
    }
  }
  public void Effect1()
  {
    currentEffects.Add(effects[0]);
    GameManager.Instance.ChangeState(GameManager.GameState.Gaming);
  }
  public void Effect2()
  {
    currentEffects.Add(effects[1]);
    GameManager.Instance.ChangeState(GameManager.GameState.Gaming);
  }
}
