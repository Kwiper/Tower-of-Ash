using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private Transform rotationCenter;
    [SerializeField]
    private float rotationRadius = 2f, angularSpeed = 2f;
    [SerializeField]
    private float  maxAngularSpeed = 2f, minAngularSpeed = 2f;
    [SerializeField]
    private Transform transformPlat;
    [SerializeField]
    private float startAngle;
    [SerializeField]
    private float endAngle;
    private float posX,posY,posXPrev,posXDif, angleDeg, angleRad = 0f;
    [SerializeField]
    private float arcSizeY = 1.5f;
    [SerializeField]
    private float arcSizeX = 1f;
    [SerializeField]
    private float acceleration = 150f;
    [SerializeField]
    private bool swingRightFirst = false;
    private float gizmoPointSize = 1f;
    //private float starterPosY;
    //private Vector2 midPointPosition;
    private GameObject essentials;
    //private GameObject player;
    private Player player;
    public bool playerOnSwing = false;
    private bool playerHitHead = false;
    private bool swingAccelerating;
    private float midPoint;
    private int swingState = 0;
    [SerializeField]
    private bool delayStart = false;
    [SerializeField]
    private float delaySeconds = 0;
    private Hazard[] spikes;
    private float knockbackDelaySeconds = 0.5f;
    private int tempKnockbackholder;
    // 0 = swinging to the left
    // 1 = swinging to the right

    private void Awake()
    {
        //Sets it to travel in the right direction if boolean is true else it swings left
        if(swingRightFirst){
            swingState = 1;
            angleRad = endAngle*(Mathf.PI/180);
            angularSpeed = angularSpeed*-1;
        }
        else{
            angleRad = startAngle*(Mathf.PI/180);
        }

        midPoint = (endAngle+startAngle)/2;



        
    }

    private void Start(){
        //Finds collider for platform and finds player script on player gameobject
        var EssentialObjectPossibles = GameObject.FindGameObjectsWithTag("EssentialObjects");
        essentials = EssentialObjectPossibles[0];

        //var embersToAdd = GameObject.FindObjectsOfType<LostEmber>();
        spikes = GetComponentsInChildren<Hazard>();
        //Debug.Log(spikes.Length);
        /*
        for (int i = 0; i < embersToAdd.Length; i++) 
        {
            //Debug.Log(embersToAdd[i].gameObject.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(embersToAdd[i].gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>(), true);
            foreach (Hazard spike in spikes){
                Physics2D.IgnoreCollision(embersToAdd[i].gameObject.GetComponent<BoxCollider2D>(), spike.gameObject.GetComponent<PolygonCollider2D>(), true);
            }
        }
        */
        if(delayStart == true) StartCoroutine(waiter());
                
    }

    void FixedUpdate(){
        //Prevents wall clipping
        if(player != null){
            if(playerOnSwing && player.CheckIfTouchingWall()) {
                
                player.transform.parent = essentials.GetComponent<Transform>();

            }
            else if(playerOnSwing && player.CheckIfTouchingWallBack()) {
                
                player.transform.parent = essentials.GetComponent<Transform>();

            }
            else if(playerOnSwing && player.CheckIfTouchingCeiling() && player.hitHead == false){
                //Player's transform is set to essential objects
                player.hitHead = true;
                player.transform.parent = essentials.GetComponent<Transform>();
                Physics2D.IgnoreCollision(player.gameObject.GetComponentInParent<BoxCollider2D>(), GetComponent<PolygonCollider2D>(), true);
                if (spikes.Length != 0){
                    foreach (Hazard spike in spikes){
                        Physics2D.IgnoreCollision(player.gameObject.GetComponentInParent<BoxCollider2D>(),spike.gameObject.GetComponent<PolygonCollider2D>(),true);
                    }
                }

                //player actually takes damage
                if (!player.invincible)
                {
                    Debug.Log("Head Damage");
                    player.gameObject.GetComponentInParent<Entity>().SetKnockback(-player.FacingDirection);
                    //Time stopped because knockback is weird without it
                    player.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);
                    player.gameObject.GetComponentInParent<Entity>().SetDamage(10);
                    player.isHit = true;
                
                }
                //Coroutine to reset the knockback for the player to the original
                StartCoroutine(platformReset());
            }
            else if(playerOnSwing && player.CheckIfTouchingCeiling() && player.hitHead == true){
                //Player's transform is set to essential objects
                Debug.Log("Head hit but no Damage");
                player.hitHead = true;
                player.transform.parent = essentials.GetComponent<Transform>();
                Physics2D.IgnoreCollision(player.gameObject.GetComponentInParent<BoxCollider2D>(), GetComponent<PolygonCollider2D>(), true);
                if (spikes.Length != 0){
                    foreach (Hazard spike in spikes){
                        Physics2D.IgnoreCollision(player.gameObject.GetComponentInParent<BoxCollider2D>(),spike.gameObject.GetComponent<PolygonCollider2D>(),true);
                    }
                }
                //Coroutine to reset the knockback for the player to the original
                StartCoroutine(platformReset());
            }
            else if(playerOnSwing && !player.CheckIfTouchingWall() || playerOnSwing && !player.CheckIfTouchingWallBack() || playerOnSwing && !player.CheckIfTouchingCeiling()) {
                
                player.transform.parent = transformPlat;

            }
        
        }

        if (rotationCenter != null && delayStart == false){
            posXPrev = posX;
            posX = rotationCenter.position.x + Mathf.Cos(angleRad)*rotationRadius*arcSizeX;
            posY = rotationCenter.position.y + Mathf.Sin(angleRad)*rotationRadius/arcSizeY;
            transformPlat.position = new Vector2(posX,posY);
            angleRad = angleRad + Time.deltaTime*angularSpeed;
            //This is for resuability's sake as doing things in rad's sucks
            angleDeg = angleRad*(180/Mathf.PI);
            posXDif = posX-posXPrev;

            if (angleDeg >= startAngle || angleDeg <= endAngle){
                if (swingState == 0){
                    swingState = 1;
                }
                else{
                    swingState = 0;
                }
                angularSpeed = angularSpeed*-1;
                swingAccelerating = true;
            }
            // Swing is moving left and accelerating
            if(angleDeg > midPoint && swingAccelerating == true && swingState == 0){
                //Debug.Log("Swing is moving left and accelerating");
                if(angularSpeed > maxAngularSpeed){
                    angularSpeed = angularSpeed+(maxAngularSpeed/Mathf.Abs(acceleration));
                }
                //if(angularSpeed < maxAngularSpeed) angularSpeed = maxAngularSpeed;
            }
            // Swing is moving left and is decelerating
            else if(angleDeg <= midPoint && swingState == 0){
                //Debug.Log("Swing is moving left and is decelerating");
                if(angularSpeed < minAngularSpeed){
                    angularSpeed = angularSpeed-(maxAngularSpeed/Mathf.Abs(acceleration));
                    swingAccelerating = false;
                }
                //if(angularSpeed > minAngularSpeed) angularSpeed = minAngularSpeed;
            }
            // Swing is moving right and accelerating
            else if(angleDeg < midPoint && swingAccelerating == true && swingState == 1){
                //Debug.Log("Swing is moving right and accelerating");
                if(angularSpeed < (maxAngularSpeed*-1)){
                    angularSpeed = angularSpeed+((maxAngularSpeed/Mathf.Abs(acceleration))*-1);
                }
                //if(angularSpeed > (maxAngularSpeed*-1))angularSpeed = maxAngularSpeed;
            }
            // Swing is moving right and is decelerating
            else if(angleDeg >= midPoint && swingState == 1){
                //Debug.Log("Swing is moving right and is decelerating");
                if(angularSpeed > (minAngularSpeed*-1)){
                    angularSpeed = angularSpeed-((maxAngularSpeed/Mathf.Abs(acceleration))*-1);
                    swingAccelerating = false;
                }
                //if(angularSpeed < (minAngularSpeed*-1))angularSpeed = minAngularSpeed;
            }
        }



    }


    void OnCollisionEnter2D(Collision2D col){

        if(col.gameObject.tag == "Player"){
            if(player == null){
                player = col.gameObject.GetComponent<Player>();
            }
                col.transform.parent = transformPlat;
                if(player.CheckIfGrounded()){
                playerOnSwing = true;
                }

        }
    }

    void OnCollisionExit2D(Collision2D col){

        if(col.gameObject.tag == "Player"){
            col.transform.parent = essentials.GetComponent<Transform>();
            playerOnSwing = false;
        }
    }

    public float getAngularSpeed(){
        return angularSpeed;
    }
    public float getPosXDif(){
        return posXDif;
    }

    private void OnDrawGizmos()
    {


        //Start Point
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector2(rotationCenter.position.x + Mathf.Cos(startAngle*(Mathf.PI/180))*rotationRadius*arcSizeX,rotationCenter.position.y + Mathf.Sin(startAngle*(Mathf.PI/180))*rotationRadius/arcSizeY), gizmoPointSize);

        //End Point
        Gizmos.DrawWireSphere(new Vector2(rotationCenter.position.x + Mathf.Cos(endAngle*(Mathf.PI/180))*rotationRadius*arcSizeX, rotationCenter.position.y + Mathf.Sin(endAngle*(Mathf.PI/180))*rotationRadius/arcSizeY), gizmoPointSize);

        //Mid point
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(rotationCenter.position.x + Mathf.Cos(((endAngle+startAngle)/2)*(Mathf.PI/180))*rotationRadius*arcSizeX, rotationCenter.position.y + Mathf.Sin(((endAngle+startAngle)/2)*(Mathf.PI/180))*rotationRadius/arcSizeY), gizmoPointSize);
    }

    IEnumerator waiter()
    {

        yield return new WaitForSecondsRealtime(delaySeconds);
        delayStart = false;

    }

    IEnumerator platformReset()
    {

        yield return new WaitForSecondsRealtime(knockbackDelaySeconds);
        //player.gameObject.GetComponentInParent<Entity>().knockbackForce = tempKnockbackholder;
        player.hitHead = false;
        Physics2D.IgnoreCollision(player.gameObject.GetComponentInParent<BoxCollider2D>(), GetComponent<PolygonCollider2D>(), false);
        if (spikes.Length != 0){
            foreach (Hazard spike in spikes){
                Physics2D.IgnoreCollision(player.gameObject.GetComponentInParent<BoxCollider2D>(),spike.gameObject.GetComponent<PolygonCollider2D>(),false);
            }
        }        
    }
}
