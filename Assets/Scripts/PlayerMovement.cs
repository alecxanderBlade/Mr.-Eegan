using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject animated_cam, cam, start_rotation_cam;
    [SerializeField]
    private CharacterController player;
    [SerializeField]
    private float mouse_sensitivity, move_speed;
    float xRotation;
    private void Start()
    {
        animated_cam.GetComponent<GameObject>();
        player.GetComponent<CharacterController>();
        cam.GetComponent<GameObject>();
        start_rotation_cam.GetComponent<GameObject>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        cam.transform.LookAt(start_rotation_cam.transform);
        Destroy(animated_cam, 13f);
    }
    void Update()
    {
        Look();
        Move();
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal"), z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        player.Move(move * move_speed * Time.deltaTime);
        player.Move(new Vector3(0f, -3f, 0f) * Time.deltaTime);
    }
    private void Look()
    {
        float mouse_x = Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

        xRotation -= mouse_y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector2.up, mouse_x);
    }
}
