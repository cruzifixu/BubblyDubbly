using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topZ = 10f;
    private float topY = 15f;
    private float topX = 28f;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topZ || transform.position.y > topY || transform.position.x > topX)
        {
            if (gameObject.CompareTag("bubble")) { Destroy(gameObject); }
            // if(gameObject.CompareTag("Player")) { }
        }
        else if (transform.position.z < -topZ || transform.position.y < -topY || transform.position.x < -topX)
        {
            if (gameObject.CompareTag("bubble")) { Destroy(gameObject); }
            // if (gameObject.CompareTag("Player")) { }
        }
    }
}
