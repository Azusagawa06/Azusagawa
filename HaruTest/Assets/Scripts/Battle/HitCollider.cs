using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class HitCollider : MonoBehaviour
    {
        Collider hitCollider;

        private void Awake()
        {
            hitCollider = GetComponent<Collider>();
            hitCollider.gameObject.SetActive(true);
            hitCollider.isTrigger = false;
            hitCollider.enabled = true;
        }
    }
}