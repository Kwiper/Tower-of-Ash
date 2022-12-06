using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{

    [SerializeField]
    PlayerData data;

    [SerializeField]
    GameObject iconPositions;

    [SerializeField]
    IconPositionId[] iconPosList;

    RectTransform transform;

    private void Start()
    {
        transform = GetComponent<RectTransform>();
        iconPosList = iconPositions.GetComponentsInChildren<IconPositionId>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(IconPositionId iconPos in iconPosList)
        {
            if(data.positionId == iconPos.id)
            {
                transform.position = iconPos.gameObject.transform.position;
            }
        }
    }
}
