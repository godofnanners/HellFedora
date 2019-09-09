using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundedEnemy))]
public class FiniteEnemy : MonoBehaviour {

    GroundedEnemy gEnemy;
    GameObject player;
    SwordRotation sR;
    const int left = -1;
    const int right = 1;
    bool playerVisible = false;

    public bool activeMovement = true;
    public bool stunned,resting,hasAttacked;
    float jump = 0;
    float facing;

    public float restingTime,stunnedtime;
    float ogRestingTime;
    public float direction;
    
    float attackMagnitude;
    [SerializeField] float attackLenght;
    public Vector3[] localPatrolpoints;
    Vector3[] globalPatrolpoints;
    int pIndex;
    public State currentEnemyState;

    public enum State {Attack,Patrol,Chase,Recovering,Dead };

    void Start()
    {
        ogRestingTime = restingTime;
        currentEnemyState = State.Patrol;

        gEnemy = GetComponent<GroundedEnemy>();
        stunnedtime = gEnemy.recoverTime;
        
        player = GameObject.FindGameObjectWithTag("Player");

        globalPatrolpoints = new Vector3[localPatrolpoints.Length];
        for (int i = 0; i < localPatrolpoints.Length; i++)
        {
            globalPatrolpoints[i] = localPatrolpoints[i] + transform.position;
        }

        pIndex = 0;
    }

    void FixedUpdate()
    {
        
        Debug.DrawRay(transform.position, transform.forward,Color.blue);
        switch (currentEnemyState)
        {
            case State.Patrol:
                if (DistanceToPlayer() <= gEnemy.visionRange )
                {
                    
                    currentEnemyState = State.Chase;
                }
                Patrol(ref direction,ref gEnemy.moveSpeed);
                break;
            case State.Chase:
                if (DistanceToPlayer() < gEnemy.attackRange)
                {
                    currentEnemyState = State.Attack;
                }
                else if(DistanceToPlayer() > gEnemy.visionRange)
                {
                    currentEnemyState = State.Patrol;
                }
                Chase(ref direction,ref gEnemy.moveSpeed);
                break;
            case State.Recovering:
                if (!stunned)
                {
                    Timer(ref restingTime);
                    if (restingTime<=0)
                    {
                        restingTime = ogRestingTime;
                        currentEnemyState = State.Patrol;
                    }
                }
                else
                {
                    Timer(ref stunnedtime);
                    if (stunnedtime <= 0)
                    {
                        stunnedtime = gEnemy.recoverTime;
                        activeMovement = true;
                        stunned = false;
                        currentEnemyState = State.Patrol;

                    }
                }
                

                break;
            case State.Attack:

                Debug.Log(gEnemy.IsOnGround());


                if (gEnemy.IsOnGround() && hasAttacked)
                {
                    hasAttacked = false;
                    currentEnemyState = State.Recovering;
                }  
                else if (gEnemy.IsOnGround())
                {
                    Attack(ref direction, ref jump, attackLenght);
                    hasAttacked = true;
                }

                
                
                break;
            case State.Dead:
                Dead();
                break;

        }

        
        //Debug.Log(currentEnemyState);
        //Debug.Log(activeMovement);

        if (activeMovement)
        {
            Vector2 directionalInput = new Vector2(direction, jump);
            gEnemy.SetDirectionalInput(directionalInput);

        }

        if (direction != 0)
        {
            facing = direction;
        }

        jump = 0;
        direction = 0;

        

    }

    

    public void Attack(ref float direction,ref float jump,float attackRechargeTime)
    {
        Vector2 enemyPosition;
        Vector2 vectorToPlayer;

        enemyPosition = this.transform.position;
        vectorToPlayer = FindPlayer() - enemyPosition;

        direction = vectorToPlayer.x / Mathf.Abs(vectorToPlayer.x);

        jump = 1;

        
        //gEnemy.BurstVelocity(attackVec);       
        
    }

    

    public void Patrol(ref float direction, ref float speed)
    {

        if (Mathf.Abs(transform.position.x - globalPatrolpoints[pIndex].x) < 0.5)
        {
            Debug.Log("change");
            if (pIndex == 0)
                pIndex = 1;
            else
                pIndex = 0;
        }
        speed = 3;
        direction = Mathf.Sign(globalPatrolpoints[pIndex].x - transform.position.x);
    }

    public void Chase(ref float direction, ref float speed)
    {
        Vector2 enemyPosition;
        Vector2 vectorToPlayer;

        enemyPosition = this.transform.position;
        vectorToPlayer = FindPlayer() - enemyPosition;


        speed = 4;
        direction = vectorToPlayer.x / Mathf.Abs(vectorToPlayer.x);
    }

    public void Deflected(Vector2 newVelocity,float magnitude)
    {
        if(currentEnemyState != State.Recovering)
        {
            stunned = true;
            hasAttacked = false;
            currentEnemyState = State.Recovering;

            gEnemy.BurstVelocity(newVelocity * magnitude);
        }
                
    }

    public IEnumerator Recover(float recoverTime)
    {
        Debug.Log("start");
        yield return new WaitForSeconds(recoverTime);

        
    }

    public void Stunned(float stunTime)
    {
        currentEnemyState = State.Recovering;
        activeMovement = false;

        StartCoroutine(Recover(stunTime));
    }

    private void Dead()
    {
        GameObject.Destroy(this);
    }

    float DistanceToPlayer()
    {
        return (player.transform.position - transform.position).magnitude;
    }

    public void Timer(ref float time)
    {
        time -= Time.deltaTime;
    }

    Vector2 FindPlayer()
    {
        return player.transform.position;
    }

    

    private void OnDrawGizmos()
    {
        if (localPatrolpoints != null)
        {
            Gizmos.color = Color.red;
            float size = .3f;

            for (int i = 0; i < localPatrolpoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalPatrolpoints[i] : localPatrolpoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }



    
    
    



}
