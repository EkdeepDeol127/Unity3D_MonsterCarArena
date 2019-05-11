using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour {

    private float timer = 2f;
	
	void FixedUpdate ()
    {
		if(timer <= 0 && this.isActiveAndEnabled)
        {
            Despawn();
        }
        else
        {
            timer -= 1.0f * Time.deltaTime;
        }
	}

    /*void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "turret")
        {
            //add turret stuff
        }
    }*/

    void Despawn()
    {
        timer = 2f;
        this.gameObject.SetActive(false);
    }
}
