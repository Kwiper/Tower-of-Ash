using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{


    [SerializeField]
    private CombatData combatData;

    private Entity target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("Entered layer");
            target = collision.gameObject.GetComponent<Entity>();
            target.SetDamage(combatData.damage);
        }
        
    }

}
