using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject movementText;

    [SerializeField]
    GameObject jumpText;

    Player player;

    [SerializeField]
    PlayerData playerData;

    bool inTutorial = false;

    bool tut1 = true;
    bool transition = false;
    bool tut2 = false;
    bool transition2 = false;

    float alpha = 1f;

    bool leftTutorial = false;
    float countdown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (playerData.movementTutorial)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI moveTextColor = movementText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI jumpTextColor = jumpText.GetComponent<TextMeshProUGUI>();

        if (inTutorial)
        {
            if (tut1)
            {
                movementText.SetActive(true);
                
                if (moveTextColor.color.a < 1f)
                    alpha += Time.deltaTime;

                moveTextColor.color = new Color(1f, 1f, 1f, alpha);

                if (player.InputHandler.NormInputX > 0 && alpha >= 1f)
                {
                    tut1 = false;
                    transition = true;

                }
            }
            if (transition)
            {
                if (alpha > 0)
                {
                    alpha -= Time.deltaTime;
                }

                moveTextColor.color = new Color(1f, 1f, 1f, alpha);

                if (alpha <= 0)
                {
                    movementText.SetActive(false);
                    transition = false;
                    tut2 = true;
                }
            }
            if (tut2)
            {
                jumpText.SetActive(true);
                
                if (jumpTextColor.color.a < 1f)
                    alpha += Time.deltaTime;

                jumpTextColor.color = new Color(1f, 1f, 1f, alpha);

                if (player.InputHandler.JumpInput && alpha >= 1f)
                {
                    tut2 = false;
                    transition2 = true;     
                }
            }
            if (transition2)
            {
                if (alpha > 0)
                {
                    alpha -= Time.deltaTime;
                }

                jumpTextColor.color = new Color(1f, 1f, 1f, alpha);

                if (alpha <= 0)
                {
                    jumpText.SetActive(false);
                    transition2 = false;
                    inTutorial = false;
                    playerData.movementTutorial = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!playerData.movementTutorial)
            {
                inTutorial = true;
            }
        }
    }

}
