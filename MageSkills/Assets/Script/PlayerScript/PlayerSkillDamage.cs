using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10;
    EnemyHealt enemyHealt;
    protected bool colided;
    
    internal virtual void Update()
    {
       
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider hit in hits)
        {
            enemyHealt = hit.gameObject.GetComponent<EnemyHealt>();
            colided = true;
            if (colided)
            {
                enemyHealt.TakeDamage(damageCount);
                enabled = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
