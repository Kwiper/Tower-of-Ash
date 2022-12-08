using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextActiveTime : MonoBehaviour
{
    [SerializeField]
    float activeTimer;

    [SerializeField]
    float fadeOutTimer;

    bool isInActiveTime = false;
    bool isFadingOut = false;

    float alpha = 0f;

    [SerializeField]
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        isInActiveTime = false;
        text.color = new Color(1, 0, 0, 0);
        isFadingOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.color = new Color(1, 0, 0, alpha);

        if (!isInActiveTime)
        {
            if(alpha < 1f)
            {
                alpha += Time.deltaTime;
            }

            if(alpha >= 1)
            {
                isInActiveTime = true;
            }
        }

        if (isInActiveTime)
        {
            activeTimer -= Time.deltaTime;

            if(activeTimer <= 0)
            {
                isFadingOut = true;
            }
        }

        if (isFadingOut)
        {
            fadeOutTimer -= Time.deltaTime;

            alpha -= 1/fadeOutTimer * Time.deltaTime;

            if(alpha <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
