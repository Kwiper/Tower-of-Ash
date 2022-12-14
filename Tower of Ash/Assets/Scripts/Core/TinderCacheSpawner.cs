using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderCacheSpawner : MonoBehaviour
{
    private TinderCache[] tinderCaches;
    [SerializeField]
    private PlayerData playerData;

        private void Start(){

        tinderCaches = GetComponentsInChildren<TinderCache>();
            foreach (TinderCache tinderCache in tinderCaches){
                var IDCheck = tinderCache.ID;
                if(playerData.CollectedTinderCacheID.Count != 0){
                    foreach (int collectedTinderCacheID in playerData.CollectedTinderCacheID){

                        if(IDCheck == collectedTinderCacheID){
                            tinderCache.gameObject.SetActive(false);
                            break;
                        }
                    }
                }
            }

    }
}
