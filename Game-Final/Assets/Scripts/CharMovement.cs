using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket h�z�
    public Rigidbody rb;        // Karakterin Rigidbody bile�eni
    private Animator animator;
    private Vector3 movement;   // Hareket vekt�r�

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Giri�leri al
        float moveX = Input.GetAxis("Horizontal"); // A ve D tu�lar� (sol/sa�)
        float moveZ = Input.GetAxis("Vertical");   // W ve S tu�lar� (ileri/geri)
        if (moveX != 0 || moveZ != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        // Hareket vekt�r�n� karakteri'n y�n�ne g�re hesapla
        movement = transform.right * moveX + transform.forward * moveZ;
    }

    void FixedUpdate()
    {
        // Rigidbody kullanarak hareket ettir
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
