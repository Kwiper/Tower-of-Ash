using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthTimer : MonoBehaviour
{
    [SerializeField]
    private Entity playerEntity;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    TextMeshProUGUI tinderText;

    [SerializeField]
    TextMeshProUGUI flaskText;

    [SerializeField]
    PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerEntity.Health / 100;

        anim.SetFloat("health", playerEntity.Health);

        tinderText.text = "x " + playerData.tinder;
        flaskText.text = "x " + playerData.healCharges;
    }
}
