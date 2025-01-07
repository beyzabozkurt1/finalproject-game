using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    float x;
    float y;
    float speed = 3f;
    float walkSpeed = 3f;
    float runSpeed = 5f;
    public Animator animator;

    public bool canMove = true;
    public GameManagerPlayer gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (gameManager.puzzleContinue) return;


        if (canMove)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StartFight"))
        {
            Destroy(other.gameObject);
            gameManager.StartFight();
        }

        if(gameManager.enemyKilled && other.gameObject.CompareTag("Puzzle"))
        {
            gameManager.StartPuzzle();
        }
    }
}
