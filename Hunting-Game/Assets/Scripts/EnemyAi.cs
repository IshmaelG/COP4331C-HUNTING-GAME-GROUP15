using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
  //Don't have animations atm...
  //private Animator anim;
  private NavMeshAgent navAgent;
  private NavMeshHit navHit;
  private Vector3 currentDestination;

  [SerializeField]
  private float maxWalkDistance = 50f;

  private void Awake()
  {
      //anim = GetComponent<Animator>();
      navAgent = GetComponent<NavMeshAgent>();
      SetNewDestination();
  }

  private void Update()
  {
      //AnimatePlayer();
      CheckIfAgentReachedDestination();
  }

  /*void AnimatePlayer()
  {
      if (navAgent.velocity.magnitude > 0)
      {
          anim.SetBool("Walk", true);
      }
      else
          anim.SetBool("Walk", false);
  }*/

  void SetNewDestination()
  {
      while (true)
      {
          NavMesh.SamplePosition(((Random.insideUnitSphere * maxWalkDistance) + transform.position),
              out navHit, maxWalkDistance, -1);

          if (currentDestination != navHit.position)
          {
              currentDestination = navHit.position;
              navAgent.SetDestination(currentDestination);
              break;
          }
      }
  }

  void CheckIfAgentReachedDestination()
  {
      if (!navAgent.pathPending)
      {
          if (navAgent.remainingDistance <= navAgent.stoppingDistance)
          {
              if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
              {
                  SetNewDestination();
              }
          }
      }
  }
}
