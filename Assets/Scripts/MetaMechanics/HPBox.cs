using UnityEngine;

public class HPBox : MonoBehaviour
{
    private void Start()
    {
        // Уничтожить объект через 10 секунд
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter playerCharacter = other.GetComponent<PlayerCharacter>();
        if (playerCharacter != null)
        {
            if (playerCharacter.Health < 100)
            {
                playerCharacter.IncreaseHealth(10);
                // Уничтожить аптечку после использования
                Destroy(gameObject);
            }
        }
    }
}
