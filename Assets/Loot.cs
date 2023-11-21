using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public class Loot : MonoBehaviour
{
    const float magnitude = 0.15f;
    const float frequency = 2f;
    const float offset = 0.1f;
    float t;
    float startY;

    Transform graphic;

    public Rarity rarity;

    // Start is called before the first frame update
    void Start()
    {
        t += Random.Range(-1f, 1f);
        graphic = transform.GetChild(0);
        startY = graphic.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        t += Time.deltaTime * frequency;
        // Item
        float y = offset + startY + 0.5f * (1 + Mathf.Sin(t)) * magnitude;
        graphic.position = new Vector3(transform.position.x, y, 0);
        
    }
}
