using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
  public List<Effect> effects;
  public bool isPlayer;
  public float hp;

  public string attackSFXName;

  [SerializeField]
  protected float armor;

  public float attack;

  [SerializeField]
  protected float enemyDetectionRange;

  [SerializeField]
  protected float attackRange;

  [SerializeField]
  protected float attackSpeed;

  [SerializeField]
  protected float moveSpeed;

  private float attackTimer = 0f;

  protected Animator animator;
  protected Rigidbody2D rb;
  protected SpriteRenderer sr;
  protected Collider2D col;

  [SerializeField]
  protected bool isDead = false;

  [SerializeField]
  protected Unit target;

  protected virtual void OnTriggerEnter2D(Collider2D collision)
  {
    if (!isPlayer && collision.tag == "Base")
    {
      Player.Instance.TakeDamage(1f);
      Die();
    }
  }

  protected virtual void Awake()
  {
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    sr = GetComponent<SpriteRenderer>();
    col = GetComponent<Collider2D>();

    attackTimer = attackSpeed + .1f;
  }

  protected virtual void Start()
  {
    Initialize();
  }

  protected virtual void Update() { }

  protected virtual void FixedUpdate()
  {
    if (isDead)
      return;
    Act();
  }

  protected virtual void Initialize()
  {
    // Color discernment if player or enemy
    sr.color = isPlayer ? Color.white : new Color32(243, 168, 218, 255);

    if (isPlayer)
    {
      effects = EffectsManager.Instance.currentEffects;

      // Apply effects
      foreach (Effect effect in effects)
      {
        effect.ApplyEffect(this);
      }
    }
  }

  public void ChangeAnimationState(string state)
  {
    if (animator.GetCurrentAnimatorStateInfo(0).IsName(state))
      return;
    animator.Play(state);
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);

    Gizmos.color = Color.blue;
    Gizmos.DrawWireSphere(transform.position, attackRange);
  }

  Vector2 direction;

  protected virtual void Act()
  {
    if (target != null)
    {
      MoveToTarget();
    }
    else
    {
      SearchForTarget();
      Move();
    }
    direction = rb.linearVelocity.normalized;
    if (direction.x == 0)
      return;
    if (sr.flipX != (direction.x < 0))
      sr.flipX = !sr.flipX;
  }

  protected void SearchForTarget()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyDetectionRange);
    Unit closestTarget = null;
    float closestDistance = Mathf.Infinity;

    foreach (Collider2D hit in hits)
    {
      if (hit.tag != "Unit")
        continue;
      if (
          isPlayer && hit.transform.gameObject.GetComponent<Unit>().isPlayer
          || !isPlayer && !hit.transform.gameObject.GetComponent<Unit>().isPlayer
      )
        continue;
      Unit unit = hit.GetComponent<Unit>();
      if (unit != null && unit != this && !unit.isDead)
      {
        float distance = Vector2.Distance(transform.position, unit.transform.position);
        if (distance < closestDistance)
        {
          closestDistance = distance;
          closestTarget = unit;
        }
      }
    }

    target = closestTarget;
  }

  protected virtual void MoveToTarget()
  {
    float distance = Vector2.Distance(transform.position, target.transform.position);
    if (distance < attackRange)
    {
      AttackTarget();
      rb.linearVelocity = Vector2.zero;
      return;
    }

    ChangeAnimationState("walk");
    Vector3 direction = (target.transform.position - transform.position).normalized;
    rb.linearVelocity = direction * moveSpeed;
  }

  protected virtual void Move()
  {
    ChangeAnimationState("walk");
    Vector2 leftMovement = new Vector2((isPlayer ? 1 : -1) * moveSpeed, rb.linearVelocity.y);
    rb.linearVelocity = leftMovement;
  }

  protected virtual void AttackTarget()
  {
    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    attackTimer += Time.deltaTime;

    if (attackTimer >= attackSpeed)
    {
      if (target.isDead)
      {
        target = null;
        return;
      }
      SoundManager.Instance.PlaySound(attackSFXName);
      attackTimer = 0f;
      ChangeAnimationState("attack");

      if (target.transform.position.x < transform.position.x)
      {
        sr.flipX = true;
      }
      else
      {
        sr.flipX = false;
      }
    }
    else
    {
      if (stateInfo.IsName("attack") && stateInfo.normalizedTime < 1.0f)
        return;
      ChangeAnimationState("idle");
    }
  }

  public virtual void GiveDamage(float damage)
  {
    if (target && !target.isDead)
    {
      target.GetComponent<Unit>().TakeDamage(attack);
    }
    if (target.isDead)
    {
      target = null;
    }
  }

  public virtual void TakeDamage(float damage)
  {
    hp -= damage - armor;
    print("I took " + (damage - armor) + " damage!");
    if (hp <= 0)
    {
      Die();
    }
  }

  protected virtual void Die()
  {
    isDead = true;
    SoundManager.Instance.PlaySound("UnitDeath");
    ChangeAnimationState("death");
    col.enabled = false;
    Destroy(gameObject, .25f);
  }

  public static void KillAll()
  {
    GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
    foreach (GameObject unit in units)
    {
      unit.GetComponent<Unit>().Die();
    }
  }
}
