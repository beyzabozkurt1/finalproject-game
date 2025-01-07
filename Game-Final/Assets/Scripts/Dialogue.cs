using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    public GameObject dialoguePanel;

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
        dialoguePanel.SetActive(true);
        index = 0;
        textComponent.text = string.Empty;
        PlayerMovement.canMove = false;
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
        PlayerMovement.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        dialoguePanel.SetActive(false);
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
