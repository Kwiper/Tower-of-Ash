using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    Player player;

    [SerializeField]
    GameObject[] children;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player.InputHandler.MapInput)
        {
            
            foreach(GameObject child in children)
            {
                child.SetActive(true);
            }
            
        }
        else
        {
            
            foreach(GameObject child in children)
            {
                child.SetActive(false);
            }
            
        }
    }


}
