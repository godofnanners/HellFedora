using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotation : MonoBehaviour {

    public float angle;

    void Update()
    {
        HurtboxPlacement(Input.mousePosition);
    }

    public void HurtboxPlacement(Vector3 mouseInput)
    {
        //rotation
        Vector2 direction = Camera.main.ScreenToWorldPoint(mouseInput) - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;


    }
}
