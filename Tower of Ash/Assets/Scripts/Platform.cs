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
    private Transform transformPlat;
    [SerializeField]
    private float startAngle;
    [SerializeField]
    private float endAngle;
    private float posX,posY,posXPrev,posXDif, angleDeg, angleRad = 0f;
    //private float starterPosY;
    private GameObject essentials;
    public bool PlayerOnSwing = false;
    private void Start(){
        //Finds collider for platform and finds player script on player gameobject
        var EssentialObjectPossibles = GameObject.FindGameObjectsWithTag("EssentialObjects");
        essentials = EssentialObjectPossibles[0];

    }

    void FixedUpdate(){
        if (rotationCenter != null){
            posXPrev = posX;
            posX = rotationCenter.position.x + Mathf.Cos(angleRad)*rotationRadius;
            posY = rotationCenter.position.y + Mathf.Sin(angleRad)*rotationRadius/1.5f;
            transformPlat.position = new Vector2(posX,posY);
            angleRad = angleRad + Time.deltaTime*angularSpeed;
            //This is for resuability's sake as doing things in rad's sucks
            angleDeg = angleRad*(180/Mathf.PI);
            posXDif = posX-posXPrev;

            if (angleDeg >= startAngle || angleDeg <= endAngle){
                angularSpeed = angularSpeed*-1;
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
}
