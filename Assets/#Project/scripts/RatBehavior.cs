using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatBehavior : MonoBehaviour
{
    NavMeshAgent rat;
    [SerializeField]Transform targets;
    [SerializeField]Transform familly;
    [SerializeField]Transform playerT;

    Transform theChosenRat;
    
    IStateRatMachine stateManager;
    // Start is called before the first frame update
    void Start()
    {
        rat = this.GetComponent<NavMeshAgent>();
        stateManager = new IStateRatMachine(targets,this.gameObject.GetComponent<Transform>(),playerT,rat);
        stateManager.TransitionTo(stateManager.idleState);
        theChosenRat = this.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(theChosenRat.GetComponent<RatBehavior>().stateManager.CurrentState == stateManager.idleState)
            {
        while(theChosenRat == rat)
        {
            theChosenRat = rat.parent.GetChild(Random.Range(1,rat.parent.childCount));
        }
        }
        */

        if(familly.childCount >= 20)
        {
            stateManager.TransitionTo(stateManager.attackState);
        }
        else if(CanSeePlayer())
        {
            if(stateManager.CurrentState != stateManager.runingAwayState && stateManager.CurrentState != stateManager.deadState && stateManager.CurrentState != stateManager.attackState && stateManager.CurrentState != stateManager.lookingToEatState)
            {
            stateManager.TransitionTo(stateManager.runingAwayState);
            }
        }
        else if(stateManager.CurrentState == stateManager.deadState && theChosenRat.GetComponent<RatBehavior>().stateManager.CurrentState != stateManager.lookingToEatState)
        {
            CallForHelp();
        }
        else if(stateManager.CurrentState == stateManager.lookingToEatState)
        {
        }
        stateManager.Perform();
    }


    
    public void CallForHelp()
    {
        
            theChosenRat = this.GetComponent<Transform>().parent.GetChild(Random.Range(1,this.GetComponent<Transform>().parent.childCount));
            int i=0;
        while(theChosenRat == this.GetComponent<Transform>() && i!=10)
        {
            if(theChosenRat.GetComponent<RatBehavior>().stateManager.CurrentState == stateManager.idleState && i != 100)
        {
            theChosenRat = this.GetComponent<Transform>().parent.GetChild(Random.Range(1,this.GetComponent<Transform>().parent.childCount));
        }
        }
        theChosenRat.GetComponent<RatBehavior>().theChosenRat = this.GetComponent<Transform>();
        theChosenRat.GetComponent<RatBehavior>().stateManager.TransitionTo(stateManager.lookingToEatState);
    }


    public void Die()
    {
        stateManager.TransitionTo(stateManager.deadState);
    }
    public void Revive()
    {
        stateManager.TransitionTo(stateManager.idleState);
    }

    private bool CanSeePlayer()
    {
        Vector3 enemyFacing= transform.forward;
        Vector3 enemyPos=transform.position;
        Vector3 enemyToPlayer = playerT.position - enemyPos;

        RaycastHit hit;
        Debug.DrawRay(enemyPos,enemyFacing,Color.red);
        if(Physics.Raycast(enemyPos+new Vector3(0,0.3f,0),enemyToPlayer+new Vector3(0,0.3f,0),out hit,30f))
        {
            if(hit.collider.CompareTag("Player"))
            {

                    return true;
            }
        }
        return false;
    }
}
