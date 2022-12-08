using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField]
    AudioSource[] sources;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        for(int i = 0; i < sources.Length; i++)
        {
            sources[i].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.StateMachine.CurrentState == player.DeathState)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].Stop();
            }
        }

    }
}
