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
            data.ids.Add(id);
        }
    }
}
