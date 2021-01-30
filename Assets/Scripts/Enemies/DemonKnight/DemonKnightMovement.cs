﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonKnightMovement : EnemyMovement
{
    [SerializeField] private Transform cliffCheckRight;

    private bool willGoRight;

    new void Awake()
    {
        base.Awake();
        MovementCheck();
        
    }

    void Update()
    {
        
        if (!isXMovementSpeedLocked)
        {
            if(willGoRight)
            {
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            }
        }
    }

    void FixedUpdate()
    {
        TurnWhenAboutToFallCheck();
    }

    private void MovementCheck()
    {
        if (PlayerIsOnTheRight())
        {
            willGoRight = true;
        }
        else
        {
            willGoRight = false;
        }
    }

    public override void SetLockXMovement(bool isLocked)
    {
        isXMovementSpeedLocked = isLocked;
        if (isLocked)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            movementSpeed = movementSpeedTemp * movementSpeedModifier;

            //Rechecks if enemy needs to go left or right based on where the player is
            MovementCheck();
        }
    }

    //Will turn when wall is hit
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;

            bool top = contactPoint.y > center.y;
            bool right = contactPoint.x > center.x;

            if (!top && !right)
            {
                willGoRight = !willGoRight;
            }
        }
    }

    //Will turn when about to fall
    private void TurnWhenAboutToFallCheck()
    {
        if (!(Physics2D.OverlapCircle(new Vector2(cliffCheckRight.position.x, cliffCheckRight.position.y), 0.05f, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.OverlapCircle(new Vector2(cliffCheckRight.position.x, cliffCheckRight.position.y), 0.05f, 1 << LayerMask.NameToLayer("Ledge"))))
        { 
            willGoRight = !willGoRight;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(cliffCheckRight.position, 0.05f);
    }

}
