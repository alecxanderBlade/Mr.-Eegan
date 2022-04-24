using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup menu_opacity;
    [SerializeField] private GameObject menu, start_game;
    void Start()
    {
        menu_opacity.GetComponent<CanvasGroup>();
    }
    public void Play()
    {
        StartCoroutine(Fade_In_Game());
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator Fade_In_Game()
    {
        start_game.SetActive(true);
        for (float i = 1.00f; i >= 0.00f;)
        {
            yield return new WaitForSeconds(0.01f);
            i -= 0.02f;
            menu_opacity.alpha = i;
        }
        menu.SetActive(false);
        Destroy(menu);
    }
}
