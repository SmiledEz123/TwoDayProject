using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookingToEatState : IStateRat
{
    NavMeshAgent agent;
    Transform target;
    public LookingToEatState(NavMeshAgent ratAgent,Transform target)
    {
        Debug.Log(ratAgent,target);
        agent = ratAgent;
        this.target = target;
    }
    public LookingToEatState()
    {
         Debug.Log("what");
    }

    public void Enter()
    {
        Debug.Log("YEY");
        agent.SetDestination(target.position);
    }

    public void Perform()
    {
       
    }

    public void Exit()
    {
        
    }
}
