using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject animated_cam, cam, start_rotation_cam, pause_menu;
    [SerializeField]
    private CharacterController player;
    [SerializeField]
    private float mouse_sensitivity, move_speed;
    [SerializeField]
    private AudioSource steps, speaker;
    [SerializeField]
    private Button btn;
    float xRotation;
    bool isPaused = false;
    private void Start()
    {
        animated_cam.GetComponent<GameObject>();
        player.GetComponent<CharacterController>();
        cam.GetComponent<GameObject>();
        start_rotation_cam.GetComponent<GameObject>();
        cam.transform.LookAt(start_rotation_cam.transform);
        Destroy(animated_cam, 13f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        Look();
        Move();
        Pause();
    }
    private void Move()
    {
        if(!isPaused)
        {
            float x = Input.GetAxis("Horizontal"), z = Input.GetAxis("Vertical");
            if (player.velocity.magnitude != 0f && !steps.isPlaying)
            {
                steps.volume = 0.1f;
                steps.Play();
            }
            Vector3 move = transform.right * x + transform.forward * z;

            player.Move(move * move_speed * Time.deltaTime);
            player.Move(new Vector3(0f, -3f, 0f) * Time.deltaTime);
        }
    }
    private void Look()
    {
        if(!isPaused)
        {
            float mouse_x = Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;
            float mouse_y = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

            xRotation -= mouse_y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector2.up, mouse_x);
        }
    }
    private void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pause_menu.SetActive(false);
            isPaused = false;
            speaker.UnPause();
            if(!btn.IsActive())
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pause_menu.SetActive(true);
            isPaused = true;
            speaker.Pause();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Resume()
    {
        pause_menu.SetActive(false);
        isPaused = false;
        speaker.UnPause();
        if (!btn.IsActive())
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
