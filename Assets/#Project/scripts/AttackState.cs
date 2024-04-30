using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IStateRat
{

    Transform[] targets;
    NavMeshAgent ratAgent;
    Transform nextDestination;
    Transform rat;
    Transform player;
    IStateRatMachine machine;

    public AttackState()
    {
    }
    public AttackState(Transform target,NavMeshAgent ratAg, Transform rat,Transform player,IStateRatMachine machine)
    {
        targets = target.GetComponentsInChildren<Transform>();;
        this.ratAgent = ratAg;
        this.rat = rat;
        this.player = player;
        this.machine = machine; 
    }
   public void Enter()
    {
        Debug.Log("KILL YES YES");
        nextDestination = player;
        ratAgent.SetDestination(nextDestination.position);
        ratAgent.speed = 6;
    }

    public void Perform()
    {
       
    }

    public void Exit()
    {
        
    }
     public void Touche(Collision other)
    {
        if(other.gameObject.GetComponent<Transform>() == player)
        {
            player.gameObject.GetComponent<Player>().TakeDmg();
        }
}
}
