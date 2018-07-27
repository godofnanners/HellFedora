using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MovingCreature {

    bool wallSliding;
    bool wallEdgeGrabbing;
    public float timeToWallUnstick;
    Sword sword;
    Deflect deflect;

    protected override void Start()
    {
        base.Start();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        deflect = GameObject.FindGameObjectWithTag("Deflect").GetComponent<Deflect>();

    }

    void Update()
    {
        CalculateVelocity();
        HandleWallSliding();
        HandleEdgeGrab();
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

    

    public override void OnJumpInputDown()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
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

    

    public void OnHitInput()
    {
        if (!sword.cooldown)
        {
            sword.HurtboxActivation();
            sword.StartCooldown();
            Invoke("HurtboxDeactivate", 0.2f);
            Invoke("RefreshSCooldown", 0.5f);
        }

    }

    void HurtboxDeactivate()
    {
        sword.HurtboxDeactivate();
    }

    void RefreshSCooldown()
    {
        sword.RefreshCooldown();
    }

    public void OnDeflectInput()
    {
        if (!deflect.cooldown)
        {
            deflect.DeflectboxActivation();
            deflect.StartCooldown();
            Invoke("DeflectboxDeactivate", 0.2f);
            Invoke("RefreshDCooldown", 0.5f);
        }

    }

    void DeflectboxDeactivate()
    {
        deflect.DeflectboxDeactivate();
    }

    void RefreshDCooldown()
    {
        deflect.RefreshCooldown();
    }



    void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

    }

    void HandleEdgeGrab()
    {
       
    }

  
}