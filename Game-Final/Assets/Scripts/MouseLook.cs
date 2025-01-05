using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensivity = 100f;
    public Transform playerBody; // Oyuncunun vücut kýsmý için referans
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ýmleci ekrana kilitle
    }

    private void Update()
    {
        // Fare girdilerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        // Yukarý ve aþaðý hareket (kamera eðimi)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Kamerayý sýnýrlamak için Clamp

        // Kamera hareketini uygula
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Saða ve sola dönüþ (oyuncu modelini döndür)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
