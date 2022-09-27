using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region Components
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Other variables
    public Vector2 CurrentVelocity { get; private set; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
