using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GeneralAnimation : MonoBehaviour
{
    // Properties
    [SerializeField] private float magnitude, strenght;
    float frequency;
    float t, targetFlip;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private float _angle, _flip;
    // Update is called once per frame
    void Update()
    {
        frequency = Vector3.Distance(rb.velocity, Vector3.zero);
        t += Time.deltaTime * frequency * magnitude;

        Sway();
        Flip();
        Hop();
        

        _flip = Mathf.Lerp(_flip, targetFlip, Time.deltaTime * strenght);

        transform.rotation = Quaternion.Euler(0, _flip, _angle);
    }

    void Hop()
    {
        // Hop
        Transform sprite = transform.GetChild(0);

        if (frequency <= 0.5f)
        {
            sprite.transform.localPosition = Vector3.Lerp(sprite.transform.localPosition, Vector3.zero, Time.deltaTime * strenght);
        }
        else
        {
            sprite.transform.localPosition = new Vector3(0, Mathf.Abs(Mathf.Sin(t)) / 6, 0);
        }

        
    }

    void Flip()
    {
        // Flip
        if (rb.velocity.x > 0)
        {
            targetFlip = 0;
        }
        else
        {
            targetFlip = 180;
        }
    }

    void Sway()
    {
        // Sway
        

        if (frequency <= 0.5f)
        {
            _angle = Mathf.Lerp(_angle, 0, Time.deltaTime * strenght);
            t = Mathf.Lerp(t, 0, Time.deltaTime * strenght);
        }
        else
        {
            _angle = Mathf.Sin(t) * strenght;
        }
    }
}
