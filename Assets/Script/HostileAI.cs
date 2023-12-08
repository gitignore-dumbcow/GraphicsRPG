using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Gloom,

}

public class HostileAI : MonoBehaviour
{
    public EnemyType type;
    public float attackRange;

    Rigidbody2D rb;
    Transform target;

    float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;


        switch (type)
        {
            case EnemyType.Gloom:
                movementSpeed = 1.5f;
                attackRange = 1;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch(type)
        {
            case EnemyType.Gloom:
                Vector3 direction = (target.position - transform.position).normalized;
                if(Vector2.Distance(transform.position,target.position) <= attackRange)
                {
                    rb.velocity = Vector2.zero;
                    
                }
                else
                {
                    rb.velocity = direction * movementSpeed;

                }
                break;
        }
        
    }
}
