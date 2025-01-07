using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameManagerPlayer gameManager;

    public bool canAttack = true;
    private Animator animator;

    [SerializeField] private GameObject magic;
    [SerializeField] private Transform magicSpawnPoint;
    public float magicSpeed = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (gameManager.puzzleContinue) return;

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        canAttack = false;
        animator.SetBool("Attack", true);
        InstantiateAndShootMagic();
        StartCoroutine(FinishAnim());
    }

    private void InstantiateAndShootMagic()
    {
        GameObject instantiatedMagic = Instantiate(magic, magicSpawnPoint.position, Quaternion.identity);

        Magic magicScript = instantiatedMagic.GetComponent<Magic>();
        if (magicScript != null)
        {
            magicScript.SetDirection(magicSpawnPoint.forward);
        }
    }


    private IEnumerator FinishAnim()
    {
        yield return new WaitForSeconds(0.45f);
        animator.SetBool("Attack", false);
        canAttack = true;
    }
}
