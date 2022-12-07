using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject combatText;

    [SerializeField]
    GameObject directionalText;

    Player player;

    [SerializeField]
    PlayerData playerData;

    bool inTutorial = false;

    bool tut1 = true;
    bool tut2 = false;

    bool pressedInput = false;

    float alpha = 1f;

    float timer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (playerData.combatTutorial)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inTutorial)
        {
            TextMeshProUGUI combatTextColor = combatText.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI directionalTextColor = directionalText.GetComponent<TextMeshProUGUI>();

            if (tut1)
            {

                combatText.SetActive(true);   

                if (combatTextColor.color.a < 1f && !pressedInput)
                    alpha += Time.deltaTime;

                combatTextColor.color = new Color(1f, 1f, 1f, alpha);

                if (player.InputHandler.AttackInput && alpha >= 1f)
                {
                    pressedInput = true;
                }

                if (pressedInput)
                {
                    alpha -= Time.deltaTime;

                    if (alpha <= 0)
                    {
                        tut1 = false;
                        combatText.SetActive(false);
                        playerData.combatTutorial = true;
                        tut2 = true;
                    }
                }
            }

            if (tut2)
            {
                timer -= Time.deltaTime;

                directionalText.SetActive(true);
                if (directionalTextColor.color.a < 1f && timer > 0)
                    alpha += Time.deltaTime;

                directionalTextColor.color = new Color(1f, 1f, 1f, alpha);

                if(timer <= 0)
                {
                    alpha -= Time.deltaTime;
                }

                if(alpha <= 0)
                {
                    tut2 = false;
                    directionalText.SetActive(false);
                    inTutorial = false;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!playerData.combatTutorial)
            {
                inTutorial = true;
            }
        }
    }
}
