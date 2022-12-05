using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollider : MonoBehaviour
{
    [SerializeField]
    PlayerData data;

    [SerializeField]
    int id;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!data.ids.Contains(id)) {
                data.ids.Add(id);
            }
        }
    }
}
