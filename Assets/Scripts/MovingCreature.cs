using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class MovingCreature : MonoBehaviour
{

    public float moveSpeed;
    public float attackSpeed;
    protected float originalmoveSpeed;
    public float elasticity;
    [SerializeField] protected float maxJumpHeight = 4;
    [SerializeField] protected float minJumpHeight = 1;
    [SerializeField] protected float airResistance;
    [SerializeField] protected float timeToJumpToApex;
    [SerializeField] protected float accelerationTimeAirborne;
    [SerializeField] protected float accelerationTimeGrounded;
    [SerializeField] protected float wallSlideSpeedMax = 3;
    [SerializeField] protected float wallStickTime = 0.25f;
    [SerializeField] protected Vector2 wallJumpClimb;
    [SerializeField] protected Vector2 wallJumpOff;
    [SerializeField] protected Vector2 wallLeap;


    protected float gravity = -9.82f;
    protected float maxJumpVelocity;
    protected float minJumpVelocity;
    protected float jumpVelocity;
    protected float velocityXSmoothing;
    protected float velocityYSmoothing;
    protected Vector3 velocity;
    public Vector2 directionalInput;
    protected bool tired;
    protected bool walking;
    protected int wallDirX;
    protected Controller2D controller;


    protected virtual void Start()
    {
        controller = GetComponent<Controller2D>();

        originalmoveSpeed = moveSpeed;
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }



    public void SetDirectionalInput(Vector2 input)
    {
        walking = true;
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


    public void BurstVelocity(Vector2 newVelocity)
    {
        velocity = newVelocity;
    }

    protected bool CheckBounceCollisions()
    {
        if ((controller.collisions.left || controller.collisions.right) || controller.collisions.above || controller.collisions.below)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    protected Vector2 CalculateBounceVelocity()
    {

        Vector2 vel=new Vector2(velocity.x,velocity.y);

        Vector2 bounceDirection = vel + (-2) * Vector2.Dot(vel, controller.collisions.slopeNormal) * controller.collisions.slopeNormal;

        return bounceDirection;
    }

    protected virtual void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }


}
