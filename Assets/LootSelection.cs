using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSelection : MonoBehaviour
{
    [SerializeField] float pickupRange;
    [SerializeField] Transform lootSelection;
    Transform selecting;
    SpriteRenderer graphic;
    [SerializeField] Color common, uncommon, rare, epic, legendary;
    // Start is called before the first frame update
    void Start()
    {
        graphic = lootSelection.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Collect loots
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);
        List<Transform> loots = new List<Transform>();

        foreach (Collider2D collider in colliders)
        {
            if(collider.transform.CompareTag("Loot"))
            {
                loots.Add(collider.transform);
            }
        }

        // Set selection
        if(loots.Count > 0) 
        {
            foreach (Transform loot in loots)
            {
                if(selecting == null)
                {
                    selecting = loot;
                    continue;
                }

                if (Vector3.Distance(transform.position, loot.position) < Vector3.Distance(transform.position, selecting.position))
                {
                    selecting = loot;
                }
            }

            
        }
        else
        {
            
            selecting = null;
        }

        // Settings
        if(selecting != null)
        {
            // Transform
            lootSelection.gameObject.SetActive(true);
            lootSelection.position = Vector3.Lerp(lootSelection.position, selecting.GetChild(0).position, 10 * Time.deltaTime);

            Vector3 boundSize = selecting.GetChild(0).GetComponent<SpriteRenderer>().bounds.size;
            float scale = Mathf.Max(boundSize.x, boundSize.y);
            Vector3 targetScale = Vector3.one * scale;

            lootSelection.localScale = Vector3.Lerp(lootSelection.localScale, targetScale, 10 * Time.deltaTime);

            // Color
            Color targetColor = Color.white;
            switch(selecting.GetComponent<Loot>().rarity)
            {
                case Rarity.Common:
                    targetColor = common;
                    break;
                case Rarity.Uncommon:
                    targetColor = uncommon;
                    break;
                case Rarity.Rare:
                    targetColor = rare;
                    break;
                case Rarity.Epic:
                    targetColor = epic;
                    break;
                case Rarity.Legendary:
                    targetColor = legendary;
                    break;
            }

            graphic.color = Color.Lerp(graphic.color, targetColor, 10 * Time.deltaTime);
        }
        else
        {
           
            if(graphic.color.a > 0.1f)
            {
                // Color
                Color color = graphic.color;
                color.a = 0;
                graphic.color = Color.Lerp(graphic.color, color, 10 * Time.deltaTime);
            }
            else
            {
                // Color
                graphic.color = new Color(1, 1, 1, 0);

                // Transform
                lootSelection.gameObject.SetActive(false);
                lootSelection.position = transform.position;
                lootSelection.localScale = Vector3.zero;
            }


        }

        // Looting
    }
}
