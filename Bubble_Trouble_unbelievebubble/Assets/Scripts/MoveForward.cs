using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 30.0f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 bubblePos = new Vector3(0, player.transform.rotation.y+1, 0);
        transform.Translate(bubblePos * Time.deltaTime * speed);
    }
}
