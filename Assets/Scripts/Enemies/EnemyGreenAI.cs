using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    public int attackDamage = 10; // Урон атаки
    public float attackRange = 1f; // Дальность атаки
    public float attackDelay = 1f; // Задержка между атаками
    private float lastAttackTime = 0f; // Время последней атаки

    void Update()
    {
        // Проверяем, находится ли игрок в зоне атаки
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRange);
        
        foreach (var player in hitPlayers)
        {
            // Проверяем, имеет ли объект тег "Player"
            if (player.CompareTag("Player"))
            {
                // Если игрок в зоне атаки и задержка истекла, начинаем атаку
                if (Time.time >= lastAttackTime + attackDelay)
                {
                    Attack(player);
                    lastAttackTime = Time.time; // Обновляем время последней атаки
                }
                break; // Выходим из цикла, если атака произошла
            }
        }
    }

    void Attack(Collider player)
    {
        // Получаем компонент PlayerCharacter, чтобы вызвать метод Hurt
        PlayerCharacter playerHealth = player.GetComponent<PlayerCharacter>();
        if (playerHealth != null)
        {
            playerHealth.Hurt(attackDamage);
            Debug.Log("Игрок получил урон: " + attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация зоны атаки в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
