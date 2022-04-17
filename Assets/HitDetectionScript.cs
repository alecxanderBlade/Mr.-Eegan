using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionScript : MonoBehaviour
{
    Ray line;
    [SerializeField]
    private GameObject elevator_panel, btn_prompt, elevator_collider, garage, office, game_logic;
    [SerializeField]
    private Animator elevator_left, elevator_right;
    [SerializeField]
    private float ray_length;
    [SerializeField]
    private AudioSource elevator_open_audio, btn_audio;
    [SerializeField]
    private AudioClip elevator_close_audio, elevator_go;

    // Start is called before the first frame update
    void Start()
    {
        game_logic.GetComponent<GameObject>();
        elevator_panel.GetComponent<GameObject>();
        btn_prompt.GetComponent<GameObject>();
        garage.GetComponent<GameObject>();
        office.GetComponent<GameObject>();
        elevator_left.GetComponent<Animator>();
        elevator_right.GetComponent<Animator>();
        elevator_collider.GetComponent<GameObject>();
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
        yield return new WaitForSeconds(22f);
        office.SetActive(true);
        Elevators_Open(true);
        elevator_open_audio.Play();
        game_logic.SetActive(true);
    }
}
