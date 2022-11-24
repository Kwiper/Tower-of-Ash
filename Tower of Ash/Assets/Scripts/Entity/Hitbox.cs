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

    public bool HitObject { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        HitObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag(tagName))
        {
            target = collision.gameObject.GetComponentInParent<Entity>();
            target.SetDamage((int)(combatData.damage * combatData.damageMultiplier));

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
            HitObject = true;

        }

        if (collision.CompareTag("Spikes"))
        {
            HitObject = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HitObject = false;
    }

}
