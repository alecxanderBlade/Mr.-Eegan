using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{ 
    [SerializeField] private new AudioSource audio;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private GameObject buttons, logo;
    [SerializeField] private CanvasGroup button_opacity;
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI btn_text;
    int question_counter = 4;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Audio_Clips());
        buttons.GetComponent<GameObject>();
        logo.GetComponent<GameObject>();
        button_opacity.GetComponent<CanvasGroup>();
        btn_text.GetComponent<TextMeshProUGUI>();
        btn.GetComponent<Button>().onClick.AddListener(Btn_Event);
        buttons.SetActive(false);
    }
    private void Btn_Event()
    {
        switch(question_counter)
        {
            case 4:
                StartCoroutine(Btn_Fade_Out(question_counter, "Unknown"));
                question_counter++;
                break;
            case 5:
                StartCoroutine(Btn_Fade_Out(question_counter, "Unknown"));
                question_counter++;
                break;
            case 6:
                StartCoroutine(Btn_Fade_Out(question_counter, "Delaware"));
                question_counter++;
                break;
            case 7:
                StartCoroutine(Btn_Fade_Out(question_counter, "What the fuck"));
                question_counter++;
                break;
            case 8:
                StartCoroutine(Btn_Fade_Out(question_counter, "Unknown"));
                question_counter++;
                break;
            default:
                StartCoroutine(Btn_Fade_Out(question_counter));
                break;
        }
    }
    // Update is called once per frame
    IEnumerator Audio_Clips()
    {
        audio.PlayOneShot(clips[0]);
        yield return new WaitForSeconds(clips[0].length + 2f);
        audio.PlayOneShot(clips[1]);
        yield return new WaitForSeconds(clips[1].length + 2f);
        audio.PlayOneShot(clips[3]);
        yield return new WaitForSeconds(clips[3].length);
        StartCoroutine(Btn_Fade_In("Begin"));
    }
    IEnumerator Btn_Fade_In(string str)
    {
        Cursor.visible = true;
        buttons.SetActive(true);
        btn_text.text = str;
        for (float i = 0.0f; i <= 1f;)
        {
            button_opacity.alpha = i;
            i += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator Btn_Fade_Out(int clip_index, string btn_text)
    {
        audio.PlayOneShot(clips[clip_index]);
        Cursor.visible = false;
        for (float i = 1f; i >= 0.0f;)
        {
            button_opacity.alpha = i;
            i -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        buttons.SetActive(false);
        yield return new WaitForSeconds(clips[clip_index].length);
        StartCoroutine(Btn_Fade_In(btn_text));
    }
    IEnumerator Btn_Fade_Out(int clip_index)
    {
        audio.PlayOneShot(clips[clip_index]);
        Cursor.visible = false;
        for (float i = 1f; i >= 0.0f;)
        {
            button_opacity.alpha = i;
            i -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        buttons.SetActive(false);
        yield return new WaitForSeconds(clips[clip_index].length/2f);
        audio.PlayOneShot(clips[clip_index + 1]);
        yield return new WaitForSeconds(clips[clip_index + 1].length);
        button_opacity.alpha = 1f;
        buttons.SetActive(true);
        logo.SetActive(true);
        yield return new WaitForSeconds(12f);
        buttons.SetActive(false);
    }
}
