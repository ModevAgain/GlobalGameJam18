using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollManager : MonoBehaviour {

    public Image BG_1;
    public Image BG_2;

    public float GroundHorizontalLength;

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {

        if (transform.position.x < -GroundHorizontalLength)
        {
            RepositionBackground();
        }

        //GetComponent<Renderer>().material.maint
    }


    private void RepositionBackground()
    {        
        Vector2 groundOffSet = new Vector2(GroundHorizontalLength * 2f, 0);

        transform.position = (Vector2)transform.position + groundOffSet;
    }

}
