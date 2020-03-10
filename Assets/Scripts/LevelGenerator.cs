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
    public LayerMask roomLayer;
    private GameObject endRoom;
    private List<GameObject> layoutRoomObjects = new List<GameObject>();
    public RoomPrefabs rooms;
    private List<GameObject> generatedOutlines = new List<GameObject>();
    public RoomFloor floorStart, floorEnd;
    public RoomFloor[] possibleFloors;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        mapDirection = (Direction)Random.Range(0, 4);
        MoveGenPoint();

        for(int i = 0; i < distanceToExit; i++) {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            layoutRoomObjects.Add(newRoom);

            if(i +1 == distanceToExit) {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                endRoom = newRoom;
            }
            mapDirection = (Direction)Random.Range(0, 4);
            MoveGenPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, roomLayer)) {
                MoveGenPoint();
            }


        }
        //create room outlines
        CreateRoomOutline(Vector3.zero);
        foreach(GameObject room in layoutRoomObjects) {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);

        foreach(GameObject outline in generatedOutlines) {
            bool generateFloor = true;

            if(outline.transform.position == Vector3.zero) {
                Instantiate(floorStart, outline.transform.position, transform.rotation).thisRoom = outline.GetComponent<Room>();
                generateFloor = false;
            }

            if (outline.transform.position == endRoom.transform.position) {
                Instantiate(floorEnd, outline.transform.position, transform.rotation).thisRoom = outline.GetComponent<Room>();
                generateFloor = false;
            }

            if (generateFloor) {
                int floorChoice = Random.Range(0, possibleFloors.Length);

                Instantiate(possibleFloors[floorChoice], outline.transform.position, transform.rotation).thisRoom = outline.GetComponent<Room>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
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

    public void CreateRoomOutline(Vector3 roomPosition) {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, roomLayer);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, roomLayer);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, roomLayer);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, roomLayer);

        int exitCount = 0;
        if (roomAbove) {
            exitCount++;
        }
        if (roomBelow) {
            exitCount++;
        }
        if (roomLeft) {
            exitCount++;
        }
        if (roomRight) {
            exitCount++;
        }

        switch (exitCount) {
            case 0:
                Debug.LogError("No adjoining room! There is only the void.");
                break;
            case 1:
                if (roomAbove) {
                    generatedOutlines.Add(Instantiate(rooms.exitUp, roomPosition, transform.rotation));
                }
                if (roomBelow) {
                    generatedOutlines.Add(Instantiate(rooms.exitDown, roomPosition, transform.rotation));
                }
                if (roomLeft) {
                    generatedOutlines.Add(Instantiate(rooms.exitLeft, roomPosition, transform.rotation));
                }
                if (roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitRight, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if (roomAbove && roomBelow) {
                    generatedOutlines.Add(Instantiate(rooms.exitUD, roomPosition, transform.rotation));
                }
                if (roomAbove && roomLeft) {
                    generatedOutlines.Add(Instantiate(rooms.exitUL, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitUR, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft) {
                    generatedOutlines.Add(Instantiate(rooms.exitDL, roomPosition, transform.rotation));
                }
                if (roomBelow && roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitDR, roomPosition, transform.rotation));
                }
                if (roomLeft && roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitLR, roomPosition, transform.rotation));
                }
                break;
            case 3:
                if (roomAbove && roomBelow && roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitUDR, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomLeft) {
                    generatedOutlines.Add(Instantiate(rooms.exitUDL, roomPosition, transform.rotation));
                }
                if (roomAbove && roomLeft && roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitULR, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft && roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitDLR, roomPosition, transform.rotation));
                }
                break;
            case 4:
                if(roomAbove && roomBelow && roomLeft && roomRight) {
                    generatedOutlines.Add(Instantiate(rooms.exitAll, roomPosition, transform.rotation));
                }
                break;
        }
    }
}

[System.Serializable]
public class RoomPrefabs {
    public GameObject exitUp, exitDown, exitLeft, exitRight,
        exitUD, exitUR, exitUL, exitDR, exitDL, exitLR,
        exitUDR, exitULR, exitUDL, exitDLR, exitAll;
}