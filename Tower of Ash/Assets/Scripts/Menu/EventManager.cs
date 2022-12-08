using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    GameObject blackScreen;

    [SerializeField]
    GameObject eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen.gameObject.SetActive(false);
        eventSystem.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnScreenBlack()
    {
        blackScreen.gameObject.SetActive(true);
    }

    public void TurnOffEventSystem()
    {
        eventSystem.gameObject.SetActive(false);
    }
}
