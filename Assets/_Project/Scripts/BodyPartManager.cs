using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BodyPartManager : MonoBehaviour {

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
    private int _healthCounter = 3;

    // Use this for initialization
    void Start () {
        _jumpMan = FindObjectOfType<JumpManager>();
        _shootMan = FindObjectOfType<ShootManager>();
        _playerMan = GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Animator.Play(Animator.StringToHash("Idle"));
        }

        if (_healthCounter == 0)
        {
            SceneManager.LoadScene("Main");
        }
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
            _playerMan.StopRunning = true;

            _playerMan.GetComponent<SpriteRenderer>().DOFade(0, 2f).SetDelay(0.5f);

            //Sequence seqRot = DOTween.Sequence();

            //seqRot.SetLoops(4);

            //seqRot.Insert(0, transform.DOLocalRotate(new Vector3(0, 0, transform.localRotation.z + 180), 0.2f));
            //seqRot.Insert(0.19f, transform.DOLocalRotate(new Vector3(0, 0, transform.localRotation.z + 180), 0.2f));
            //seqRot.Insert(0.4f, transform.DOLocalRotate(new Vector3(0, 0, 0), 0f));

            //seqRot.Play();

            float rot = 0;

            DOTween.To(x => rot = x, 0, 1340, 2.5f).OnUpdate(() =>
            {
                transform.Rotate(new Vector3(0, 0, -rot * Time.deltaTime));
            });
            

            return;
        }

        Sequence seq = DOTween.Sequence();
        seq.SetLoops(5);

        seq.Insert(0, GetComponent<SpriteRenderer>().DOFade(0, 0.05f).SetEase(Ease.OutBack));
        seq.Insert(0.05f, GetComponent<SpriteRenderer>().DOFade(1, 0.05f).SetEase(Ease.OutBack));

        seq.Play();

        _healthCounter--;
        Debug.Log(_healthCounter);

    }
}
