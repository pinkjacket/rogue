using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D enRB;
    public float moveSpeed;

    public bool chasePlayer;
    public float attentionRange;
    private Vector3 moveDirection;
    public bool runsAway;
    public float fleeRange;

    public Animator anim;
    public int health = 150;
    public GameObject[] deathEffects;
    public GameObject hitEffect;
    public bool doesShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCount;
    public SpriteRenderer theBody;
    public float shootingRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy){
            moveDirection = Vector3.zero;
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < attentionRange && chasePlayer) {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }

            if(runsAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < fleeRange) {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }
            /*else {
                moveDirection = Vector3.zero;
            }*/
            moveDirection.Normalize();

            enRB.velocity = moveDirection * moveSpeed;

            if (doesShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootingRange){
                fireCount -= Time.deltaTime;

                if (fireCount <= 0) {
                    fireCount = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(13);

                }
            }
        } else {
            enRB.velocity = Vector2.zero;
        }

        if (moveDirection != Vector3.zero) {
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }
    }


    public void DamageEnemy(int damage) {
        health -= damage;
        AudioManager.instance.PlaySFX(2);
        Instantiate(hitEffect, transform.position, transform.rotation);

        if(health <= 0) {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(1);

            int selectedEffect = Random.Range(0, deathEffects.Length);
            int rotation = Random.Range(0, 4);

            Instantiate(deathEffects[selectedEffect], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

            //Instantiate(deathEffects, transform.position, transform.rotation);
        }
    }
}
