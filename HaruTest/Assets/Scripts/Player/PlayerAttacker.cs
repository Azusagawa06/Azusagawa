using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AtkInfo
    {
        //对应不同的攻击片段
        public int id = 0;
        public string canCombo;
        public bool isFirst = false;
        public AtkInfo(int id, string canCombo, bool isFirst)
        {
            this.id = id;
            this.canCombo = canCombo;
            this.isFirst = isFirst;
        }
    }

    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        PlayerManager playerManager;

        //public string lastAttack;
        public Dictionary<string, AtkInfo> dic;
        private int count = 0;
        public Animator anim;
        public bool isGrounded;
        public bool canCombo;

        AnimatorStateInfo state;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
            anim = transform.GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            state = anim.GetCurrentAnimatorStateInfo(1);
            dic = new Dictionary<string, AtkInfo>();

            dic.Add("AT1", new AtkInfo(1, "canCombo1", true));
            dic.Add("AT2", new AtkInfo(2, "canCombo2", false));
            dic.Add("AT3", new AtkInfo(3, "canCombo3", false));
            dic.Add("AT4", new AtkInfo(4, "canCombo4", false));
        }

        private void Update()
        {
            isGrounded = playerLocomotion.GroundDetection();
            state = anim.GetCurrentAnimatorStateInfo(1);

            if (isGrounded)
            {
                HandleCombo();
            }

        }

        public void HandleSkillAttack()
        {
            if (playerManager.isInteracting)
                return;

            animatorHandler.PlayTargetAnimation("ATH", true);
        }

        public void CheckCombo()
        {
            if (state.IsName("Empty"))
            {
                animatorHandler.AttackAnimation(true, true);
                count = 1;
            }
            else
            {
                foreach (var item in dic)
                {
                    if (state.IsName("AT1"))
                    {
                        if (state.normalizedTime > 0.3)
                        {
                            if (anim.GetBool(item.Value.canCombo))
                            {
                                animatorHandler.anim.SetBool((item.Value.canCombo), false);
                            }

                            if (state.IsName(item.Key))
                            {
                                count = item.Value.id + 1;
                                //Debug.Log(count);
                            }
                        }
                    }
                    else if (state.IsName("AT4"))
                    {
                        if (state.normalizedTime > 0.6)
                        {
                            if (anim.GetBool(item.Value.canCombo))
                            {
                                animatorHandler.anim.SetBool((item.Value.canCombo), false);
                            }

                            if (state.IsName(item.Key))
                            {
                                count = item.Value.id + 1;
                                //Debug.Log(count);
                            }
                        }
                    }
                    else
                    {
                        if (anim.GetBool(item.Value.canCombo))
                        {
                            animatorHandler.anim.SetBool((item.Value.canCombo), false);
                        }

                        if (state.IsName(item.Key))
                        {
                            count = item.Value.id + 1;
                            //Debug.Log(count);
                        }
                    }

                }
            }
        }

        public void HandleCombo()
        {
            if (!state.IsName("Empty"))
            {
                anim.SetBool("doCombo", false);
            }

            foreach (var item in dic)
            {
                if (state.IsName(item.Key) && (count == item.Value.id + 1))
                {
                    animatorHandler.AttackAnimation(true, true);
                }
            }
        }

        //public void HandleLightAttack(WeaponItem weapon)
        //{
        //    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
        //    lastAttack = weapon.OH_Light_Attack_1;
        //}

        //public void HandleHeavyAttack(WeaponItem weapon)
        //{
        //    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
        //    lastAttack = weapon.OH_Heavy_Attack_1;
        //}

        //public void HandleWeaponCombo(WeaponItem weapon)
        //{
        //    if (inputHandler.comboFlag)
        //    {
        //        animatorHandler.anim.SetBool("canDoCombo", false);

        //        if (lastAttack == weapon.OH_Light_Attack_1)
        //        {
        //            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
        //        }
        //    }
        //}
    }
}