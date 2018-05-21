using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    BoxCollider2D collider;
    SpriteRenderer sR;
    public bool cooldown;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        sR = GetComponent<SpriteRenderer>();
    }

    public void HurtboxActivation()
    {
        collider.enabled = true;
        sR.color = Color.red;
    }

    public void HurtboxDeactivate()
    {
        collider.enabled = false;
        sR.color = Color.white;
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
