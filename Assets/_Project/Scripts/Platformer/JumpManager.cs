using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class JumpManager : MonoBehaviour {

    public ShootManager ShootMan;

    [Header("References")]
    public Image FillImg;
    public Image JumpHighlight;


    [Header("Data")]
    [Range(1,10)]
    public float JumpHeight;

    public float FillSpeed_Current;

    public float FillSpeed_2Legs;
    public float FillSpeed_1Legs;
    public float FillSpeed_0Legs;

    public bool CanJump = true;

    private Transform _player;
    private PlayerManager _playerMan;

    AudioSource audio;

	// Use this for initialization
	void Start () {
        _playerMan = FindObjectOfType<PlayerManager>();
        _player = _playerMan.transform;
        FillSpeed_Current = FillSpeed_2Legs;

        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!CanJump)
            return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            CanJump = false;
            ShootMan.CanShoot = false;
            StartCoroutine(StartJumpFill());
        }
	}

    public IEnumerator StartJumpFill()
    {
        float finalFill = 0;

        yield return 0;

        DOTween.To(x => FillImg.fillAmount = x, 0, 1, FillSpeed_Current)
            .SetEase(Ease.Linear)
            .SetId("JumpFill")
            .OnUpdate(() =>
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
                {
                    finalFill = FillImg.fillAmount;
                    Jump(finalFill);
                    DOTween.Kill("JumpFill");
                }
            })
            .OnComplete(() =>
            {
                Jump(1);
            });

        yield return 0;
    }

    public void Jump(float strength)
    {
        Debug.Log("Jump with strength: " + strength);

        JumpHighlight.DOFade(1, 0.6f).SetEase(Ease.InOutBounce).OnComplete(() =>
         {
             JumpHighlight.DOFade(0, 0.2f).SetEase(Ease.OutSine);
             DOTween.To(x => FillImg.fillAmount = x, strength, 0, 0.2f);
         });

        _player.DOLocalJump(new Vector3(0, -1.54f, 0), JumpHeight * strength, 1, 1.6f).OnComplete(() =>
        {
            ShootMan.CanShoot = true;
            CanJump = true;
            _player.localPosition = new Vector3(0, -1.54f, 0);
        });

        audio.Play();
    }
}
