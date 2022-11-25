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
                var positionCheck = new Vector2(tinderCache.gameObject.GetComponent<Transform>().position.x,tinderCache.gameObject.GetComponent<Transform>().position.y);
                foreach (Vector2 collectedTinderCachePos in playerData.CollectedTinderCacheLocations){
                    if(positionCheck == collectedTinderCachePos){
                        tinderCache.gameObject.SetActive(false);
                        break;
                    }
                }
            }

    }
}
