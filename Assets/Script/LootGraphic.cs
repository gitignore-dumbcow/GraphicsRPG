using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGraphic : MonoBehaviour
{
    const float magnitude = 0.15f;
    const float frequency = 2f;
    const float offset = 0.1f;
    float t;
    float startY;

    Transform graphic;

    public GameObject @object;

    public Rarity rarity;
    public EquipmentType type;

    Loot loot;

    // Start is called before the first frame update
    void Start()
    {
        t += Random.Range(-1f, 1f);
        graphic = transform.GetChild(0);
        loot = new Loot(rarity, type, @object);
    }

    // Update is called once per frame
    void Update()
    {
        // Root
        startY = transform.position.y;
        t += Time.deltaTime * frequency;

        // Float
        float y = offset + startY + 0.5f * (1 + Mathf.Sin(t)) * magnitude;
        graphic.position = new Vector3(transform.position.x, y, 0);
        
    }

    public Loot GetLoot()
    {
        return loot;
    }
}
