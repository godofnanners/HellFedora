using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflect : MonoBehaviour {

    BoxCollider2D collider;
    float angle;
    [SerializeField] float magnitude;
    SpriteRenderer sR;
    SwordRotation rC;
    Color originialColor;
    public bool cooldown;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        sR = GetComponent<SpriteRenderer>();
        originialColor = sR.color;
        rC = gameObject.GetComponentInParent<SwordRotation>();
    }

    public void DeflectboxActivation()
    {
        collider.enabled = true;
        sR.color = Color.green;
        
    }

    public void DeflectboxDeactivate()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("deflect");
            angle = rC.angle;
            var enemy=collision.gameObject.GetComponent<FiniteEnemy>();
            if (enemy!=null)
            {
                enemy.Deflected(DeflectionDirection(angle),magnitude);
            }
        }
    }

    Vector2 DeflectionDirection(float angle)
    {
        
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad*angle));
    }

}
