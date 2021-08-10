using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectorDamage : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
            callNextScene();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void callNextScene()
    {
        SceneManager.LoadScene(3);
    }

    void LoadNextScene()
    {
        Invoke("callNextScene", 6f);
    }
}
