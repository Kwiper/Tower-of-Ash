using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{

    float timer = 3f;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
