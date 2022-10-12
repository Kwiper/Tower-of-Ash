using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public float maxHealth;
    public float Health;

    public int knockbackForce;

    public bool InKnockback { get; private set; }
    public int Knockback { get; private set; }

    private float knockbackTimer;

    private void Awake()
    {
        Health = maxHealth;
        InKnockback = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InKnockback)
        {

            knockbackTimer -= Time.deltaTime;

            if(knockbackTimer <= 0)
            {
                InKnockback = false;
            }
        }

    }

    public void SetDamage(int damage)
    {
        Health -= damage;
    }

    public void SetKnockback(int direction)
    {
        InKnockback = true;
        knockbackTimer = 0.1f;
        Knockback = direction * knockbackForce;
    }
}
