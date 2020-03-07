using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour {
    public GameObject[] BrokenPieces;
    public int maxPieces = 5;
    public bool itemInside;
    public GameObject[] possibleContents;
    public float dropPercent;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (PlayerController.instance.dashCounter > 0) {
                Destroy(gameObject);
                AudioManager.instance.PlaySFX(0);

                //show broken crate bits
                int piecesDropped = Random.Range(1, maxPieces);

                for(int i = 0; i < piecesDropped; i++) {
                    int randomPiece = Random.Range(0, BrokenPieces.Length);
                    Instantiate(BrokenPieces[randomPiece], transform.position, transform.rotation);
                }

                //drop items
                if (itemInside) {
                    float dropChance = Random.Range(0f, 100f);

                    if(dropChance < dropPercent) {
                        int randomItem = Random.Range(0, possibleContents.Length);
                        Instantiate(possibleContents[randomItem], transform.position, transform.rotation);
                    }
                }
                
            }

        }

    }
}
