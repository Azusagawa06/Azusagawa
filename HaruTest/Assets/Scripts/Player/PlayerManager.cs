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
        //public bool canCombo;

        //��Ҫ�ڸýű��м�������⣬��Ȼ���ڿ��������������滹�����Ƿ�Dead����Ȼ���󻹻���������
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
            //canCombo = anim.GetBool("canCombo"); //��ȡ���������п�ִ��combo��ʱ��

            isGrounded = playerLocomotion.GroundDetection();

            if (isGrounded)
            {
                inputHandler.CameraInput(delta);
                inputHandler.TickInput(delta);
                playerLocomotion.HandleMovement(delta);
                playerLocomotion.HandleRollingAndSprinting(delta);
                playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            }
            else
            {
                inputHandler.CameraInput(delta);
                playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            }
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

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }


    }
}
