using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    //Finds NavMeshAgent and player Transform
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;

    //Player and ground layermasks
    public LayerMask whatIsGround, whatIsPlayer;

    //walkPoint variables
    public Vector3 walkPoint;
    bool walkPoinSet;
    public float walkPointRange;

    //Attack variables
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //Range variables
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Shooting variables
    public float damage;
    public float range;

    private void Awake()
    { 
        //Finds player and NavMeshAgent 
        player = GameObject.Find("Capsule").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Update()
    {
        //Checks if player is in their sight and attack
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //Checks if the enemy should patrol, chase or attack
        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }


    private void Patroling()
    {
        //calls on the function to find walkpoint
        if (!walkPoinSet) SearchWalkPoint();

        //Moves towards walkpoint
        if (walkPoinSet)
        {
            agent.SetDestination(walkPoint);
        }

        //Finds the distance of walkPoint from the enemy
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //When the distance to the walk point is less than 1 set walkPoint to false
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPoinSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Finds walkPoint
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //Turns walkPoint into a Vector3
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Checks if walkPoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPoinSet = true;
        }
    }

    private void ChasePlayer()
    {
        //Chases the player
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Makes enemy still   
        agent.SetDestination(transform.position);

        //Looks at player
        transform.LookAt(player);

        //checks if attack is ready and calls on the shoot function
        if(!alreadyAttacked)
        {
           EnemyShoot();

            //Unreadies attack and activates the ResetAttack() function when a certain time (timeBetweenAttacks) passes
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        //Resets attack
        alreadyAttacked = false;
    }
    public void EnemyShoot()
    {
            //Gets RayCastHit name variable
            RaycastHit shot;
            //Shoots raycast
            if(Physics.Raycast(transform.position, transform.forward, out shot, range))
            {
                //Finds the PlayerInfo of the player
                PlayerInfo HitPlayer = shot.transform.GetComponent<PlayerInfo>();

                //Activates the EnemyHit function on the PlayerInfo script if the object does have a PlayerInfo script
                if (HitPlayer != null)
                {
                    
                    HitPlayer.EnemyHit(damage);
                }
            }
    }
}

