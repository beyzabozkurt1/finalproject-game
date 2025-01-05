using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensivity = 100f;
    public Transform playerBody; // Oyuncunun v�cut k�sm� i�in referans
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �mleci ekrana kilitle
    }

    private void Update()
    {
        // Fare girdilerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        // Yukar� ve a�a�� hareket (kamera e�imi)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Kameray� s�n�rlamak i�in Clamp

        // Kamera hareketini uygula
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Sa�a ve sola d�n�� (oyuncu modelini d�nd�r)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
