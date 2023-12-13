using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextDestroy : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform canvas = animator.transform.parent;
        Transform parentOfText = canvas.transform.parent;
        Destroy(parentOfText.gameObject);
    }
}
