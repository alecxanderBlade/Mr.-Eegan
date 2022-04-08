using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionScript : MonoBehaviour
{
    Ray line;
    [SerializeField]
    private GameObject elevator_panel, elevator_light, btn_prompt;
    [SerializeField]
    private Animator elevator_left, elevator_right;
    [SerializeField]
    float ray_length;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        elevator_panel.GetComponent<GameObject>();
        elevator_light.GetComponent<GameObject>();
        btn_prompt.GetComponent<GameObject>();
        elevator_left.GetComponent<Animator>();
        elevator_right.GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        line = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(line, out hit, ray_length))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            if (hit.collider.CompareTag("Btn"))
            {
                btn_prompt.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E) && elevator_light.activeSelf)
                {
                    elevator_left.SetBool("Btn_Pressed", true);
                    elevator_right.SetBool("Btn_Pressed", true);
                }
                else if(Input.GetKeyDown(KeyCode.E) && !elevator_light.activeSelf)
                {
 
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
}
