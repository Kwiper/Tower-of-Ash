using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public int maxHealth;
    public int Health { get; private set; }

    private void Awake()
    {
        Health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDamage(int damage)
    {
        Health -= damage;
    }
}
