using UnityEngine;
using UnityEngine.SceneManagement;

public class serverDamage : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject obj1, obj2;

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
            obj1.SetActive(false);
            obj2.SetActive(true);
            callNextScene();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void callNextScene()
    {
        SceneManager.LoadScene(2);
    }

    void LoadNextScene()
    {
        Invoke("callNextScene",6f);
    }
}
