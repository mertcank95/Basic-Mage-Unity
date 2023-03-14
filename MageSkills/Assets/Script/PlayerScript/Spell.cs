using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10;
    EnemyHealt enemyHealt;
    bool colided;
    public float speed = 13f;
    public GameObject explosion;
    internal virtual void Update()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));

        foreach (Collider hit in hits)
        {
            enemyHealt = hit.gameObject.GetComponent<EnemyHealt>();
            colided = true;
            if (colided)
            {
                enemyHealt.TakeDamage(damageCount);
                enabled = false;
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

   
    
    
}
