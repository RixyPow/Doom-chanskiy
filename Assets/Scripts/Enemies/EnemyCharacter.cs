using System.Collections;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);

        StartCoroutine(FlashWhite());

        if (currentHealth <= 0)
        {
            StartCoroutine(DieWithDelay());
        }
    }

    private IEnumerator FlashWhite()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            renderer.material.color = originalColor;
        }
    }

    private IEnumerator DieWithDelay()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.black;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
