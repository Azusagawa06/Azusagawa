using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool jump_Input;


        public bool rollFlag;
        public bool sprintFlag;
        //public bool comboFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleJumpInput(delta);
        }

        public void CameraInput(float delta)
        {
            //还是得GetAxis不然inputaction输入的视野移动指令可能跟不上实际高速移动鼠标时的速度
            //mouseX = cameraInput.x;
            //mouseY = cameraInput.y;

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }
        
        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }        

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            inputActions.PlayerActions.RT.performed += i => rt_Input = true;

            if (rb_Input)
            {
                playerAttacker.CheckCombo();

                //else
                //{
                //    if (playerManager.isInteracting)
                //        return;

                //    if (playerManager.canCombo)
                //        return;

                //    playerAttacker.HandleNormalAttack();
                //}
            }

            if (rt_Input)
            {
                playerAttacker.HandleSkillAttack();
            }

            #region Video Type
            //if (rb_Input)
            //{
            //    if (playerManager.canDoCombo)
            //    {
            //        comboFlag = true;
            //        playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
            //        comboFlag = false;
            //    }
            //    else
            //    {
            //        if (playerManager.isInteracting)
            //            return;

            //        if (playerManager.canDoCombo)
            //            return;

            //        playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            //    }


            //}

            //if (rt_Input)
            //{
            //    playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            //}
            #endregion

        }

        private void HandleJumpInput(float delta)
        {
            inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
        }
    }
}

