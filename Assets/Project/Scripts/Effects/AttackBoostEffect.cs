using UnityEngine;

[CreateAssetMenu(fileName = "AttackBoost", menuName = "Effects/AttackBoost")]
public class AttackBoostEffect : Effect
{
  public AttackBoostEffect() : base("AttackBoost", 5f) { }

  public override void ApplyEffect(Unit unit)
  {
    base.ApplyEffect(unit);
    unit.attack += magnitude;
    Debug.Log("Attack boost applied! " + unit.attack + " " + magnitude);
  }
}
