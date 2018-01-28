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
    public float ProjectileSpeed;
    public bool OnTheWay;

    public Transform ShootOrigin;
    public Transform ShootOrigin_NoLegs;
    public Vector3 _startPosition;
    private Transform _playerTrans;



	// Use this for initialization
	void Start () {
        _projectileRange = Longrange;
        //if (!NoLegs)
        //{
        //    _startPosition = transform.position;
        //}

        

        _playerTrans = FindObjectOfType<PlayerManager>().transform;
	}

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            if (!OnTheWay)
                OnTheWay = true;
            transform.DOMoveX(transform.position.x + ProjectileSpeed, Time.deltaTime);
        }
        if (transform.position.x >= _playerTrans.position.x + _projectileRange)
        {
            ProjectileOutOfRange();
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
        
    }

    public void ShootProjectile()
    {
        ProjectileOutOfRange();

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
            transform.position = ShootOrigin_NoLegs.position;
        }
        else
        {
            transform.position = new Vector3(ShootOrigin.position.x, ShootOrigin.position.y, 0);
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
