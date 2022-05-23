using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] potions;
    private float spawnRangeX = 11;
    private float spawnPosZ = -3;
    private float spawnPosY = 10;
    private float startDelay = 20;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("instanticatePotion", startDelay, Random.Range(10.0f, 20.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void instanticatePotion()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, spawnPosZ);
        int prefabIndex = Random.Range(0, potions.Length);
        Instantiate(potions[prefabIndex], spawnPos, potions[prefabIndex].transform.rotation);
       
    }
}
