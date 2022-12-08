using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthMusic : MonoBehaviour
{
    [SerializeField]
    AudioSource lowHealthMusic;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.PlayerEntity.Health <= (player.PlayerEntity.maxHealth * 0.25))
        {
            if(lowHealthMusic.volume < 0.25f)
            {
                lowHealthMusic.volume += 0.25f * Time.deltaTime;
            }
            else if(lowHealthMusic.volume > 0.25f)
            {
                lowHealthMusic.volume = 0.25f;
            }
        }

        else
        {
            if (lowHealthMusic.volume > 0)
            {
                lowHealthMusic.volume -= 0.25f * Time.deltaTime;
            }
            else if (lowHealthMusic.volume < 0)
            {
                lowHealthMusic.volume = 0;
            }
        }
    }
}
