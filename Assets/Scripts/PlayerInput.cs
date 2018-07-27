using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour {

    Player player;
    SwordRotation swordRot;
    

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        swordRot = GetComponent<SwordRotation>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 directionalInput= new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        player.SetDirectionalInput(directionalInput);
        
        if (Input.GetButtonDown("Jump"))
        {
            player.OnJumpInputDown();
        }
        if (Input.GetButtonUp("Jump"))
        {
            player.OnJumpInputUp();
        }

        if (Input.GetButton("Hit"))
        {
            player.OnHitInput();
        }
        if (Input.GetButton("Deflect"))
        {
            player.OnDeflectInput();
        }
    }
}
