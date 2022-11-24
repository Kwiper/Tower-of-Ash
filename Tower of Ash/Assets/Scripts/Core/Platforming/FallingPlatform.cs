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
    private Collider2D col;
    private SpriteRenderer sr;
    private bool isFalling = false;
    private bool isVisible = true;

    [SerializeField]
    private Rigidbody2D rb;    

    private void Start()
    {
        transform  = this.gameObject.GetComponent<Transform>();
        col = this.gameObject.GetComponent<Collider2D>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        originalPos = new Vector2(transform.position.x,transform.position.y);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if(collision.gameObject.tag == "Player" && isFalling == false && player.CheckIfGrounded()){
            StartCoroutine(Fall());
            isFalling = true;
        }

    }


    private IEnumerator Fall() 
    {
        yield return new WaitForSeconds(fallDelay);
        col.enabled = !col.enabled;
        sr.enabled = !sr.enabled;
        yield return new WaitForSeconds(destroyDelay);
        transform.position = originalPos;
        isFalling = false;
        col.enabled = !col.enabled;
        sr.enabled = !sr.enabled;

    }
}
