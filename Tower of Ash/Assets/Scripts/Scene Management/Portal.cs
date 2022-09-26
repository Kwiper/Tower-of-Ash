using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, IplayerTriggerable
{
    
    public void OnPlayerTriggered(Player play)
    {
        Debug.Log("Player entered portal");
    }
    
}
