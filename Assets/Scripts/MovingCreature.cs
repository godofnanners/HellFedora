using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class MovingCreature : MonoBehaviour {

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float maxJumpHeight = 4;
    [SerializeField] protected float minJumpHeight = 1;
    [SerializeField] protected float timeToJumpToApex;
    [SerializeField] protected float accelerationTimeAirborne;
    [SerializeField] protected float accelerationTimeGrounded;
    [SerializeField] protected float wallSlideSpeedMax = 3;
    [SerializeField] protected float wallStickTime = 0.25f;
    [SerializeField] protected Vector2 wallJumpClimb;
    [SerializeField] protected Vector2 wallJumpOff;
    [SerializeField] protected Vector2 wallLeap;
    protected float timeToWallUnstick;
    protected float gravity = -9.82f;
    protected float maxJumpVelocity;
    protected float minJumpVelocity;
    protected float jumpVelocity;
    protected float velocityXSmoothing;
    protected Vector3 velocity;
    protected Vector2 directionalInput;
    protected bool tired;
    protected int wallDirX;
    protected Controller2D controller;
    

    protected virtual void Start()
    {
        controller = GetComponent<Controller2D>();
        

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public virtual void OnJumpInputDown()
    {
        
        if (controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    


    

    protected void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }
}
