using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour {
    public GameObject[] BrokenPieces;
    public int maxPieces = 5;

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

                int piecesDropped = Random.Range(1, maxPieces);

                for(int i = 0; i < piecesDropped; i++) {
                    int randomPiece = Random.Range(0, BrokenPieces.Length);
                    Instantiate(BrokenPieces[randomPiece], transform.position, transform.rotation);
                }
                
            }

        }

    }
}
