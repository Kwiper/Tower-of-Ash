using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField]
    private float fallDelay = 1f;
    [SerializeField]
    private float destroyDelay = 2f;
    private Vector2 originalPos;
    private Transform transform;

    [SerializeField]
    private Rigidbody2D rb;    

    private void Start()
    {
        transform  = gameObject.GetComponent<Transform>();
        originalPos = new Vector2(transform.position.x,transform.position.y);
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            StartCoroutine(Fall());
        }

    }


    private IEnumerator Fall() 
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyDelay);
        //destroy(gameObject,destroyDelay);
    }
}
