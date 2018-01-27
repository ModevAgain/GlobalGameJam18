using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private readonly int _animHash_LeftLeg = Animator.StringToHash("LeftLeg");
    private readonly int _animHash_RightLeg = Animator.StringToHash("RightLeg");
    private readonly int _animHash_LeftHand = Animator.StringToHash("LeftHand");
    private readonly int _animHash_RightHand = Animator.StringToHash("RightHand");


    // Use this for initialization
    void Start () {
        _jumpMan = FindObjectOfType<JumpManager>();
        _shootMan = FindObjectOfType<ShootManager>();       
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Animator.Play(Animator.StringToHash("Idle"));
        }
		
	}

    public void ResetAnim()
    {
        Animator.Play(Animator.StringToHash("Idle"));
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

}
