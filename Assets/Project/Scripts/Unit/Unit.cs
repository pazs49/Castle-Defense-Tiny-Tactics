using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected bool isPlayer;

    [SerializeField]
    protected float hp;

    [SerializeField]
    protected float armor;

    [SerializeField]
    protected float attack;

    [SerializeField]
    protected float attackRange;

    [SerializeField]
    protected float attackSpeed;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected Animator animator;
    protected Rigidbody2D rb;

    [SerializeField]
    protected bool isDead = false;

    [SerializeField]
    protected Unit target;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update() { }

    protected virtual void FixedUpdate() { }

    protected virtual void Initialize() { }

    public void ChangeAnimationState(string state)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(state))
            return;
        animator.Play(state);
    }

    protected virtual void Act() { }

    protected void SearchForTarget() { }

    protected void MoveToTarget() { }

    protected virtual void AttackTarget() { }

    public virtual void TakeDamage(float damage) { }

    protected virtual void Die() { }
}
