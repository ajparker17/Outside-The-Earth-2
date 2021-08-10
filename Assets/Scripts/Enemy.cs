using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public GameObject enemy;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(enemy);
    }

}
