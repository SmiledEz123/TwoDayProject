using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class IStateRatMachine
{
    public IStateRat CurrentState{get;private set;}
    public AttackState attackState;
    public IdleState idleState;
    public LookingToEatState lookingToEatState;
    public RuningAwayState runingAwayState;
    public DeadState deadState;
    Transform playerT;
    Transform targets;
    Transform rat;
    NavMeshAgent ratAg;
    public Transform ThechosenRat;

    public IStateRatMachine(Transform targets,Transform rat,Transform playerT,NavMeshAgent ratAg)
    {
        attackState = new AttackState();
        idleState = new IdleState(targets,rat.gameObject.GetComponent<NavMeshAgent>(),rat);
        lookingToEatState = new LookingToEatState();
        runingAwayState = new RuningAwayState();
        deadState = new DeadState(rat);
        this.playerT = playerT;
        this.rat = rat;
        this.targets = targets;
        this.ratAg = ratAg;
        
    }

    public void reciveChosenRat(Transform chosen)
    {
        ThechosenRat = chosen;
    }

    public void Perform()
    {
        CurrentState?.Perform();
    }

    public void TransitionTo(IStateRat nextState)
    {
        CurrentState?.Exit();

        if(CurrentState != runingAwayState && nextState == runingAwayState)
            {
                TransitionToRuningAwayState(nextState);
            }
            else if(CurrentState != lookingToEatState && ""+nextState == ""+lookingToEatState)
            {
                lookingToEatState = new LookingToEatState(ratAg,ThechosenRat);
                CurrentState = lookingToEatState;
            }
            else if(CurrentState != deadState && nextState == deadState)
            {
                CurrentState = nextState;
            }
            else if(CurrentState+"" != attackState+"" && nextState+"" == ""+attackState)
            {
                TransitionToAttackState(nextState);
            }
            else
            {
                CurrentState = nextState;}
        CurrentState.Enter();
    }

    private void TransitionToAttackState(IStateRat nextState)
    {
        attackState = new AttackState(targets,ratAg,rat,playerT,this);
        nextState = attackState;
        CurrentState = nextState;
    }
    private void TransitionToRuningAwayState(IStateRat nextState)
    {
        runingAwayState = new RuningAwayState(targets,ratAg,rat,playerT,this);
        nextState = runingAwayState;
        CurrentState = nextState;
    }

    public void Touche(Collision other)
    {
        Debug.Log("touche2");
        if(CurrentState+""==""+lookingToEatState)
        {   Debug.Log("touche2.2");
            CurrentState.Touche(other);
            TransitionTo(idleState);
        }
    }
}
