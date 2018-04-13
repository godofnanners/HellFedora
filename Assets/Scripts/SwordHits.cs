using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordHits : MonoBehaviour {

    [SerializeField]private SpriteRenderer swordsprite;
    [SerializeField]bool hitDelay;
    [SerializeField] float activeHitTime;
    Color originalColor;

	// Use this for initialization
	void Start () {
        originalColor = swordsprite.color;
        hitDelay = false;
	}
	
	// Update is called once per frame
	void FixedUpdate (){

        swordsprite.color = originalColor;

        if (Input.GetButton("Hit") && hitDelay==false)
        {
            swordsprite.color = Color.red;
            hitDelay = true;
        }

        StartCoroutine(ActivityTime(activeHitTime));
        
        
    }

    IEnumerator ActivityTime(float time)
    {
        if (hitDelay)
        {
            yield return new WaitForSeconds(time);
            hitDelay = false;
            
        }
            
    }
}
