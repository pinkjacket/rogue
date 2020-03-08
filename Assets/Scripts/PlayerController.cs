using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D playerRB;
    public Transform gunArm;
    private Camera mainCam;
    public Animator anim;
    public GameObject bulletToShoot;
    public Transform firePoint;
    public float shotDelay;
    private float shotDelayCounter;
    public SpriteRenderer bodySR;
    private float activeMoveSpeed;
    public float dashSpeed = 8f;
    public float dashLength = .5f;
    public float dashCoolOff = 1f;
    public float dashInvuln = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;
    [HideInInspector]
    public bool canMove = true;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused) {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

            playerRB.velocity = moveInput * activeMoveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = mainCam.WorldToScreenPoint(transform.localPosition);

            //flip player to face the way they're aiming
            if (mousePos.x < screenPoint.x) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }

            //formula to rotate gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetMouseButtonDown(0)) {
                Instantiate(bulletToShoot, firePoint.position, firePoint.rotation);
                shotDelayCounter = shotDelay;
                AudioManager.instance.PlaySFX(12);
            }

            if (Input.GetMouseButton(0)) {
                shotDelayCounter -= Time.deltaTime;
                if (shotDelayCounter <= 0) {
                    Instantiate(bulletToShoot, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(12);
                    shotDelayCounter = shotDelay;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (dashCoolCounter <= 0 && dashCounter <= 0) {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;

                    anim.SetTrigger("dash");
                    AudioManager.instance.PlaySFX(8);

                    PlayerHealthController.instance.TempInvulnerability(dashInvuln);
                }
            }

            if (dashCounter > 0) {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0) {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCoolOff;
                }
            }

            if (dashCoolCounter > 0) {
                dashCoolCounter -= Time.deltaTime;
            }

            if (moveInput != Vector2.zero) {
                anim.SetBool("isMoving", true);
            }
            else {
                anim.SetBool("isMoving", false);
            }
        } else {
            playerRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }
}
