using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour {

    public CanvasGroup CG_Puzzle;
    public RectTransform Rect_Puzzle;
    public Image BackgroundImg;

    private PlayerManager _playerMan;
    private BodyPartManager _bodyMan; 
    private PuzzleManager _puzzleMan;

	// Use this for initialization
	void Start () {
        _playerMan = FindObjectOfType<PlayerManager>();
        _bodyMan = _playerMan.GetComponent<BodyPartManager>();
        _puzzleMan = FindObjectOfType<PuzzleManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.M))
            StartPuzzleAnim();

	}


    public void StartPuzzleAnim()
    {

        _playerMan.StopRunning = true;

        Sequence seq = DOTween.Sequence();

        seq.OnComplete(() => StartPuzzleGame());

        seq.Insert(0, BackgroundImg.DOFade(1, 0.2f));

        seq.Insert(0, Rect_Puzzle.DOAnchorPosY(-300, 0f));
        seq.Insert(0, Rect_Puzzle.DOScale(Vector2.zero,0f));
        seq.Insert(0, CG_Puzzle.DOFade(1, 0));

        seq.Insert(0.1f, Rect_Puzzle.DOAnchorPosY(0, 0.6f).SetEase(Ease.OutSine));
        seq.Insert(0.1f, Rect_Puzzle.DOScale(Vector2.one, 0.6f).SetEase(Ease.OutSine));
        seq.Insert(0, CG_Puzzle.DOFade(1, 0.6f));
        


    }

    public void EndPuzzleAnim()
    {
        //set player pos to next level

        _bodyMan.ResetAnim();

        Sequence seq = DOTween.Sequence();

        seq.OnComplete(() => _playerMan.StopRunning = false);

        seq.Insert(0.1f, Rect_Puzzle.DOAnchorPosY(-300, 0.6f).SetEase(Ease.OutSine));
        seq.Insert(0.1f, Rect_Puzzle.DOScale(Vector2.zero, 0.6f).SetEase(Ease.OutSine));
        seq.Insert(0, CG_Puzzle.DOFade(0, 0.6f));
        seq.Insert(0.6f, BackgroundImg.DOFade(0, 0.2f));
    }

    public void StartPuzzleGame()
    {
        StartCoroutine(_puzzleMan.ShufflePuzzle());
    }
}
