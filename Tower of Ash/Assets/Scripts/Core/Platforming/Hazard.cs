using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (!collider.gameObject.GetComponentInParent<Player>().invincible)
            {
                Debug.Log("Spike Damage");
                collider.gameObject.GetComponentInParent<Entity>().SetDamage(10);
                collider.gameObject.GetComponentInParent<Entity>().SetKnockback(-collider.gameObject.GetComponentInParent<Player>().FacingDirection);
                collider.gameObject.GetComponentInParent<Player>().isHit = true; ;
                collider.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);
            }
        }
    }
}
