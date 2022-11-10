using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tindercount : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    TextMeshProUGUI tinder;

    private void Update()
    {
        tinder.text = "Tinder: " + playerData.tinder;
    }
}
