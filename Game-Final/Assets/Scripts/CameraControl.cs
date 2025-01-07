using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameManagerPlayer gameManager;

    private float sensibility = 3f;
    private float softness = 0f;
    private float maxVerticalAngle = 35f;
    private float minVerticalAngle = -30.25f;

    private Vector2 switchPosition;
    private Vector2 camPosition;

    private GameObject playerObj;




    public Transform player; // Oyuncu Transform
    public float orbitDistance = 5f; // Oyuncudan olan uzaklýk
    public float orbitHeight = 2f; // Kameranýn yüksekliði
    public float orbitDuration = 2f; // Kameranýn bir tam tur atma süresi
    public float playerLiftHeight = 3f; // Oyuncunun yükselme yüksekliði

    private Vector3 originalPosition; // Kameranýn baþlangýç pozisyonu
    private Quaternion originalRotation; // Kameranýn baþlangýç rotasyonu
    private Vector3 playerOriginalPosition; // Oyuncunun baþlangýç pozisyonu
    private bool isOrbiting = false;


    void Start()
    {
        playerObj = transform.parent.gameObject;
        camPosition = Vector2.zero;
    }

    public void StartOrbit()
    {
        if (!isOrbiting)
        {
            isOrbiting = true;
            StartCoroutine(OrbitAroundPlayer());
        }
    }

    void Update()
    {
        if (gameManager.puzzleContinue) return;
        if (isOrbiting)
        {
            return;
        }

        Vector2 mousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mousePos = Vector2.Scale(mousePos, new Vector2(sensibility, sensibility));

        switchPosition.x = Mathf.Lerp(switchPosition.x, mousePos.x, 1f / softness);
        switchPosition.y = Mathf.Lerp(switchPosition.y, mousePos.y, 1f / softness);

        camPosition.x += switchPosition.x; // Yatay hareket (X ekseni)
        camPosition.y = Mathf.Clamp(camPosition.y + switchPosition.y, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(-camPosition.y, 0f, 0f);
        playerObj.transform.localRotation = Quaternion.Euler(0f, camPosition.x, 0f);
    }

    private IEnumerator OrbitAroundPlayer()
    {

        // Kaydet kameranýn ve oyuncunun orijinal pozisyon ve rotasyonu
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        playerOriginalPosition = player.position;

        float elapsedTime = 0f;

        while (elapsedTime < orbitDuration)
        {
            // Kamera etrafýnda döndürme
            float angle = (elapsedTime / orbitDuration) * 360f; // Dönüþ açýsý
            Vector3 offset = new Vector3(
                Mathf.Sin(Mathf.Deg2Rad * angle) * orbitDistance,
                orbitHeight,
                Mathf.Cos(Mathf.Deg2Rad * angle) * orbitDistance
            );
            transform.position = player.position + offset;
            transform.LookAt(player); // Oyuncuya bak

            // Oyuncuyu yukarý kaldýr
            float playerLift = Mathf.Sin((elapsedTime / orbitDuration) * Mathf.PI) * playerLiftHeight;
            player.position = new Vector3(playerOriginalPosition.x, playerOriginalPosition.y + playerLift, playerOriginalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Kamerayý eski pozisyonuna ve rotasyonuna geri döndür
        float transitionDuration = 0.5f; // Eski pozisyona dönme süresi
        elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, elapsedTime / transitionDuration);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, elapsedTime / transitionDuration);

            // Oyuncuyu eski pozisyonuna döndür
            player.position = Vector3.Lerp(player.position, playerOriginalPosition, elapsedTime / transitionDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pozisyon ve rotasyonu tamamen eski haline ayarla
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        player.position = playerOriginalPosition;

        isOrbiting = false;
    }
}