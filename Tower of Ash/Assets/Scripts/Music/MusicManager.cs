using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField]
    AudioSource[] sources;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sources.Length; i++)
        {
            sources[i].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
