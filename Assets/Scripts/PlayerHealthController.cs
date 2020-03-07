using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;
    public float invulnLength = 1f;
    private float invulnCount;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnCount > 0) {
            invulnCount -= Time.deltaTime;
            if(invulnCount <= 0) {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        }
    }

    public void DamagePlayer() {
        if (invulnCount <= 0) {

            currentHealth--;
            AudioManager.instance.PlaySFX(11);
            invulnCount = invulnLength;

            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
            if (currentHealth <= 0) {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
                AudioManager.instance.PlaySFX(8);
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void TempInvulnerability(float length) {
        invulnCount = length;
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
    }

    public void HealPlayer(int healthBoost) {
        currentHealth += healthBoost;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
