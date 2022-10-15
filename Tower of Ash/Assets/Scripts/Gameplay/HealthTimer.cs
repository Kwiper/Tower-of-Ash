using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthTimer : MonoBehaviour
{
    [SerializeField]
    private Entity playerEntity;

    [SerializeField] TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = ((int)playerEntity.Health).ToString();
    }
}
