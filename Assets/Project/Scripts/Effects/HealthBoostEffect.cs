using UnityEngine;

[CreateAssetMenu(fileName = "HealthBoost", menuName = "Effects/HealthBoost")]
public class HealthBoostEffect : Effect
{
  public HealthBoostEffect() : base("HealthBoost", 20f) { }

  public override void ApplyEffect(Unit unit)
  {
    base.ApplyEffect(unit);
    unit.hp += magnitude;
    Debug.Log("Health boost applied! " + unit.hp + " " + magnitude);
  }
}
