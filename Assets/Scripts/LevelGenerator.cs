using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor;
    public int distanceToExit;
    public Transform generatorPoint;
    public enum Direction { up, right, down, left};
    public Direction mapDirection;
    public float xOffset = 18f;
    public float yOffset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        mapDirection = (Direction)Random.Range(0, 4);
        MoveGenPoint();

        for(int i = 0; i < distanceToExit; i++) {
            Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            mapDirection = (Direction)Random.Range(0, 4);
            MoveGenPoint();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void MoveGenPoint() {
        switch (mapDirection) {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }
    }
}
