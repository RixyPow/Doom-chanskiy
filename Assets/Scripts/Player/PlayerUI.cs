using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text armorText;
    private PlayerCharacter player;

    private void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            healthText.text = "Health: " + player.Health;
            armorText.text = "Armor: " + player.Armor;
        }
    }
}
