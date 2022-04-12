using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisterEeganScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mister_eegan;
    void Start()
    {
        mister_eegan.GetComponent<GameObject>();
        StartCoroutine(Disappear(mister_eegan));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Disappear(GameObject gameObject)
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
