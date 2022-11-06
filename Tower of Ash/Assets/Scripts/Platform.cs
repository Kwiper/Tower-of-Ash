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
    private float gizmoPointSize = 1f;
    //private float starterPosY;
    //private Vector2 midPointPosition;
    private GameObject essentials;
    public bool PlayerOnSwing = false;
    private bool swingAccelerating;
    private float midPoint;
    private int swingState = 0;
    // 0 = swinging to the left
    // 1 = swinging to the right

    private void Awake()
    {
        angleRad = startAngle*(Mathf.PI/180);
        midPoint = (endAngle+startAngle)/2;
        
        
    }

    private void Start(){
        //Finds collider for platform and finds player script on player gameobject
        var EssentialObjectPossibles = GameObject.FindGameObjectsWithTag("EssentialObjects");
        essentials = EssentialObjectPossibles[0];
                
    }

    void FixedUpdate(){
        if (rotationCenter != null){
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
            }
            // Swing is moving left and is decelerating
            else if(angleDeg <= midPoint && swingState == 0){
                //Debug.Log("Swing is moving left and is decelerating");
                if(angularSpeed < minAngularSpeed){
                    angularSpeed = angularSpeed-(maxAngularSpeed/Mathf.Abs(acceleration));
                    swingAccelerating = false;
                }
            }
            // Swing is moving right and accelerating
            else if(angleDeg < midPoint && swingAccelerating == true && swingState == 1){
                //Debug.Log("Swing is moving right and accelerating");
                if(angularSpeed < (maxAngularSpeed*-1)){
                    angularSpeed = angularSpeed+((maxAngularSpeed/Mathf.Abs(acceleration))*-1);
                }
            }
            // Swing is moving right and is decelerating
            else if(angleDeg >= midPoint && swingState == 1){
                //Debug.Log("Swing is moving right and is decelerating");
                if(angularSpeed > (minAngularSpeed*-1)){
                    angularSpeed = angularSpeed-((maxAngularSpeed/Mathf.Abs(acceleration))*-1);
                    swingAccelerating = false;
                }
            }
        }
        

    }

    void OnCollisionEnter2D(Collision2D col){

        if(col.gameObject.tag == "Player"){
            col.transform.parent = transformPlat;
            PlayerOnSwing = true;
        }
    }

    void OnCollisionExit2D(Collision2D col){

        if(col.gameObject.tag == "Player"){
            col.transform.parent = essentials.GetComponent<Transform>();
            PlayerOnSwing = false;
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
}
