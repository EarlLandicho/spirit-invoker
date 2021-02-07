using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : EnemyAttack
{
    [SerializeField] protected Transform projectileLaunchPosition;
    [SerializeField] protected float projectileSpeed;


    protected GameObject currentProjectile;

    protected EnemyMovement enemyMovement;
    protected Animator animator;
    protected bool isAttacking;

    new void Awake()
    {
        base.Awake();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (attackSpeedTemp > 0)
        {
            attackSpeedTemp -= Time.deltaTime;
        }
        else if (attackSpeedTemp <= 0 && !isAttacking)
        {
            animator.SetTrigger("attack");
            isAttacking = true;
        }

    }


    // Called in Animator
    public void ResetAttackSpeed()
    {
        attackSpeedTemp = attackSpeed;
        isAttacking = false;
    }

    // Called in Animator
    public virtual void Attack()
    {
        currentProjectile = Instantiate(projectile, projectileLaunchPosition.position, transform.rotation);
        currentProjectile.GetComponent<EnemyProjectile>().SetDamage(damage);
        currentProjectile.GetComponent<EnemyProjectile>().SetPlayerDirection(ExtensionMethods.GetNormalizedDirectionToPlayer2D(gameObject));
        currentProjectile.GetComponent<EnemyProjectile>().SetProjectileSpeed(projectileSpeed);
        currentProjectile.GetComponent<EnemyProjectile>().Launch();
    }
}