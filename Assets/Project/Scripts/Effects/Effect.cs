
using UnityEngine;

public class Effect : ScriptableObject
{
  public string effectName;
  public float magnitude;

  public Effect(string name, float magnitude)
  {
    this.effectName = name;
    this.magnitude = magnitude;
  }

  public virtual void ApplyEffect(Unit unit)
  {
  }
}
