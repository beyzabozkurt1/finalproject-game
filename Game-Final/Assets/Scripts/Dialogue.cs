using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    public GameObject dialoguePanel;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    private int index = 0;

    private void Start()
    {
        textComponent.text = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NpcArea"))
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        playerMovement.canMove = false;
        playerAttack.canAttack = false;
        playerMovement.animator.SetBool("Walking", false);
        dialoguePanel.SetActive(true);
        index = 0;
        textComponent.text = string.Empty;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        dialoguePanel.SetActive(false);
        playerMovement.canMove = true;
        StartCoroutine(SetCanAttack());
    }
    private IEnumerator SetCanAttack()
    {
        yield return new WaitForSeconds(1);
        playerAttack.canAttack = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
}
