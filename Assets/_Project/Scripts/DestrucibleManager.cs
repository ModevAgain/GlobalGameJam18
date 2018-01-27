using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestrucibleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void FadeAndKill()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.SetLoops(5);
        seq.OnComplete(() => 
        GetComponent<SpriteRenderer>().DOFade(0, 0.05f).OnComplete(() => 
        Destroy(gameObject)));

        seq.Insert(0, GetComponent<SpriteRenderer>().DOFade(0, 0.05f).SetEase(Ease.OutBack));
        seq.Insert(0.05f, GetComponent<SpriteRenderer>().DOFade(1, 0.05f).SetEase(Ease.OutBack));

        seq.Play();
        
    }
}
