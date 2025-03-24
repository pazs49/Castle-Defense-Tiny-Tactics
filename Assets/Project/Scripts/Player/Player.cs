using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
  public static Player Instance;
  public float maxHp;
  public float baseHp;
  public GameObject[] units;
  public InputActionReference summonUnit;

  public float spawnTime = 1.5f;
  public bool canSpawn = true;

  public Slider hpSlider;
  public Slider spawnTimeSlider;

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
    Initialize();
  }

  private void Update()
  {
    // Update spawn time slider
    if (!canSpawn)
    {
      spawnTime -= Time.deltaTime;
      if (spawnTime <= 0)
      {
        canSpawn = true;
        spawnTime = 2f;
      }
    }
    spawnTimeSlider.value = spawnTime;
  }

  private void OnDestroy()
  {
    GameManager.Instance.GameStateChanged -= OnGameStateChanged;
  }

  private void Initialize()
  {
    baseHp = maxHp;
    hpSlider.maxValue = baseHp;
    spawnTimeSlider.maxValue = spawnTime;
    UpdateUI();
  }

  private void OnGameStateChanged(GameManager.GameState gameState)
  {
    switch (gameState)
    {
      case GameManager.GameState.Gaming:
        Initialize();
        summonUnit.action.performed += SummonUnit;
        break;
      default:
        summonUnit.action.performed -= SummonUnit;
        break;
    }
  }

  private void InstantiateUnit(GameObject unit)
  {
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Instantiate(unit, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
  }

  private void SummonUnit(InputAction.CallbackContext context)
  {
    var pressedButton = ((KeyControl)context.control).keyCode.ToString();
    switch (pressedButton)
    {
      case "Q":
        if (canSpawn)
        {
          InstantiateUnit(units[0]);
          canSpawn = false;
        }
        break;
      case "W":
        if (canSpawn)
        {
          InstantiateUnit(units[1]);
          canSpawn = false;
        }
        break;
      default:
        break;
    }
  }

  public void TakeDamage(float damage)
  {
    StartCoroutine(Shake(0.5f, 0.1f));
    baseHp -= damage;
    UpdateUI();
    if (baseHp <= 0)
    {
      Die();
    }
  }

  private void Die()
  {
    GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
  }

  private void UpdateUI()
  {
    hpSlider.value = baseHp;
  }

  public IEnumerator Shake(float duration, float magnitude)
  {
    Vector3 originalPosition = Camera.main.transform.localPosition;

    float elapsed = 0.0f;

    while (elapsed < duration)
    {
      float x = Random.Range(-1f, 1f) * magnitude;
      float y = Random.Range(-1f, 1f) * magnitude;

      Camera.main.transform.localPosition = new Vector3(x, y, originalPosition.z);

      elapsed += Time.deltaTime;

      yield return null;
    }

    Camera.main.transform.localPosition = originalPosition;
  }
}
