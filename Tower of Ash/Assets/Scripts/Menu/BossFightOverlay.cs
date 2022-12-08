using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFightOverlay : MonoBehaviour
{
    float color;

    float stayWhiteTimer;

    [SerializeField] Image image;

    // Start is called before the first frame update
    void Start()
    {
        color = 1f;
        image.color = new Color(1, 1, 1, 1);
        stayWhiteTimer = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        image.color = new Color(color, color, color, 1);

        stayWhiteTimer -= Time.deltaTime;

        if(stayWhiteTimer <= 0)
        {
            color -= 0.5f * Time.deltaTime;
        }

    }
}
