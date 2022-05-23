using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float bound = 30f;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > bound || transform.position.y > bound || transform.position.x > bound)
        {
            if (gameObject.CompareTag("0") || gameObject.CompareTag("1") || gameObject.CompareTag("2")) { Destroy(gameObject); }
        }
        else if (transform.position.z < -bound || transform.position.y < -bound || transform.position.x < -bound)
        {
            if (gameObject.CompareTag("0") || gameObject.CompareTag("1") || gameObject.CompareTag("2")) { Destroy(gameObject); }
        }
    }
}
