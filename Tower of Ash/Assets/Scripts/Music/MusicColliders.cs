using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColliders : MonoBehaviour
{

    [SerializeField]
    AudioSource[] areaSounds;

    [SerializeField]
    AudioSource[] soundsToDisable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            for (int i = 0; i < areaSounds.Length; i++)
            {
                if (areaSounds[i].volume < 0.25f)
                {
                    areaSounds[i].volume += 0.25f * Time.deltaTime;
                }

                if (areaSounds[i].volume > 0.25f)
                {
                    areaSounds[i].volume = 0.25f;
                }
            }

            for (int i = 0; i < soundsToDisable.Length; i++)
            {
                if (soundsToDisable[i].volume > 0)
                {
                    areaSounds[i].volume -= 0.25f * Time.deltaTime;
                }

                if (soundsToDisable[i].volume < 0)
                {
                    areaSounds[i].volume = 0;
                }
            }
        }
    }

}
