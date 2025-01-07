using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 10f;

    private bool move;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        move = true;
    }

    private void Update()
    {
        if (move)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage();
        }
    }


}
