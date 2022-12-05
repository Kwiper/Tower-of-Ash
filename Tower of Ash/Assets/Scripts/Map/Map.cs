using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    [SerializeField]
    PlayerData data;

    MapID[] children;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        children = GetComponentsInChildren<MapID>(true);

        foreach (MapID id in children)
        {
            for (int i = 0; i < data.ids.Count; i++)
            {
                if (id.id == data.ids[i])
                {
                    id.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnEnable()
    {

    }
}
