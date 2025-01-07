using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool canAttack = true;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();

        }
    }

    private void Attack()
    {
        canAttack = false;
        animator.SetBool("Attack", true);
        StartCoroutine(FinishAnim());
    }

    private IEnumerator FinishAnim()
    {
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("Attack", false);
        canAttack = true;
    }
}
