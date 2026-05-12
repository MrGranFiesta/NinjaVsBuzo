    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatReturnBehaviour : StateMachineBehaviour
{
    [SerializeField] private float spreed;
    private Bat bat;
    private int debuger = 0; 


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bat = animator.gameObject.GetComponent<Bat>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("BatReturnBehaviour - OnStateUpdate");
        animator.transform.position = Vector2.MoveTowards(
            animator.transform.position,
            bat.PointInital,
            spreed * Time.deltaTime
        );
        bat.FlipX(bat.PointInital);
        Debug.Log("Posición actual: " + animator.transform.position + ", Punto inicial: " + bat.PointInital);
        if (animator.transform.position == bat.PointInital)
        {
            Debug.Log("Llego " + ++debuger);
            animator.SetTrigger("Llego");
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
