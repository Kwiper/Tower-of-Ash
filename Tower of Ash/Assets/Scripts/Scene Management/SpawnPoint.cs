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
        play = GameObject.FindGameObjectsWithTag("EssentialObjects")[0];
        //play.SetActive(true);
        playerPos = play.GetComponent<Transform>();
        playerPos.position = spawnPoint.position;

        var essentialClean =  GameObject.FindGameObjectsWithTag("EssentialObjects");

        //Prevents players from duping themselves in the spawn room
        if(essentialClean.Length > 1){
            //Destroy(essentialClean[0]);
            
            for (int i = 0; i < essentialClean.Length; i++) 
            {
                var pos = essentialClean[i].GetComponent<Transform>().position;
                essentialClean[i].GetComponent<Transform>().position = new Vector2(pos.x+(i*10),0);
                Debug.Log("I am at the "+i+" array position and my transform is at "+ pos);
            }   
        }
    }
}
