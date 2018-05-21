using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGroundEnemyStates : MonoBehaviour {

    public abstract void Enter();

    public abstract void Execute();
	
    public Vector2 FindPlayer()
    {
        Vector2 playerLocation;

        playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;

        return playerLocation;
    }
}

public class Chase : BaseGroundEnemyStates{

    public override void Enter()
    {
        Vector2 enemyPosition=this.transform.position;

        Vector2 vectorToPlayer = FindPlayer() - enemyPosition;


        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

}
