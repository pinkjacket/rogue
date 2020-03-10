using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFloor : MonoBehaviour
{
    public bool openOnEnemyClear;
    public List<GameObject> enemies = new List<GameObject>();
    public Room thisRoom;

    // Start is called before the first frame update
    void Start()
    {
        if (openOnEnemyClear) {
            thisRoom.closeOnEnter = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && thisRoom.roomActive && openOnEnemyClear) {
            for (int i = 0; i < enemies.Count; i++) {
                if (enemies[i] == null) {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if (enemies.Count == 0) {
                thisRoom.OpenDoors();
            }
        }
    }
}
