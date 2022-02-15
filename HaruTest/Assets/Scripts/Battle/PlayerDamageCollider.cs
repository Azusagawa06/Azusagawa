using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerDamageCollider : MonoBehaviour
    {
        BoxCollider leftHandCollider;
        BoxCollider rightHandCollider;
        BoxCollider leftFootCollider;
        BoxCollider rightFootCollider;
        SphereCollider skillCollider;

        GameObject leftHand;
        GameObject rightHand;
        GameObject leftFoot;
        GameObject rightFoot;
        GameObject skill;

        private void Awake()
        {
            leftHand = GameObject.Find("Left Hand Attack").gameObject;
            rightHand = GameObject.Find("Right Hand Attack").gameObject;
            leftFoot = GameObject.Find("Left Foot Attack").gameObject;
            rightFoot = GameObject.Find("Right Foot Attack").gameObject;
            skill = GameObject.Find("Skill Attack").gameObject;

            leftHandCollider = leftHand.GetComponentInChildren<BoxCollider>();
            rightHandCollider = rightHand.GetComponentInChildren<BoxCollider>();
            leftFootCollider = leftFoot.GetComponentInChildren<BoxCollider>();
            rightFootCollider = rightFoot.GetComponentInChildren<BoxCollider>();
            skillCollider = GetComponentInChildren<SphereCollider>();

            leftHand.SetActive(true);
            leftHandCollider.isTrigger = true;
            leftHandCollider.enabled = false;

            rightHand.SetActive(true);
            rightHandCollider.isTrigger = true;
            rightHandCollider.enabled = false;

            leftFoot.SetActive(true);
            leftFootCollider.isTrigger = true;
            leftFootCollider.enabled = false;

            rightFoot.SetActive(true);
            rightFootCollider.isTrigger = true;
            rightFootCollider.enabled = false;

            skill.SetActive(true);
            skillCollider.isTrigger = true;
            skillCollider.enabled = false;
        }

        public void EnableLeftHandCollider()
        {
            leftHandCollider.enabled = true;
        }

        public void DisableLeftHandCollider()
        {
            leftHandCollider.enabled = false;
        }

        public void EnableRightHandCollider()
        {
            rightHandCollider.enabled = true;
        }

        public void DisableRightHandCollider()
        {
            rightHandCollider.enabled = false;
        }

        public void EnableLeftFootCollider()
        {
            leftFootCollider.enabled = true;
        }

        public void DisableLeftFootCollider()
        {
            leftFootCollider.enabled = false;
        }

        public void EnableRightFootCollider()
        {
            rightFootCollider.enabled = true;
        }

        public void DisableRightFootCollider()
        {
            rightFootCollider.enabled = false;
        }

        public void EnableSkillCollider()
        {
            skillCollider.enabled = true;
        }

        public void DisableSkillCollider()
        {
            skillCollider.enabled = false;
        }
    }
}