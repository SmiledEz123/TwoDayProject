using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IStateRat
{
    Transform[] targets;
    NavMeshAgent ratAgent;
    int nbrTargets;
    Transform nextDestination;
    Transform rat;
    public IdleState(Transform target,NavMeshAgent ratAg, Transform rat)
    {
        targets = target.GetComponentsInChildren<Transform>();;
        this.ratAgent = ratAg;
        this.rat = rat;
        nbrTargets = target.childCount;
        
    }
   public void Enter()
    {
        Debug.Log("MMOB");
        nextDestination = targets[Random.Range(1,nbrTargets)];
        ratAgent.SetDestination(nextDestination.position);
    }

    public void Perform()
    {
        if(Vector3.Distance(rat.position,nextDestination.position)<=1)
        {
        nextDestination = targets[Random.Range(1,nbrTargets)];
        ratAgent.SetDestination(nextDestination.position);
        }
    }

    public void Exit()
    {
        
    }
}
