using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Shadow
{
    public Shadow(Transform caster, Transform shadow)
    {
        _caster = caster;
        _shadow = shadow;
        _graphic = caster.Find("Sprite");
        graphicRenderer = _graphic.GetComponent<SpriteRenderer>();
        shadowRenderer = _shadow.GetComponent<SpriteRenderer>();
        offsetY = -graphicRenderer.bounds.size.y / 2 + shadow.localScale.y / 2;

        Vector3 realScale = new Vector3(graphicRenderer.bounds.size.x, graphicRenderer.bounds.size.x / shadowScale.x * shadowScale.y, 1);

        shadow.localScale = realScale;
        shadowScale = realScale;
    }

    public Transform GetGraphic()
    {
        return _graphic;
    }

    public void ResetPosition()
    {
        _shadow.position = _caster.transform.position + new Vector3(0,offsetY,0);
    }

    public Vector3 GetShadowPosition()
    {
        return _shadow.position;
    }
    public void SetScale(Vector3 scale)
    {
        _shadow.localScale = scale;
    }

    public void SetAlpha(float a)
    {
        Color color = shadowRenderer.color;
        color.a = a;

        shadowRenderer.color = color;
    }

    Transform _caster;
    Transform _shadow;
    Transform _graphic;
    SpriteRenderer graphicRenderer, shadowRenderer;
    float offsetY;

    public Vector3 shadowScale = new Vector3(0.58f, 0.19f, 1);

}
public class ShadowCaster : MonoBehaviour
{
    [SerializeField] GameObject shadowPF;
    public List<Transform> casters;
    [SerializeField]List<Shadow> shadows;
    [SerializeField] float shadowScale = 0.35f, transparency = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform caster in casters)
        {
            Transform shadow = Instantiate(shadowPF, transform).transform;
            shadow.name = caster.name + " Shadow";
            Shadow newShadow = new Shadow(caster, shadow);
            
            shadows.Add(newShadow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Shadow shadow in shadows)
        {
            shadow.ResetPosition();

            float intensity = 1 / Vector3.Distance(shadow.GetGraphic().position, shadow.GetShadowPosition()) * shadowScale;

            shadow.SetScale(shadow.shadowScale * intensity);
            shadow.SetAlpha(transparency * intensity);
        }
    }
}
