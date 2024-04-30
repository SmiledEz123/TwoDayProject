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
        agent = ratAgent;
        this.target = target;
    }
    public LookingToEatState()
    {
    }

    public void Enter()
    {
        agent.SetDestination(target.position);
    }

    public void Perform()
    {
       
    }

    public void Exit()
    {
        
    }
    public void Touche(Collision other)
    {
        Debug.Log("touché3");
        Debug.Log(other.gameObject.GetComponent<Transform>() == target);
        if(other.gameObject.GetComponent<Transform>() == target)
        {
            Debug.Log("touché4");
            target.gameObject.GetComponent<RatBehavior>().Revive();
        }
}}
