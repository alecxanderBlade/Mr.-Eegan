using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementMain player_;
    [SerializeField]
    private TextMeshProUGUI timer;
    [SerializeField]
    private AudioSource right_ear, left_ear, both_jumpscare, flashlight_btn;
    [SerializeField]
    private GameObject flashlight, flash_light, restart, player, eegan, instructions_game_logic, pause_menu;
    [SerializeField]
    private Animator jumpscare;
    private bool flash_on = true, isPaused = false;
    float time;
    IEnumerator timer_coroutine, game_logic;
    void Start()
    {
        player_.enabled = true;
        timer_coroutine = Timer();
        game_logic = Game_Logic();
        flashlight.SetActive(true);
        flashlight_btn.Play();
        jumpscare.GetComponent<Animator>();
        timer.GetComponent<TextMeshProUGUI>();
        right_ear.volume = 0.1f;
        StartCoroutine(timer_coroutine);
        StartCoroutine(game_logic);
        StartCoroutine(Instructions());
    }
    // Update is called once per frame
    void Update()
    {
        Flashlight();
        Pause();
    }
    private void Flashlight()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.F) && flash_on == true)
            {
                flash_light.SetActive(false);
                flashlight_btn.Play();
                flash_on = false;
            }
            else if (Input.GetKeyDown(KeyCode.F) && flash_on == false)
            {
                flash_light.SetActive(true);
                flashlight_btn.Play();
                flash_on = true;
            }
        }
    }
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pause_menu.SetActive(false);
            isPaused = false;
            player_.enabled = true;
            StartCoroutine(timer_coroutine);
            StartCoroutine(game_logic = Game_Logic());
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pause_menu.SetActive(true);
            isPaused = true;
            player_.enabled = false;
            StopCoroutine(timer_coroutine);
            StopCoroutine(game_logic);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Resume()
    {
        pause_menu.SetActive(false);
        isPaused = false;
        player_.enabled = true;
        StartCoroutine(timer_coroutine);
        StartCoroutine(game_logic = Game_Logic());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator Game_Logic()
    {
        for (int i = 0; i < 15; i++)
        {
            int ear = Random.Range(0, 2);
            yield return new WaitForSeconds(Random.Range(30f, 46f));
            if (ear == 0)
            {
                left_ear.Play();
            }
            else if (ear == 1)
            {
                right_ear.Play();
            }
            yield return new WaitForSeconds(1f);
            time = 0f;
            while (time <= 11f)
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
            if(i == 0)
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
        }
    }
    IEnumerator Instructions()
    {
        instructions_game_logic.SetActive(true);
        yield return new WaitForSeconds(7f);
        Destroy(instructions_game_logic);
    }
}
