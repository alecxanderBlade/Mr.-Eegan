using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private AudioSource right_ear, left_ear, both_jumpscare, flashlight_btn;
    [SerializeField]
    private GameObject flashlight, flash_light, restart, player, eegan, instructions;
    [SerializeField]
    private Animator jumpscare;
    private bool flash_on = true;
    IEnumerator timer_coroutine;
    void Start()
    {
        timer_coroutine = Timer();
        flashlight.SetActive(true);
        flashlight_btn.Play();
        jumpscare.GetComponent<Animator>();
        instructions.GetComponent<GameObject>();
        eegan.GetComponent<GameObject>();
        restart.GetComponent<GameObject>();
        player.GetComponent<GameObject>();
        flash_light.GetComponent<GameObject>();
        timer.GetComponent<TextMeshProUGUI>();
        right_ear.volume = 0.1f;
        StartCoroutine(timer_coroutine);
        StartCoroutine(Game_Logic());
        StartCoroutine(Instructions());
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && flash_on == true)
        {
            flash_light.SetActive(false);
            flashlight_btn.Play();
            flash_on = false;
        }
        else if(Input.GetKeyDown(KeyCode.F) && flash_on == false)
        {
            flash_light.SetActive(true);
            flashlight_btn.Play();
            flash_on = true;
        }
    }
    IEnumerator Game_Logic()
    {
        for(int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(Random.Range(30f, 46f));
            int ear = Random.Range(0, 2);
            if (ear == 0)
            {
                left_ear.Play();
            }
            else if (ear == 1)
            {
                right_ear.Play();
            }
            yield return new WaitForSeconds(2f);
            float time = 0f;
            while(time <= 11f)
            {
                if (left_ear.isPlaying && flash_on == true)
                {
                    left_ear.Stop();
                    flashlight.SetActive(false);
                    eegan.SetActive(true);
                    jumpscare.SetBool("Game_Over", true);
                    yield return new WaitForSeconds(0.5f);
                    both_jumpscare.Play();
                    yield return new WaitForSeconds(4f);
                    eegan.SetActive(false);
                    eegan.transform.position += new Vector3(0, 10f, 0);
                    player.transform.position = restart.transform.position;
                    flashlight.SetActive(true);
                    time = 11f;
                    StopCoroutine(timer_coroutine);
                    StartCoroutine(timer_coroutine = Timer());
                }
                if (right_ear.isPlaying && flash_on == true)
                {
                    right_ear.Stop();
                    flashlight.SetActive(false);
                    eegan.SetActive(true);
                    jumpscare.SetBool("Game_Over", true);
                    yield return new WaitForSeconds(0.5f);
                    both_jumpscare.Play();
                    yield return new WaitForSeconds(4f);
                    eegan.SetActive(false);
                    eegan.transform.position += new Vector3(0, 10f, 0); 
                    player.transform.position = restart.transform.position;
                    flashlight.SetActive(true);              
                    time = 11f;
                    StopCoroutine(timer_coroutine);
                    StartCoroutine(timer_coroutine = Timer());
                }
                time++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
    IEnumerator Timer()
    {
        for(int i = 300; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            timer.SetText(i.ToString());
        }
    }
    IEnumerator Instructions()
    {
        instructions.SetActive(true);
        yield return new WaitForSeconds(7f);
        Destroy(instructions);
    }
}
