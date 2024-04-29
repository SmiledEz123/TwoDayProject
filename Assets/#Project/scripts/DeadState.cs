using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadState : IStateRat
{
    Transform rat;
    
    Transform theChosenRat;
    public DeadState(Transform rat)
    {
        this.rat = rat;
    }
   public void Enter()
    {
        Debug.Log("dead");
        rat.GetComponent<NavMeshAgent>().isStopped = true;
        theChosenRat = rat;
        rat.localEulerAngles = new Vector3(180, 0, 0);
    }

    public void Perform()
    {
        //wait for help
    }

    public void Exit()
    {
        Debug.Log("alive");
        rat.GetComponent<NavMeshAgent>().isStopped = false;
        
    }
}
