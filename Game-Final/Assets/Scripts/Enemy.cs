using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 3f;
    public float groundCheckDistance = 2f;
    public LayerMask groundLayer;

    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    private bool isAttacking = false;

    private bool startFight;

    public Animator animator;

    private float health = 100f;

    private bool canAttack = true;
    private bool canMove = true;

    public GameManagerPlayer gameManager;



    private void Update()
    {
        if (startFight)
        {
            if(Vector3.Distance(this.transform.position, player.transform.position) <= 1.5f && canAttack)
            {
                Attack();
            }
            else if (canMove)
            {
                FollowPlayer();
                AdjustToGround();
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0; // Düþmanýn sadece yatay eksende hareket etmesini saðla
        transform.position += direction * speed * Time.deltaTime;

        // Oyuncuya doðru yüzünü döndür
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        }
    }

    private void AdjustToGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer))
        {
            Vector3 targetPosition = transform.position;
            targetPosition.y = hit.point.y;
            transform.position = targetPosition;
        }
    }

    public void FightStarted()
    {
        startFight = true;
        animator.SetBool("Walking", true);
    }

    private void Attack()
    {
        Debug.Log("Attack");
        animator.SetBool("Attack", true);
        canAttack = false;
        canMove = false;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Attack", false);
        canAttack = true;
        canMove = true;
    }

    public void TakeDamage()
    {
        health -= 10f;
        Debug.Log("TakeDamage");
        if(health <= 0)
        {
            startFight = false;
            gameManager.KillEnemy();
            Dead();
        }
    }

    public void Dead()
    {
        animator.SetBool("Dead", true);
    }
}