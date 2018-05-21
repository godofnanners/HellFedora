using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundedEnemy))]
public class FiniteEnemy : MonoBehaviour {

    GroundedEnemy gEnemy;
    BaseGroundEnemyStates chase, attack, patrol, heal;
    const int left = -1;
    const int right = 1;
    int jump = 0;

    void Start()
    {
        gEnemy = GetComponent<GroundedEnemy>();
    }

    void Update()
    {
        int direction=0;

        Vector2 directionalInput = new Vector2(direction, jump);
        gEnemy.SetDirectionalInput(directionalInput);
    }

    
}
