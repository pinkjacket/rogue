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
    }
}
