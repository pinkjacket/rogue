using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    private SpriteRenderer theRenderer;

    // Start is called before the first frame update
    void Start()
    {
        theRenderer = GetComponent<SpriteRenderer>();

        theRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
