using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private AudioSource right_ear;
    [SerializeField]
    private AudioSource left_ear;
    [SerializeField]
    private GameObject flashlight, flash_light, restart, player, eegan;
    private bool flash_on = true;
    void Start()
    {
        flashlight.SetActive(true);
        eegan.GetComponent<GameObject>();
        restart.GetComponent<GameObject>();
        player.GetComponent<GameObject>();
        flash_light.GetComponent<GameObject>();
        right_ear.GetComponent<AudioSource>();
        left_ear.GetComponent<AudioSource>();
        timer.GetComponent<TextMeshProUGUI>();
        right_ear.volume = 0.1f;
        StartCoroutine(Timer());
        StartCoroutine(Game_Logic());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && flash_on == true)
        {
            flash_light.SetActive(false);
            flash_on = false;
        }
        else if(Input.GetKeyDown(KeyCode.F) && flash_on == false)
        {
            flash_light.SetActive(true);
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
                    player.transform.position = restart.transform.position;

                    time = 11f;
                }
                if (right_ear.isPlaying && flash_on == true)
                {
                    player.transform.position = restart.transform.position;
                    time = 11f;
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
}
