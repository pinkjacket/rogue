using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D enRB;
    public float moveSpeed;
    [Header("Chase")]
    public bool chasePlayer;
    public float attentionRange;
    private Vector3 moveDirection;
    [Header("Flee")]
    public bool runsAway;
    public float fleeRange;
    [Header("Wanderer")]
    public bool wanderer;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;
    [Header("Patroller")]
    public bool patroller;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    
    [Header("Shoots")]
    public bool doesShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCount;
    public float shootingRange;
    [Header("Variables")]
    public SpriteRenderer theBody;
    public Animator anim;
    public int health = 150;
    public GameObject[] deathEffects;
    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (wanderer) {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy){
            moveDirection = Vector3.zero;
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < attentionRange && chasePlayer) {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else {
                if (wanderer) {
                    if(wanderCounter > 0) {
                        wanderCounter -= Time.deltaTime;
                        //move enemy
                        moveDirection = wanderDirection;

                        if(wanderCounter <= 0) {
                            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        }
                    }
                    if(pauseCounter > 0) {
                        pauseCounter -= Time.deltaTime;

                        if(pauseCounter <= 0) {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);
                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }
                    }
                }
                if (patroller) {
                    moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                    if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f) {
                        currentPatrolPoint++;
                        if(currentPatrolPoint >= patrolPoints.Length) {
                            currentPatrolPoint = 0;
                        }
                    }
                }
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
