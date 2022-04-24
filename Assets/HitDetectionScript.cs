using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitDetectionScript : MonoBehaviour
{
    Ray line;
    [SerializeField]
    private GameObject btn_prompt, elevator_collider, garage, office, game_logic, instructions;
    [SerializeField]
    private Animator elevator_left, elevator_right, entrance_door_open;
    [SerializeField]
    private float ray_length;
    [SerializeField]
    private AudioSource elevator_open_audio, btn_audio, door_sound;
    [SerializeField]
    private AudioClip elevator_close_audio, elevator_go, door_open, door_close;
    [SerializeField]
    private TextMeshPro level;

    // Start is called before the first frame update
    void Start()
    {
        garage.GetComponent<GameObject>();
        level.GetComponent<TextMeshPro>();
        StartCoroutine(Instructions());
    }

    private void Elevators_Open(bool status)
    {
        elevator_left.SetBool("Btn_Pressed", status);
        elevator_right.SetBool("Btn_Pressed", status);
    }

    // Update is called once per frame
    void Update()
    {
        line = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(line, out hit, ray_length))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            if (hit.collider.CompareTag("Btn_Outside"))
            {
                btn_prompt.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && elevator_left.GetBool("Btn_Pressed") == false)
                {

                    Elevators_Open(true);
                    btn_audio.Play();
                    elevator_open_audio.Play();
                }
                else if (Input.GetKeyDown(KeyCode.E) && elevator_left.GetBool("Btn_Pressed") == true)
                {
                    Elevators_Open(false);
                    btn_audio.Play();
                    elevator_open_audio.PlayOneShot(elevator_close_audio);
                }
            }
            else if (hit.collider.CompareTag("Btn_Inside"))
            {
                btn_prompt.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && elevator_left.GetBool("Btn_Pressed") == true)
                {
                    elevator_collider.SetActive(true);
                    Elevators_Open(false);
                    btn_audio.Play();
                    elevator_open_audio.PlayOneShot(elevator_close_audio);
                    elevator_open_audio.PlayOneShot(elevator_go);
                    StartCoroutine(Load_Office());
                }
            }
            else if (hit.collider.CompareTag("Disable_Entrance"))
            {
                btn_prompt.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && entrance_door_open.GetBool("IsOpen") == false)
                {
                    entrance_door_open.SetBool("IsOpen", true);
                    door_sound.PlayOneShot(door_open);
                }
                else if (Input.GetKeyDown(KeyCode.E) && entrance_door_open.GetBool("IsOpen") == true)
                {
                    entrance_door_open.SetBool("IsOpen", false);
                    door_sound.PlayOneShot(door_close);
                }
            }
            else if (hit.collider.CompareTag("Quit"))
            {
                btn_prompt.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Application.Quit();
                }
                
            }
            else
            {
                btn_prompt.SetActive(false);
            }
        }
        else
        {
            btn_prompt.SetActive(false);
        }
    }
    IEnumerator Load_Office()
    {
        yield return new WaitForSeconds(5f);
        elevator_collider.SetActive(false);
        Destroy(garage);
        for(int i = 0; i < 7; i++)
        {
            level.text = (i + 1).ToString();
            yield return new WaitForSeconds(3.14285714286f);
        }
        office.SetActive(true);
        Elevators_Open(true);
        elevator_open_audio.Play();
        game_logic.SetActive(true);
    }
    IEnumerator Instructions()
    {
        instructions.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(instructions);
    }
}
