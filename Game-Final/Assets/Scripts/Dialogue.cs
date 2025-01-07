using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Di�er karakterlerin metinlerini g�sterecek TextMeshPro bile�eni
    public string[] lines; // Diyalog sat�rlar�
    public float textSpeed = 0.05f; // Metin yazma h�z�
    public GameObject dialoguePanel;

    private int index = 0; // Hangi sat�rda oldu�umuzu tutar

    private void Start()
    {
        textComponent.text = string.Empty;
    }

    // NPC ile �arp��may� alg�layan fonksiyon (Collider ile etkile�im)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NpcArea")) // Oyuncu ile �arp��ma
        {
            StartDialogue(); // Diyalo�u ba�lat
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        index = 0; // Diyalog ba��
        textComponent.text = string.Empty; // Metin temizle
        PlayerMovement.canMove = false; // Karakteri durdur
        Cursor.lockState = CursorLockMode.None; // �mleci serbest b�rak
        StartCoroutine(TypeLine()); // Metni yazmaya ba�la
    }

    IEnumerator TypeLine()
    {
        // Mevcut diyalog sat�r�n� yazma
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed); // Yazma h�z�
        }
    }

    // Di�er sat�ra ge�mek i�in kullan�lan fonksiyon
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // Sonraki sat�ra ge�
            textComponent.text = string.Empty; // Eski metni temizle
            StartCoroutine(TypeLine()); // Yeni sat�r� yaz
        }
        else
        {
            EndDialogue(); // Diyalog bitince i�lemler
        }
    }

    void EndDialogue()
    {
        // Diyalog bitti�inde yap�lacak i�lemler
        PlayerMovement.canMove = true; // Karakteri hareket ettir
        Cursor.lockState = CursorLockMode.Locked; // �mleci kilitle
        dialoguePanel.SetActive(false); // Diyalog panelini kapat
    }

    // Herhangi bir tu�a bas�ld���nda (�zellikle sol t�klama veya ba�ka bir tu�) metni h�zl� ge�irme
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�klama
        {
            if (textComponent.text == lines[index])
            {
                NextLine(); // Metni ge�
            }
            else
            {
                StopAllCoroutines(); // Yazma i�lemini durdur
                textComponent.text = lines[index]; // Tam metni hemen g�ster
            }
        }
    }
}
