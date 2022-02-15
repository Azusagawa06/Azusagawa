using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ColliderAttack : MonoBehaviour
    {
        public int currentAttackDamage = 25;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "PlayerHit")
            {
                PlayerStats playerStats = collision.GetComponentInParent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentAttackDamage);
                }
            }

            if (collision.tag == "EnemyHit")
            {
                EnemyStats enemyStats = collision.GetComponentInParent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentAttackDamage);
                }
            }
        }
    }
}