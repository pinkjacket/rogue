using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float pieceSpeed = 3f;
    private Vector3 pieceDirection;
    public float pieceSlowDown = 5f;
    public float lifetime = 3f;
    public SpriteRenderer theRenderer;
    public float fadeOut = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        pieceDirection.x = Random.Range(-pieceSpeed, pieceSpeed);
        pieceDirection.y = Random.Range(-pieceSpeed, pieceSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += pieceDirection * Time.deltaTime;

        pieceDirection = Vector3.Lerp(pieceDirection, Vector3.zero, pieceSlowDown * Time.deltaTime);

        lifetime -= Time.deltaTime;
        if(lifetime < 0) {
            theRenderer.color = new Color(theRenderer.color.r, theRenderer.color.g, theRenderer.color.b, Mathf.MoveTowards(theRenderer.color.a, 0f, fadeOut * Time.deltaTime));
            if(theRenderer.color.a == 0f) {
                Destroy(gameObject);
            }
        }
    }
}
