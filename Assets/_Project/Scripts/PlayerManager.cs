using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour {

    [Range(0,10)]
    public float MovementSpeed;

    public MeshRenderer Ren;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.DOMoveX(transform.position.x + MovementSpeed * Time.deltaTime,0);
        Ren.material.mainTextureOffset = new Vector2(Ren.material.mainTextureOffset.x + Time.deltaTime/MovementSpeed,0);

	}
}
