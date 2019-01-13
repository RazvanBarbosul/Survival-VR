using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject Player;

    public Transform tPlayer;
    public Transform tCamera;

    private Camera cam;

    private float distance = 0f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float sensX = 4f;
    private float senxY = 1f;

    Vector3 camera;

    void Start()
    {
        Player = GameObject.Find("Player");
        tCamera = transform;
        cam = Camera.main;
        Player = GameObject.Find("Player");
        camera = new Vector3(0, 1.17f, 0);
    }


    void Update()
    {
        transform.position = Player.transform.position + camera;
        transform.rotation = Player.transform.rotation;
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, -0, 90);
        currentX = Mathf.Clamp(currentX, -89, 89);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        tCamera.position = tPlayer.position + rotation * dir;
        tCamera.LookAt(tPlayer.position);
    }
}
