﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour {

    [Header("References")]
    public CanvasGroup CG_Puzzle;
    public RectTransform Rect_Puzzle;
    public Image BackgroundImg;

    [Header("Level Data")]
    public Transform Portal_1;
    public Transform Portal_2;
    public int CurrentLevel = 0;

    private PlayerManager _playerMan;
    private BodyPartManager _bodyMan; 
    private PuzzleManager _puzzleMan;
    private ShootManager _shootMan;
    private JumpManager _jumpMan;

    private bool _inAnim;

	// Use this for initialization
	void Start () {
        _playerMan = FindObjectOfType<PlayerManager>();
        _bodyMan = _playerMan.GetComponent<BodyPartManager>();
        _puzzleMan = FindObjectOfType<PuzzleManager>();
        _shootMan = FindObjectOfType<ShootManager>();
        CG_Puzzle.blocksRaycasts = false;
        _jumpMan = FindObjectOfType<JumpManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.M))
        {
            _bodyMan.MFunction();
        }
            //StartPuzzleAnim();_

	}


    public void StartPuzzleAnim()
    {
        if (_inAnim)
            return;

        Debug.Log("Start Puzzle Animation");

        _inAnim = true;

        _playerMan.StopRunning = true;
        _shootMan.CanShoot = false;
        _jumpMan.CanJump = false;

        Sequence seq = DOTween.Sequence();

        seq.OnComplete(() => StartPuzzleGame());

        seq.Insert(0, BackgroundImg.DOFade(1, 0.2f));

        seq.Insert(0, Rect_Puzzle.DOAnchorPosY(-300, 0f));
        seq.Insert(0, Rect_Puzzle.DOScale(Vector2.zero,0f));
        seq.Insert(0, CG_Puzzle.DOFade(1, 0));

        seq.Insert(0.1f, Rect_Puzzle.DOAnchorPosY(0, 0.6f).SetEase(Ease.OutSine));
        seq.Insert(0.1f, Rect_Puzzle.DOScale(Vector2.one, 0.6f).SetEase(Ease.OutSine));
        seq.Insert(0, CG_Puzzle.DOFade(1, 0.6f));

        CurrentLevel++;

    }

    public void EndPuzzleAnim()
    {
        //set player pos to next level

        Debug.Log("End Puzzle Animation");

        _bodyMan.ResetAnim();
        _shootMan.FillImg.DOFillAmount(0, 0);

        switch (CurrentLevel)
        {
            case 1:
                _playerMan.transform.parent.position = new Vector3(Portal_1.transform.position.x, 0, 0);

                break;
            case 2:
                _playerMan.transform.parent.position = new Vector3(Portal_2.transform.position.x, 0, 0);
                break;
        }



        Sequence seq = DOTween.Sequence();

        seq.OnComplete(() =>
        {


            _playerMan.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            _playerMan.GetComponent<SpriteRenderer>().DOFade(1, 0f);
            _playerMan.transform.DOScale(Vector3.one, 0.2f).OnComplete(() => _playerMan.StopRunning = false);


        });

        seq.Insert(0.1f, Rect_Puzzle.DOAnchorPosY(-300, 0.6f).SetEase(Ease.OutSine));
        seq.Insert(0.1f, Rect_Puzzle.DOScale(new Vector3(1,1,1), 0.6f).SetEase(Ease.OutSine).OnComplete(() => Debug.Log("Scale Up abgeschlossen")));
        seq.Insert(0, CG_Puzzle.DOFade(0, 0.6f));
        seq.Insert(0.6f, BackgroundImg.DOFade(0, 0.2f));
    }

    public void StartPuzzleGame()
    {
        StartCoroutine(_puzzleMan.ShufflePuzzle());
        CG_Puzzle.blocksRaycasts = true;
    }
}
