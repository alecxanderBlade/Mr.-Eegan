using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementMain : MonoBehaviour
{
    [SerializeField]
    private GameObject cam, pause_menu;
    [SerializeField]
    private CharacterController player;
    [SerializeField]
    private float mouse_sensitivity, move_speed;
    [SerializeField]
    private GameObject game_logic_script;
    [SerializeField]
    private AudioSource footstep;
    float xRotation;
    private void Start()
    {
        player.GetComponent<CharacterController>();
        cam.GetComponent<GameObject>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        Look();
        Move();
        StartCoroutine(Stepping());
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
    private void Pause()
    {
        pause_menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        pause_menu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    IEnumerator Stepping()
    {
        yield return new WaitForSeconds(1f);
        if(player.velocity.sqrMagnitude > 0 && !footstep.isPlaying)
        {
            footstep.volume = 0.1f;
            footstep.Play();
        }
    }
}
