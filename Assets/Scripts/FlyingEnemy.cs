using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MovingCreature {

    bool flying;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        flying = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void CalculateVelocity()
    {

        float targetVelocityX = directionalInput.x * moveSpeed;
        float targetVelocityY = directionalInput.y * moveSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        if (!flying)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        


    }
}
