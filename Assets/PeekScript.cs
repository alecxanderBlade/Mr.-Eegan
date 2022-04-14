using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekScript : MonoBehaviour
{
    [SerializeField]
    private Animator peek;
    [SerializeField]
    private GameObject eegan;

    // Start is called before the first frame update
    void Start()
    {
        peek.GetComponent<Animator>();
        eegan.GetComponent<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            peek.SetBool("Peek", true);
            StartCoroutine(Destroy_Self());
        }
    }
    IEnumerator Destroy_Self()
    {
        yield return new WaitForSeconds(0.36f);
        Destroy(this);
        Destroy(eegan);
    }
}
