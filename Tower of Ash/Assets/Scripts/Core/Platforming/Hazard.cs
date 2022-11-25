using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] 
    private bool usesSpikeCheckpoint = false;
    //private bool playerHitAlready = false;
    private Player player;
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            player = collider.gameObject.GetComponentInParent<Player>();
            if (!player.invincible)
            {
                Debug.Log("Spike Damage");
                collider.gameObject.GetComponentInParent<Entity>().SetDamage(10);
                collider.gameObject.GetComponentInParent<Entity>().SetKnockback(-collider.gameObject.GetComponentInParent<Player>().FacingDirection);
                collider.gameObject.GetComponentInParent<Player>().isHit = true; ;
                player.hitSpike = true;
                collider.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);
            }
            if(usesSpikeCheckpoint == true && player.hitSpike == true) StartCoroutine(reseter());
        }
    }

    IEnumerator reseter()
    {

        yield return new WaitForSecondsRealtime(0.2f);
        player.setPosition();
        player.hitSpike = false;;

    }
}
