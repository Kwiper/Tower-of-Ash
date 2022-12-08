using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventManager : MonoBehaviour
{
    [SerializeField]
    private LockableDoor bossDoor;

    public static bool bossFight = false;
    public static bool enteredRoom = false;
    public static bool transition = false;
    bool bossFightEnded = false;
    bool oneShotPlayed = false;

    float enteredRoomTimer;
    float bossFightTimer;

    float bossFightEndedTimer;

    Boss boss;
    Player player;

    [SerializeField]
    AudioSource bossMusic;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip bossDefeated;

    [SerializeField]
    GameObject titleCard;

    [SerializeField]
    GameObject whiteOverlay;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
        player = FindObjectOfType<Player>();
        bossFight = false;
        enteredRoom = false;
        transition = false;
        bossFightEnded = false;
        oneShotPlayed = false;
        enteredRoomTimer = 2f;
        bossFightTimer = 2f;
        bossFightEndedTimer = 8f;
    }

    private void Update()
    {
        if (enteredRoom && (!transition || !bossFight))
        {
            enteredRoomTimer -= Time.deltaTime;
            if(enteredRoomTimer <= 0)
            {
                if (!bossFight && !transition)
                {
                    bossMusic.Play();
                    titleCard.SetActive(true);
                }
                transition = true;
                enteredRoom = false;
            }
        }

        if (transition)
        {
            bossFightTimer -= Time.deltaTime;

            if(bossFightTimer <= 0)
            {
                titleCard.SetActive(false);
                transition = false;
                bossFight = true;
            }
        }

        if(boss.EnemyEntity.Health <= 0)
        {
            if (!oneShotPlayed)
            {
                source.PlayOneShot(bossDefeated,0.5f);
                bossMusic.Stop();
            }

            oneShotPlayed = true;
            bossFightEnded = true;
        }

        if (bossFightEnded)
        {
            whiteOverlay.SetActive(true);
            bossFightEndedTimer -= Time.deltaTime;


            if(bossFightEndedTimer <= 0)
            {
                //Transition to cutscene scene.
                Debug.Log("Boss fight ended");
            }
        }

    }

}
