using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemy : MovingCreature {

    public float visionRange;
    public float attackRange;
    public float attackChargeUpTime;
    public float deflectRecoverTime;
    public float recoverTime;
    float resetRecTime;
    FiniteEnemy FE;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        resetRecTime = deflectRecoverTime;
        FE = GetComponent<FiniteEnemy>();
	}
	
	// Update is called once per frame
	void Update () {
        CalculateVelocity();
       
        
        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
        
    }

    protected override void CalculateVelocity()
    {

        if (FE.currentEnemyState==FiniteEnemy.State.Chase && IsOnGround() 
            || FE.currentEnemyState == FiniteEnemy.State.Patrol && IsOnGround() 
            || FE.currentEnemyState == FiniteEnemy.State.Recovering && IsOnGround())
        {
            float targetVelocityX = directionalInput.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
        }
        else if (FE.currentEnemyState==FiniteEnemy.State.Attack && IsOnGround() && directionalInput.y!=0)
        {
            velocity = directionalInput * attackSpeed;
            velocity.y += gravity * Time.deltaTime;
            
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        
    }

   

    public bool IsOnGround()
    {
        if (controller.collisions.below)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DeflectedTimer(ref float timer)
    {
        timer -= Time.deltaTime;
        if (timer<=0)
        {
            //velocity.x = Mathf.MoveTowards(velocity.x, velocity.normalized.x, 20f);
            //velocity.y = Mathf.MoveTowards(velocity.y, velocity.normalized.y, 20f);
            if (velocity.magnitude <= 3)
            {
                
                ResetActiveMovement();
                ResetDeflectTimer();

            }
        }
    }

    protected void ResetActiveMovement()
    {
        GetComponent<FiniteEnemy>().activeMovement = true;

    }

    public void ResetDeflectTimer()
    {
        deflectRecoverTime = resetRecTime;
    }




}
