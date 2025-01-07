using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Diðer karakterlerin metinlerini gösterecek TextMeshPro bileþeni
    public string[] lines; // Diyalog satýrlarý
    public float textSpeed = 0.05f; // Metin yazma hýzý
    public GameObject dialoguePanel;

    private int index = 0; // Hangi satýrda olduðumuzu tutar

    private void Start()
    {
        textComponent.text = string.Empty;
    }

    // NPC ile çarpýþmayý algýlayan fonksiyon (Collider ile etkileþim)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NpcArea")) // Oyuncu ile çarpýþma
        {
            StartDialogue(); // Diyaloðu baþlat
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        index = 0; // Diyalog baþý
        textComponent.text = string.Empty; // Metin temizle
        PlayerMovement.canMove = false; // Karakteri durdur
        Cursor.lockState = CursorLockMode.None; // Ýmleci serbest býrak
        StartCoroutine(TypeLine()); // Metni yazmaya baþla
    }

    IEnumerator TypeLine()
    {
        // Mevcut diyalog satýrýný yazma
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed); // Yazma hýzý
        }
    }

    // Diðer satýra geçmek için kullanýlan fonksiyon
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++; // Sonraki satýra geç
            textComponent.text = string.Empty; // Eski metni temizle
            StartCoroutine(TypeLine()); // Yeni satýrý yaz
        }
        else
        {
            EndDialogue(); // Diyalog bitince iþlemler
        }
    }

    void EndDialogue()
    {
        // Diyalog bittiðinde yapýlacak iþlemler
        PlayerMovement.canMove = true; // Karakteri hareket ettir
        Cursor.lockState = CursorLockMode.Locked; // Ýmleci kilitle
        dialoguePanel.SetActive(false); // Diyalog panelini kapat
    }

    // Herhangi bir tuþa basýldýðýnda (özellikle sol týklama veya baþka bir tuþ) metni hýzlý geçirme
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týklama
        {
            if (textComponent.text == lines[index])
            {
                NextLine(); // Metni geç
            }
            else
            {
                StopAllCoroutines(); // Yazma iþlemini durdur
                textComponent.text = lines[index]; // Tam metni hemen göster
            }
        }
    }
}
