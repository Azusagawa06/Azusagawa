using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AnimatorHandler : MonoBehaviour
    {
        PlayerManager playerManager;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            anim = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");

        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            #endregion

            if (isSprinting && inputHandler.moveAmount > 0)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        //攻击动画用CrossFade来执行可能会无法被GetCurrentAnimatorStateInfo检测到
        public void AttackAnimation(bool doCombo, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("doCombo", doCombo);
            anim.SetBool("isInteracting", isInteracting);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }

        #region Action Combo Set
        public void EnableCombo1()
        {
            anim.SetBool("canCombo1", true);
        }

        public void DisableCombo1()
        {
            anim.SetBool("canCombo1", false);
        }

        public void EnableCombo2()
        {
            anim.SetBool("canCombo2", true);
        }

        public void DisableCombo2()
        {
            anim.SetBool("canCombo2", false);
        }

        public void EnableCombo3()
        {
            anim.SetBool("canCombo3", true);
        }

        public void DisableCombo3()
        {
            anim.SetBool("canCombo3", false);
        }

        public void EnableCombo4()
        {
            anim.SetBool("canCombo4", true);
        }

        public void DisableCombo4()
        {
            anim.SetBool("canCombo4", false);
        }
        #endregion

        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = velocity;
        }
    }
}