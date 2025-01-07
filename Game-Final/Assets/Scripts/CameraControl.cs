using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float sensibility = 3f;
    private float softness = 0f;
    private float maxVerticalAngle = 35f;
    private float minVerticalAngle = -30.25f;

    private Vector2 switchPosition;
    private Vector2 camPosition;

    private GameObject playerObj;

    void Start()
    {
        playerObj = transform.parent.gameObject;
        camPosition = Vector2.zero;
    }

    void Update()
    {
        Vector2 mousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mousePos = Vector2.Scale(mousePos, new Vector2(sensibility, sensibility));

        switchPosition.x = Mathf.Lerp(switchPosition.x, mousePos.x, 1f / softness);
        switchPosition.y = Mathf.Lerp(switchPosition.y, mousePos.y, 1f / softness);

        camPosition.x += switchPosition.x; // Yatay hareket (X ekseni)
        camPosition.y = Mathf.Clamp(camPosition.y + switchPosition.y, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(-camPosition.y, 0f, 0f);
        playerObj.transform.localRotation = Quaternion.Euler(0f, camPosition.x, 0f);
    }
}