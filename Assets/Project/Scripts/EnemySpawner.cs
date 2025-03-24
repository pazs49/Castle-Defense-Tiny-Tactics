using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField]
  private GameObject[] enemyPrefabs;

  [SerializeField]
  private Transform[] spawnPoints;

  public int maxEnemies = 10;
  private float spawnTime;

  [SerializeField]
  private float spawnRate = 1f;

  private float checkTime;

  private void OnEnable()
  {
    Initialize(LevelManager.Instance.currentLevel);
  }
  private void Update()
  {
    SpawnEnemy();

    if (Time.time >= checkTime)
    {
      CheckIfAllEnemiesAreDeadThenEndLevel();
      checkTime = Time.time + 1f;
    }
  }

  private void Initialize(int currentLevel)
  {
    maxEnemies = (int)(((currentLevel * 2) + 10) * 2f);
    print("maxEnemies: " + maxEnemies);
    // maxEnemies = 5;
  }

  private void SpawnEnemy()
  {
    if (Time.time >= spawnTime && maxEnemies > 0)
    {
      maxEnemies--;
      GameObject unit = Instantiate(
          enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
          new Vector3(
              spawnPoints[0].position.x,
              Random.Range(spawnPoints[0].position.y, spawnPoints[1].position.y),
              0
          ),
          Quaternion.identity
      );
      unit.GetComponent<Unit>().isPlayer = false;
      spawnTime = Time.time + spawnRate;
    }
  }

  private void CheckIfAllEnemiesAreDeadThenEndLevel()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Unit").Where(e => !e.GetComponent<Unit>().isPlayer).ToArray();
    if (enemies.Length == 0 && Player.Instance.baseHp > 0)
    {
      GameManager.Instance.ChangeState(GameManager.GameState.LevelPreparation);
    }
  }
}
