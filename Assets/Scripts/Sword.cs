using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    BoxCollider2D collider;
    SpriteRenderer sR;
    public bool cooldown;
    Color originialColor;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        sR = GetComponent<SpriteRenderer>();
        originialColor = sR.color;
    }

    public void HurtboxActivation()
    {
        collider.enabled = true;
        sR.color = Color.red;
    }

    public void HurtboxDeactivate()
    {
        collider.enabled = false;
        sR.color = originialColor;
    }

    public void RefreshCooldown()
    {
        cooldown = false;
    }

    public void StartCooldown()
    {
        cooldown = true;
    }

}
