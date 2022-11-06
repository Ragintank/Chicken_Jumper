using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.y < transform.position.y)
        {
            GetComponent<BoxCollider>().enabled = false;
        }

        if (GameObject.FindGameObjectWithTag("Player").transform.position.y > transform.position.y)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}
