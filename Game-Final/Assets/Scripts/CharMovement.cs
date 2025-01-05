using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket hýzý
    public Rigidbody rb;        // Karakterin Rigidbody bileþeni
    private Animator animator;
    private Vector3 movement;   // Hareket vektörü

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Giriþleri al
        float moveX = Input.GetAxis("Horizontal"); // A ve D tuþlarý (sol/sað)
        float moveZ = Input.GetAxis("Vertical");   // W ve S tuþlarý (ileri/geri)
        if (moveX != 0 || moveZ != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        // Hareket vektörünü karakteri'n yönüne göre hesapla
        movement = transform.right * moveX + transform.forward * moveZ;
    }

    void FixedUpdate()
    {
        // Rigidbody kullanarak hareket ettir
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
