using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D bulRB;
    public GameObject impactEffect;
    public int bulletDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        if(other.tag == "Enemy") {
            other.GetComponent<EnemyController>().DamageEnemy(bulletDamage);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
