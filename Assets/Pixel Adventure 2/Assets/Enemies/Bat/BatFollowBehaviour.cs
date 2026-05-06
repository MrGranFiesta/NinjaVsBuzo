using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BatFollowBehaviour : StateMachineBehaviour
{
    Bat bat;
    [SerializeField] float speed = 3f;
    [SerializeField] float timeFollow = 0f;
    private int debuger = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("BatFollowBehaviour - OnStateEnter");
        bat = animator.gameObject.GetComponent<Bat>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("BatFollowBehaviour - OnStateUpdate");
        Transform playerNearby = bat.GetPlayerNearby();
        
        if (playerNearby == null) {
            return;
        }

        Vector3 playerPos = playerNearby.position;

        animator.transform.position = Vector2.MoveTowards(
            animator.transform.position,
            playerPos,
            speed * Time.deltaTime
        );

        bat.FlipX(playerPos);
        timeFollow -= Time.deltaTime;
        if (timeFollow <= 0)
        {
            Debug.Log("Volver " + ++debuger);
            animator.SetTrigger("Volver");
            timeFollow = 3f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
