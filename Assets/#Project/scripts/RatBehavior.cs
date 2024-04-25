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
    
    IStateRatMachine stateManager;
    // Start is called before the first frame update
    void Start()
    {
        rat = this.GetComponent<NavMeshAgent>();
        stateManager = new IStateRatMachine(targets,this.gameObject.GetComponent<Transform>(),playerT,rat);
        stateManager.TransitionTo(stateManager.idleState);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(je suis mort)
        {
            faireLeMort;
        }
        else if(veuxtuerjoueur)
        {
            tuerJoueur;
        }
        else if(voisjoueur)//si il ne veux pas tuer le joueur
        {
            ildoismanger = faux;
            fuire;
        }else if(ildoismanger)//si il ne veux pas tuer le joueur et ne le vois pas
        {
            allermanger;
        }
        else//si il ne veux pas tuer le joueur et ne le vois pas et ne veut pas manger
        {
        stateManager.Perform();
        }*/
        if(stateManager.CurrentState == stateManager.deadState)
        {
            stateManager.Perform();
        }
        else if(familly.childCount >= 20)
        {
            stateManager.TransitionTo(stateManager.attackState);
            stateManager.Perform();
        }
        else if(CanSeePlayer() || stateManager.CurrentState == stateManager.runingAwayState)
        {
            if(stateManager.CurrentState != stateManager.runingAwayState)
            {
            stateManager.TransitionTo(stateManager.runingAwayState);
            }
            stateManager.Perform();
        }
        else if(stateManager.CurrentState == stateManager.lookingToEatState)
        {
            stateManager.Perform();
        }
        else
        {
            if(stateManager.CurrentState != stateManager.idleState)
            {
            stateManager.TransitionTo(stateManager.idleState);
            }
            stateManager.Perform();
        }
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
