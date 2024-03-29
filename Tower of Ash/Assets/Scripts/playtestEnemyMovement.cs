using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playtestEnemyMovement : Enemy
{
    public Enemy testEnemy;
    public float xVelocity;
    public int flipTime;

    [SerializeField]
    protected float timer = 0;

    // Start is called before the first frame update
    new void Start()
    {
    }

    // Update is called once per frame
    new void Update()
    {
        testEnemy.SetVelocityX(xVelocity);
        timer += Time.deltaTime;
        if(timer > flipTime) {
            xVelocity *= -1;
            timer = 0;
        }
    }
}
