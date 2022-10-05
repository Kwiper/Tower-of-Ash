using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{


    [SerializeField]
    private CombatData combatData;

    [SerializeField]
    private string tagName;

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

        if(collision.CompareTag(tagName))
        {
            target = collision.gameObject.GetComponent<Entity>();
            target.SetDamage(combatData.damage);

            int dir = (int)(collision.gameObject.transform.position.x - this.transform.parent.position.x);
            if (dir >= 0)
            {
                dir = 1;
            }
            else if (dir < 0)
            {
                dir = -1;
            }

            target.SetKnockback(dir);

        }
        
    }

}
