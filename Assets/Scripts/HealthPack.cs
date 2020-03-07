using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healBy = 1;
    public float pickUpDelay = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUpDelay > 0) {
            pickUpDelay -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && pickUpDelay <= 0) {
            PlayerHealthController.instance.HealPlayer(healBy);
            AudioManager.instance.PlaySFX(7);
            Destroy(gameObject);
        }
    }
}
