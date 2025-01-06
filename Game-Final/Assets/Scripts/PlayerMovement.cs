using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float x;
    float y;
    float speed = 3f;
    float walkSpeed = 3f;
    float runSpeed = 5f;
    private Animator animator;

    private bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Walking", true);


            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        x *= Time.deltaTime * speed;
        y *= Time.deltaTime * speed;

        transform.Translate(x, 0f, y);
    }

}
