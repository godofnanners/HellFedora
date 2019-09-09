using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyEnemy : MonoBehaviour {

    FlyingEnemy flyingE;
    GameObject player;
    float HP = 10;
    float dirInputProcent = 1;
    float activasionAmount;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        flyingE=GetComponent<FlyingEnemy>();
	}
	
	// Update is called once per frame
	void Update () {

        GetStates(ref flyingE.directionalInput);
	}

    void GetStates(ref Vector2 enemysDirInput)
    {
        
    }

    Vector2 GetClosertoPlayer()
    {
        Vector2 vectorToPlayer = transform.position - player.transform.position;

        vectorToPlayer=vectorToPlayer.normalized;

        return vectorToPlayer;
    }

    //Vector2 GetDistanceFromPlayer()
    //{

    //}

    //Vector2 GetClosertoPowerUp()
    //{

    //}
}
