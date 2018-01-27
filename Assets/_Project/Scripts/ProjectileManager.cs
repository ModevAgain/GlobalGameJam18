﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ShootRange
{
    SHORT,
    MID,
    LONG
};

public class ProjectileManager : MonoBehaviour {

    public bool shoot = false;
    public Sprite ProjectileLow;
    private int _projectileRange;

    private Vector3 _startPosition;



	// Use this for initialization
	void Start () {
        
        _startPosition = transform.position;
	}

    void SetShootRange(ShootRange range)
    {
        if (range == ShootRange.SHORT)
        {
            GetComponent<SpriteRenderer>().sprite = ProjectileLow;
            _projectileRange = 5;
        }
        else if (range == ShootRange.MID)
            _projectileRange = 10;
        else if (range == ShootRange.LONG)
            _projectileRange = 15;
    }
	
	// Update is called once per frame
	void Update () {
        if (shoot)
        {
            transform.DOMoveX(transform.position.x + 0.2f, Time.deltaTime);
        }
        if (transform.position.x >= transform.parent.position.x + 10)
        {
            ProjectileOutOfRange();
        }
    }

    public void ShootProjectile()
    {
        shoot = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    private void ProjectileOutOfRange()
    {
        shoot = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        transform.position = new Vector3(transform.parent.position.x + _startPosition.x, _startPosition.y,0);       
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "destructible")
        {
            col.GetComponent<DestrucibleManager>().FadeAndKill();
            ProjectileOutOfRange();
        }
    }
}
