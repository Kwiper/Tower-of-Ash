using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject combatText;

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
            combatText.SetActive(true);
            TextMeshProUGUI combatTextColor = combatText.GetComponent<TextMeshProUGUI>();

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
                    inTutorial = false;
                    combatText.SetActive(false);
                    inTutorial = false;
                    playerData.combatTutorial = true;
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
