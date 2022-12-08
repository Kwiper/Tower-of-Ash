using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusic : MonoBehaviour
{

    Player player;

    [SerializeField]
    AudioSource combatMusic;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.CheckIfEnemyIsInRange())
        {
            if(combatMusic.volume < 0.25f)
            {
                combatMusic.volume += 0.25f * Time.deltaTime;
            }
            else if(combatMusic.volume > 0.25f)
            {
                combatMusic.volume = 0.25f;
            }
        }
        else
        {
            if(combatMusic.volume > 0)
            {
                combatMusic.volume -= 0.25f * Time.deltaTime;
            }
            else if (combatMusic.volume < 0)
            {
                combatMusic.volume = 0f;
            }
        }
    }
}
