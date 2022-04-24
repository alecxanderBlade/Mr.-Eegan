using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEntrance : MonoBehaviour
{
    [SerializeField]
    private Collider door;
    [SerializeField]
    private Animator door_open;
    [SerializeField]
    private AudioSource door_sound;
    [SerializeField]
    private GameObject game_logic_script, flash_light;
    void Start()
    {
        door.GetComponent<Collider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            door.enabled = false;
            door_open.SetBool("IsOpen", false);
            door_sound.Play();
            Destroy(gameObject);
            game_logic_script.SetActive(false);
            Destroy(flash_light);
        }
    }
}
