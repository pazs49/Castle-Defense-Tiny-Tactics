using UnityEngine;

public class Swordman : Unit
{
  protected override void Awake()
  {
    base.Awake();
  }

  protected override void Start()
  {
    base.Start();
  }

  protected override void Update()
  {
    base.Update();
  }

  protected override void FixedUpdate()
  {
    base.FixedUpdate();
  }

  public override void TakeDamage(float damage)
  {
    int rand = Random.Range(0, 100);
    if (rand < 50)
    {
      print("Attack Blocked!");
      damage = 0;
    }

    base.TakeDamage(damage);
  }
}
