using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BodyPartManager : MonoBehaviour {

    public GameObject ShieldHolder;
    private ProjectileManager _projectile;

    [Header("States")]
    public bool RightLegActive;
    public bool LeftLegActive;
    public bool RightHandActive;
    public bool LeftHandActive;

    [Header("References")]
    public Animator Animator;
    private JumpManager _jumpMan;
    private ShootManager _shootMan;
    private PlayerManager _playerMan;

    private readonly int _animHash_LeftLeg = Animator.StringToHash("LeftLeg");
    private readonly int _animHash_RightLeg = Animator.StringToHash("RightLeg");
    private readonly int _animHash_LeftHand = Animator.StringToHash("LeftHand");
    private readonly int _animHash_RightHand = Animator.StringToHash("RightHand");
    private int _healthCounter = 0;
    private List<GameObject> _shieldList = new List<GameObject>();

    public AudioSource audio;
    public AudioClip audioClip1;
    public AudioClip audioClip2;


    // Use this for initialization
    void Start () {
        _jumpMan = FindObjectOfType<JumpManager>();
        _shootMan = FindObjectOfType<ShootManager>();
        _playerMan = GetComponent<PlayerManager>();
        _projectile = FindObjectOfType<ProjectileManager>();

	}
	
	// Update is called once per frame
	void Update () {


        for (int i = 0; i < 3; i++)
        {
            _shieldList.Add(ShieldHolder.transform.GetChild(i).gameObject);
        }
            
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Animator.Play(Animator.StringToHash("Idle"));
        }

        if (_healthCounter == 3)
        {
            DOTween.KillAll();
            SceneManager.LoadScene("Main");
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    UpdateActiveParts(Bodypart.PartType.LeftHand)
        //}
	}

    public void ResetAnim()
    {
        Animator.Play(Animator.StringToHash("Idle"));
        _shootMan.CanShoot = true;
    }

    public void UpdateActiveParts(Bodypart.PartType part)
    {
        switch (part)
        {
            case Bodypart.PartType.LeftLeg:
                LeftLegActive = false;
                Animator.SetBool(_animHash_LeftLeg, false);
                if(!LeftLegActive && !RightLegActive)
                {
                    _jumpMan.FillSpeed_Current = _jumpMan.FillSpeed_0Legs;
                    _projectile.SetNoLegPosition();
                    
                }
                else
                {
                    _jumpMan.FillSpeed_Current = _jumpMan.FillSpeed_1Legs;
                }
                break;
            case Bodypart.PartType.RightLeg:
                RightLegActive = false;
                Animator.SetBool(_animHash_RightLeg, false);
                if (!LeftLegActive && !RightLegActive)
                {
                    _jumpMan.FillSpeed_Current = _jumpMan.FillSpeed_0Legs;
                    _projectile.SetNoLegPosition();
                }
                else
                {
                    _jumpMan.FillSpeed_Current = _jumpMan.FillSpeed_1Legs;
                }
                break;
            case Bodypart.PartType.LeftHand:
                LeftHandActive = false;
                Animator.SetBool(_animHash_LeftHand, false);
                if (!LeftHandActive && !RightHandActive)
                {
                    _shootMan.CurrentFillHeight = _shootMan.FillHeight_0Hands;
                    _shootMan.SetAvailableHands(0.3f);
                }
                else
                {
                    _shootMan.CurrentFillHeight = _shootMan.FillHeight_1Hands;
                    _shootMan.SetAvailableHands(0.65f);
                }
                break;
            case Bodypart.PartType.RightHand:
                RightHandActive = false;
                Animator.SetBool(_animHash_RightHand, false);
                if (!LeftHandActive && !RightHandActive)
                {
                    _shootMan.CurrentFillHeight = _shootMan.FillHeight_0Hands;
                    _shootMan.SetAvailableHands(0.3f);
                }
                else
                {
                    _shootMan.CurrentFillHeight = _shootMan.FillHeight_1Hands;
                    _shootMan.SetAvailableHands(0.65f);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        

        if(col.tag == "Teleporter")
        {
            Debug.Log("collided mit Teleporter");

            _playerMan.StopRunning = true;

            _playerMan.GetComponent<SpriteRenderer>().DOFade(0, 2f).SetDelay(0.5f);
            _playerMan.transform.DOScale(0, 2f).SetDelay(0.5f);

            float rot = 0;

            DOTween.To(x => rot = x, 0, 1080, 2.5f).OnUpdate(() =>
            {
                transform.Rotate(new Vector3(0, 0, -rot * Time.deltaTime));
            }).OnComplete(() => FindObjectOfType<LevelManager>().StartPuzzleAnim());

            audio.clip = audioClip2;
            audio.time = 0.1f;
            audio.Play();

            return;
        }

        if (col.gameObject.name == "Projectile")
        {
            return;
        }

        Sequence seq = DOTween.Sequence();
        seq.SetLoops(5);

        seq.Insert(0, GetComponent<SpriteRenderer>().DOFade(0, 0.05f).SetEase(Ease.OutBack));
        seq.Insert(0.05f, GetComponent<SpriteRenderer>().DOFade(1, 0.05f).SetEase(Ease.OutBack));

        seq.Play();

        _shieldList[_healthCounter].transform.DOShakePosition(0.5f,10,1000).OnComplete(() => 
        _shieldList[_healthCounter - 1].transform.GetChild(0).gameObject.SetActive(false));

        _healthCounter++;
        Debug.Log(_healthCounter);

        Debug.Log(col.gameObject.name);

        audio.clip = audioClip1;
        audio.Play();
        

    }



    public void MFunction()
    {
        _playerMan.StopRunning = true;

        _playerMan.GetComponent<SpriteRenderer>().DOFade(0, 2f).SetDelay(0.5f);
        _playerMan.transform.DOScale(0, 2f).SetDelay(0.5f);

        float rot = 0;

        DOTween.To(x => rot = x, 0, 1080, 2.5f).OnUpdate(() =>
        {
            transform.Rotate(new Vector3(0, 0, -rot * Time.deltaTime));
        }).OnComplete(() => FindObjectOfType<LevelManager>().StartPuzzleAnim());

        audio.clip = audioClip2;
        audio.time = 1.5f;
        audio.Play();
    }
}
