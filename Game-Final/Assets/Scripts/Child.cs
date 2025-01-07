using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    bool startMoving = false;
    Animator animator;

    public GameObject player;

    private float speed;
    private float groundCheckDistance = 1f;
    public LayerMask groundLayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        float delay = Random.Range(0f, 2f);
        float newSpeed = Random.Range(0.2f, 1.5f);
        speed = newSpeed;
        StartCoroutine(StartMovement(delay));
    }


    private void Update()
    {
        if (!startMoving) return;

        FollowPlayer();
        AdjustToGround();
    }

    private IEnumerator StartMovement(float delay)
    {
        yield return new WaitForSeconds(delay);

        animator.enabled = true;
        startMoving = true;

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
}
