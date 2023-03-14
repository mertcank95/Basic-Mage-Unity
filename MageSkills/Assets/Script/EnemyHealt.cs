using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealt : MonoBehaviour
{
    [HideInInspector] public float currentHealt;
    [SerializeField] private float maxHealth = 100f;
    private Animator anim;
    [SerializeField] private Image enemyHealtBar;
    private BoxCollider boxCollider;
    private void Awake()
    {
        currentHealt = maxHealth;
        anim = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    public void TakeDamage(float amount)
    {
        currentHealt -= amount;
        enemyHealtBar.fillAmount = currentHealt / maxHealth;
        if (currentHealt > 0)
        {
            anim.SetTrigger("Hit");
        }
        if (currentHealt <= 0)
        {
            Canvas canvas = enemyHealtBar.gameObject.GetComponentInParent<Canvas>();
            anim.SetTrigger("Death");
        }

    }
}
