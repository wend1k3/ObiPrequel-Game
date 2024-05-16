using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBooleanStateMachineBehaviour : StateMachineBehaviour
{
    public string booleanName;
    public bool setOnStateMachineEnterAndExit;
    public bool trueWhileInMachine = true;

    public bool setOnStateEnterAndExit = false;
    public bool trueWhileInState = true;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (setOnStateEnterAndExit)
            animator.SetBool(booleanName, trueWhileInState);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (setOnStateEnterAndExit)
            animator.SetBool(booleanName, !trueWhileInState);
    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (setOnStateMachineEnterAndExit)
            animator.SetBool(booleanName, trueWhileInMachine);
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (setOnStateMachineEnterAndExit)
            animator.SetBool(booleanName, !trueWhileInMachine);
    }
}
