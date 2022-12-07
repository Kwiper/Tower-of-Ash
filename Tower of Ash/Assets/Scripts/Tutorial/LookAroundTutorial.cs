using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LookAroundTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject lookText;

    Player player;

    [SerializeField]
    PlayerData playerData;

    bool inTutorial = false;

    bool pressedInput = false;

    float alpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (playerData.lookTutorial)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inTutorial)
        {
            lookText.SetActive(true);
            TextMeshProUGUI lookTextColor = lookText.GetComponent<TextMeshProUGUI>();

            if (lookTextColor.color.a < 1f && !pressedInput)
                alpha += Time.deltaTime;

            lookTextColor.color = new Color(1f, 1f, 1f, alpha);

            if ((player.InputHandler.NormLookInputX > 0 || player.InputHandler.NormInputY > 0) && alpha >= 1f)
            {
                pressedInput = true;
            }

            if (pressedInput)
            {
                alpha -= Time.deltaTime;

                if(alpha <= 0)
                {
                    inTutorial = false;
                    lookText.SetActive(false);
                    inTutorial = false;
                    playerData.lookTutorial = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!playerData.lookTutorial)
            {
                inTutorial = true;
            }
        }
    }

}
