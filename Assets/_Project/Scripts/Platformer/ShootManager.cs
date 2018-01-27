using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShootManager : MonoBehaviour {

    public JumpManager JumpMan;

    [Header("References")]
    public Image FillImg;
    public Image ShootHighlight;


    [Header("Data")]
    public float FillSpeed;

    public float CurrentFillHeight;

    public float FillHeight_2Hands;
    public float FillHeight_1Hands;
    public float FillHeight_0Hands;

    public bool CanShoot = true;

    private Transform _player;
    private PlayerManager _playerMan;
    private ProjectileManager _projectileMan;

    // Use this for initialization
    void Start()
    {
        _playerMan = FindObjectOfType<PlayerManager>();
        _player = _playerMan.transform;
        _projectileMan = _player.GetComponentInChildren<ProjectileManager>();

        CurrentFillHeight = FillHeight_2Hands;
    }

    // Update is called once per frame
    void Update()
    {

        if (!CanShoot)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_projectileMan.OnTheWay)
                return;

            CanShoot = false;
            JumpMan.CanJump = false;
            StartCoroutine(StartShootFill());
        }

    }


    public IEnumerator StartShootFill()
    {

        yield return 0;

        DOTween.To(x => FillImg.fillAmount = x, 0, CurrentFillHeight, FillSpeed)
            .SetEase(Ease.Linear)
            .SetId("ShootFill")
            .OnComplete(() =>
            {
                Shoot(CurrentFillHeight);
            });

        yield return 0;
    }

    public void Shoot(float shootRange)
    {
        ShootRange range =    shootRange > 0.6f ?   ShootRange.LONG :
                            shootRange > 0.3f ?     ShootRange.MID :    
                                                    ShootRange.SHORT;

        _projectileMan.SetShootRange(range);

        Debug.Log("Shoot with Range: " + range);

        ShootHighlight.DOFade(1, 0.6f).SetEase(Ease.InOutBounce).OnComplete(() =>
        {
            ShootHighlight.DOFade(0, 0.2f).SetEase(Ease.OutSine);
            DOTween.To(x => FillImg.fillAmount = x, CurrentFillHeight, 0, 0.2f).OnComplete(() =>
            {
                CanShoot = true;
                JumpMan.CanJump = true;
            });
        });
        _projectileMan.ShootProjectile();        

    }

    public void SetAvailableHands(float height)
    {
        FillImg.DOFillAmount(height,0.1f);
        CurrentFillHeight = height;
    }
}
