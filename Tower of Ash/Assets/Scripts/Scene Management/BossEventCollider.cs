using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (!BossEventManager.enteredRoom || !BossEventManager.transition || !BossEventManager.bossFight))
        { 
            BossEventManager.enteredRoom = true;
            this.gameObject.SetActive(false);
        }
    }
}
