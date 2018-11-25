using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {

    [SerializeField] private Image player1_Health;
    [SerializeField] private Image player1_Energy;
    [SerializeField] private Image player2_Health;
    [SerializeField] private Image player2_Energy;

    [SerializeField] private Color fullHealth;
    [SerializeField] private Color halfHealth;
    [SerializeField] private Color lowHealth;

    private Player player1;
    private Player player2;

    void Awake() {
        EventsManager.Subscribe(EventsManager.EventType.PlayerSpawn, OnPlayerSpawn);
        EventsManager.Subscribe(EventsManager.EventType.PlayerDeath, OnPlayerDeath);
    }

    private void OnDestroy() {
        EventsManager.Unsubscribe(EventsManager.EventType.PlayerSpawn, OnPlayerSpawn);
        EventsManager.Unsubscribe(EventsManager.EventType.PlayerSpawn, OnPlayerDeath);
    }

    private void OnPlayerSpawn(object o) {
        var player = (Player)o;
        var number = player.playerNumber;
        if (number == Player.PlayerNumbers.player1) {
            player1 = player;
        }
        if (number == Player.PlayerNumbers.player2) {
            player2 = player;
        }
    }

    private void CalculateUI() {
        if (player1 != null) {
            player1_Health.fillAmount = player1.CurrentHealth / player1.maxHealth;
            player1_Energy.fillAmount = player1.currentEnergy / player1.maxEnergy;
            if (player1_Health.fillAmount < 0.3)
                player1_Health.color = lowHealth;
            else if
                (player1_Health.fillAmount < 0.6) player1_Health.color = halfHealth;
            else
                player1_Health.color = fullHealth;
        }
        if (player2 != null) {
            player2_Health.fillAmount = player2.CurrentHealth / player1.maxHealth;
            player2_Energy.fillAmount = player2.currentEnergy / player1.maxEnergy;
            if (player2_Health.fillAmount < 0.3)
                player2_Health.color = lowHealth;
            else if (player2_Health.fillAmount < 0.6)
                player2_Health.color = halfHealth;
            else
                player2_Health.color = fullHealth;
        }
    }

    private void Update() {
        CalculateUI();
    }

    private void playerUiUpdate(Player player) {
        player1_Health.fillAmount = player.CurrentHealth / player.maxHealth;
        player1_Energy.fillAmount = player.currentEnergy / player.maxEnergy;
        if (player1_Health.fillAmount < 0.2) player1_Health.color = lowHealth;
        else if (player1_Health.fillAmount < 0.6) player1_Health.color = halfHealth;
        else player1_Health.color = fullHealth;
    }

    private void OnPlayerDeath(object o) {
        var player = (Player)o;
        var number = player.playerNumber;
        if (number == Player.PlayerNumbers.player1) {
            player1 = null;
        }
        if (number == Player.PlayerNumbers.player1) {
            player2 = null;
        }
    }
}
