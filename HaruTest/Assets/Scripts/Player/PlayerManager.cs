using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        public bool isInteracting;
        
        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;

        //需要在该脚本中加入地面检测，不然会在空中做动作（后面还会检测是否Dead，不然死后还会做动作）
        public bool isGrounded;

        void Start()
        {
            cameraHandler = CameraHandler.singleton;
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }


        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            anim.SetBool("isInAir", isInAir);

            inputHandler.CameraInput(delta);
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleJumping();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
            }
        }

        private void LateUpdate()
        {
            cameraHandler.HandleCameraRotation(Time.deltaTime, inputHandler.mouseX, inputHandler.mouseY);

            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.jump_Input = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }


    }
}
