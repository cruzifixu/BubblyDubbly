using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- Move
    private float verticalInput;

    // --- Skills
    private float speed = 10.0f;

    // --- Range
    private float xRange = 18;
    private float PlusZRange = 16;
    private float MinusZRange = -1;

    // --- bubbles
    public GameObject projectilePrefab;

    //private UpdatePlayerStats Stats;

    // Start is called before the first frame update
    void Start()
    {
        //Stats = GameObject.Find("PlayerStats").GetComponent<UpdatePlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Stats.getHealth() > 0)
        //{
           
            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
          

            /* player can not get off grounds
            if (transform.position.x < -xRange)
            { transform.position = new Vector3(-xRange, transform.position.y, transform.position.z); }
            if (transform.position.x > xRange)
            { transform.position = new Vector3(xRange, transform.position.y, transform.position.z); }

            if (transform.position.z < MinusZRange)
            { transform.position = new Vector3(transform.position.x, transform.position.y, MinusZRange); }
            if (transform.position.z > PlusZRange)
            { transform.position = new Vector3(transform.position.x, transform.position.y, PlusZRange); }


            */

            if (Input.GetKeyDown(KeyCode.Space))
            { Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation); } // returns clone of original
        //}

    }
}
