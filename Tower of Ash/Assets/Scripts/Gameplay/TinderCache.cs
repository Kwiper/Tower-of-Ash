using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderCache : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private int tinderReward = 50;
    [SerializeField]
    public int ID = 0;
    //private bool isCollected = false;

    [SerializeField]
    Sprite[] sprites;

    SpriteRenderer renderer;

    int timesHit = 0;

    [SerializeField]
    List<GameObject> tinderList;

    [SerializeField]
    GameObject[] tinderObjects;

    [SerializeField] GameObject tinderParticleContainer;
    GameObject tParticle;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[0];

        tParticle = Instantiate(tinderParticleContainer, transform);
        tParticle.transform.position = new Vector3(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y+.1f, transform.position.z);
        tParticle.GetComponent<ParticleSystem>().Play();

        CreateTinderList();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if (other.CompareTag("Player"))
        {	
	        playerData.tinder = playerData.tinder + tinderReward;
            var pos = new Vector2(gameObject.GetComponent<Transform>().position.x,gameObject.GetComponent<Transform>().position.y);
            playerData.CollectedTinderCacheLocations.Add(pos);
	        GetComponent<BoxCollider2D>().enabled = false;
	        GetComponent<SpriteRenderer>().enabled = false;
        }
        */

        if (other.CompareTag("Hitbox"))
        {
            timesHit += 1;

            if(timesHit > 3)
            {
                //Explode into tinder
                SpawnTinder();
                playerData.CollectedTinderCacheID.Add(ID);
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;

                Destroy(tParticle, .1f);
            }
        }

    }

    private void Update()
    {
        if(timesHit <= 3)
            renderer.sprite = sprites[timesHit];
    }

    void CreateTinderList()
    {
        int tens = tinderReward / 10;
        for (int i = 0;i < tens; i++)
        {
            tinderList.Add(tinderObjects[2]);
        }

        int fives = (tinderReward % 10) / 5;
        for(int i = 0; i < fives; i++)
        {
            tinderList.Add(tinderObjects[1]);
        }

        int ones = (tinderReward % 10) % 5;
        for(int i = 0; i < ones; i++)
        {
            tinderList.Add(tinderObjects[0]);
        }
    }

    void SpawnTinder()
    {
        for(int i = 0; i < tinderList.Count; i++)
        {
            Vector2 randomVel = new Vector2(Random.Range(-1f, 1f), 1).normalized;

            GameObject instance = Instantiate(tinderList[i], transform.position, transform.rotation);
            //instance.GetComponent<Rigidbody2D>().velocity = randomVel;

        }
    }

    public void setTinderReward(int tinderRewardVal){
        tinderReward = tinderRewardVal;
    }

    public void setPlayerData(PlayerData playData){
        playerData = playData;
    }   
}
