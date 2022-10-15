using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    GameObject play;
    private Transform playerPos;
    [SerializeField] Transform spawnPoint;


    private void Awake()
    {
        //play = GameObject.FindGameObjectsWithTag("Player")[0];
       // play.SetActive(true);
        //playerPos = play.GetComponent<Transform>();
        //gameObject.transform.position = playerPos.position;
    }
}
