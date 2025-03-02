using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text armorText;
    [SerializeField] private TMP_Text ammoText; // Новое поле для отображения патронов
    [SerializeField] private TMP_Text fuelText; // Новое поле для отображения топлива огнемета
    private PlayerCharacter player;
    private MyScope scope; // Новое поле для доступа к скрипту MyScope

    private void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        scope = FindObjectOfType<MyScope>(); // Инициализация поля scope

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }

        if (scope == null)
        {
            Debug.LogError("Scope not found!");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            healthText.text = "Health " + player.Health;
            armorText.text = "Armor " + player.Armor;
        }

        if (scope != null)
        {
            if (scope._isReloading)
            {
                ammoText.text = scope._currentAmmo + " / " + scope._totalAmmo + " Reloading";
            }
            else
            {
                ammoText.text = scope._currentAmmo + " / " + scope._totalAmmo;
            }

            // Обновление текста топлива огнемета
            fuelText.text =scope.FlamethrowerFuel + " L";
        }
    }
}
