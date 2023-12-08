using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSelection : MonoBehaviour
{
    [SerializeField] float pickupRange;
    [SerializeField] Transform lootSelection;
    Transform selecting, selectedLooting;
    SpriteRenderer graphic;
    [SerializeField] Color common, uncommon, rare, epic, legendary;
    bool looting = false;
    Inventory inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        graphic = lootSelection.GetComponent<SpriteRenderer>();
        inventory = GetComponent<Inventory>();
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
                if(selectedLooting != collider.transform)
                {
                    loots.Add(collider.transform);

                }
                
            }
        }


        // Set selecting
        if (loots.Count > 0) 
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

                if(selecting == selectedLooting)
                {
                    selecting = null;
                    continue;
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
            switch(selecting.GetComponent<LootGraphic>().rarity)
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
        if(Input.GetKeyDown(KeyCode.H)) 
        {
            if(selecting != null && !looting && !inventory.isFull())
            {
                selectedLooting = selecting;
                looting = true;
            }
        }

        if(looting && !inventory.isFull())
        {
            selectedLooting.position = Vector3.Lerp(selectedLooting.position,transform.position, 10 * Time.deltaTime);
            selectedLooting.localScale = Vector3.Lerp(selectedLooting.localScale, Vector3.zero, 10 * Time.deltaTime);

            if(Vector3.Distance(selectedLooting.position,transform.position) < 1)
            {
                looting = false;


                // Add item to inventory
                LootGraphic lootGraphic = selectedLooting.GetComponent<LootGraphic>();
                Loot loot = lootGraphic.GetLoot();
                
                inventory.slots.Add(loot);
                inventory.Equip(loot);

                // Remove Icon
                Destroy(selectedLooting.gameObject);
                selectedLooting = null;
            }
        }
    }
}
