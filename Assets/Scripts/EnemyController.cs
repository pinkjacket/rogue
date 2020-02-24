using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D enRB;
    public float moveSpeed;
    public float attentionRange;
    private Vector3 moveDirection;
    public Animator anim;
    public int health = 150;
    public GameObject[] deathEffects;
    public GameObject hitEffect;
    public bool doesShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < attentionRange) {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        } else {
            moveDirection = Vector3.zero;
        }
        moveDirection.Normalize();

        enRB.velocity = moveDirection * moveSpeed;

        if (moveDirection != Vector3.zero) {
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }

        if (doesShoot) {
            fireCount -= Time.deltaTime;

            if(fireCount <= 0) {
                fireCount = fireRate;
                Instantiate(bullet, firePoint.position, firePoint.rotation);

            }
        }
    }


    public void DamageEnemy(int damage) {
        health -= damage;
        Instantiate(hitEffect, transform.position, transform.rotation);

        if(health <= 0) {
            Destroy(gameObject);

            int selectedEffect = Random.Range(0, deathEffects.Length);
            int rotation = Random.Range(0, 4);

            Instantiate(deathEffects[selectedEffect], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

            //Instantiate(deathEffects, transform.position, transform.rotation);
        }
    }
}
