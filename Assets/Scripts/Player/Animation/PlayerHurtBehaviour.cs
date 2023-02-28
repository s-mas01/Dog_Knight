using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBehaviour : StateMachineBehaviour
{
    // �A�j���[�V�����J�n���Ɏ��s�����FStart�֐��̂悤�Ȃ���
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �ړ����x�E��]���x��x��������
        animator.ResetTrigger("Hurt");
        animator.GetComponent<PlayerManager>().moveSpeed = 0.005f;
        animator.GetComponent<PlayerManager>().rotateSpeed = 0.2f;
    }

    // �A�j���[�V�������Ɏ��s�����FUpdate�֐��̂悤�Ȃ���
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // �A�j���[�V�����̑J�ڂ��s����Ƃ��F
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���x�����ɖ߂�����
        animator.ResetTrigger("Hurt");
        animator.GetComponent<PlayerManager>().moveSpeed = 0.03f;
        animator.GetComponent<PlayerManager>().rotateSpeed = 0.7f;
    }

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