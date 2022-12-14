using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjectsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EssentialObjectPrefab;
    [SerializeField]
    private PlayerData playerData;


    private void Awake(){

        var existingObjects = FindObjectsOfType<EssentialObjects>();

        if(existingObjects.Length == 0){
            Instantiate(EssentialObjectPrefab, playerData.spawnPoint, Quaternion.identity);
        }
    }
}
