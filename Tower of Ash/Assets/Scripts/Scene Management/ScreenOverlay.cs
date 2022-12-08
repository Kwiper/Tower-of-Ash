using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOverlay : MonoBehaviour
{
    bool fadeToBlack;

    [SerializeField]
    Image overlay;

    Player player;

    float alpha;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        fadeToBlack = false;
        alpha = 1f;
        overlay.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        overlay.color = new Color(0, 0, 0, alpha);

        if (fadeToBlack)
        {
            if(alpha < 1f)
            {
                alpha += 0.75f * Time.deltaTime;
            }
            if(alpha > 1f)
            {
                alpha = 1f;
            }
        }

        if (!fadeToBlack)
        {
            if(alpha > 0)
            {
                alpha -= 0.75f * Time.deltaTime;
            }
            if(alpha < 0)
            {
                alpha = 0;
            }
        }

        if(player.StateMachine.CurrentState == player.DeathState)
        {
            fadeToBlack = true;
        }
    }

    public void SetFadeToBlackTrue() => fadeToBlack = true;

    public void SetFadeToBlackFalse() => fadeToBlack = false;
}
