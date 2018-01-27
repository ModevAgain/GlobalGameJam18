using System.Collections;
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
    public bool NoLegs = false;

    public int Longrange;
    public int Midrange;
    public int Lowrange;

    public bool OnTheWay;

    public Vector3 _startPosition;



	// Use this for initialization
	void Start () {
        _projectileRange = Longrange;
        if (!NoLegs)
        {
            _startPosition = transform.position;
        }
	}

    public void SetShootRange(ShootRange range)
    {
        if (range == ShootRange.SHORT)
        {
            GetComponent<SpriteRenderer>().sprite = ProjectileLow;
            _projectileRange = Lowrange;
        }
        else if (range == ShootRange.MID)
            _projectileRange = Midrange;
        else if (range == ShootRange.LONG)
            _projectileRange = Longrange;
    }
	
    public void SetNoLegPosition()
    {
        NoLegs = true;
        _startPosition = new Vector3(1.25f, 0.017f, 0);
        transform.localPosition = new Vector3(1.25f, 0.017f, 0);
    }

    // Update is called once per frame
    void Update () {
        if (shoot)
        {
            if (!OnTheWay)
                OnTheWay = true;
            transform.DOMoveX(transform.position.x + 0.2f, Time.deltaTime);
        }
        if (transform.position.x >= transform.parent.position.x + _projectileRange)
        {
            ProjectileOutOfRange();
        }
    }

    public void ShootProjectile()
    {
        shoot = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ProjectileOutOfRange()
    {
        shoot = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        if (NoLegs)
        {
            transform.localPosition = _startPosition; //new Vector3(transform.parent.localPosition.x, _startPosition.y, 0);
        }
        else
        {
            transform.position = new Vector3(transform.parent.position.x + _startPosition.x, _startPosition.y, 0);
        }
        OnTheWay = false;
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "destructible")
        {
            col.GetComponent<DestrucibleManager>().FadeAndKill();
            ProjectileOutOfRange();
        }
    }
}
