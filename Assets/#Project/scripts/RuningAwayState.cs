using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class RuningAwayState : IStateRat
{

    Transform[] targets;
    NavMeshAgent ratAgent;
    int nbrTargets;
    Transform nextDestination;
    Transform rat;
    Transform player;
    IStateRatMachine machine;

    public RuningAwayState()
    {
    }
    public RuningAwayState(Transform target,NavMeshAgent ratAg, Transform rat,Transform player,IStateRatMachine machine)
    {
        targets = target.GetComponentsInChildren<Transform>();;
        this.ratAgent = ratAg;
        this.rat = rat;
        nbrTargets = target.childCount;
        this.player = player;
        this.machine = machine; 
    }
    public void Enter()
    {
        Debug.Log("RUN");
        nextDestination = ChoseADestination();
        ratAgent.SetDestination(nextDestination.position);
    }

    public void Perform()
    {
       if(Vector3.Distance(rat.position,nextDestination.position)<=1)
        {
            machine.TransitionTo(machine.idleState);
        }
    }

    public void Exit()
    {
        
    }

      private Transform ChoseADestination()
    {
        Transform poss = targets[1];
        Transform poss2;
        foreach(Transform target in targets)
        {
            poss2 = target;
            if(Math.Abs(poss2.position.x - player.position.x) + Math.Abs(poss2.position.z - player.position.z) > Math.Abs(poss.position.x - player.position.x) + Math.Abs(poss.position.z - player.position.z))
            {
                poss = poss2;
            }

        }
        return poss;
    }
}
