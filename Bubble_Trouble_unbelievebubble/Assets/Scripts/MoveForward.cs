using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 30.0f;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
