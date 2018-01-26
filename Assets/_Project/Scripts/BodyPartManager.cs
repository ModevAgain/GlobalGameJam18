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

    private readonly int _animHash_LeftLeg = Animator.StringToHash("LeftLeg");
    private readonly int _animHash_RightLeg = Animator.StringToHash("RightLeg");
    private readonly int _animHash_LeftHand = Animator.StringToHash("LeftHand");
    private readonly int _animHash_RightHand = Animator.StringToHash("RightHand");


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void UpdateActiveParts(Bodypart.PartType part, bool isActive = false)
    {
        switch (part)
        {
            case Bodypart.PartType.LeftLeg:
                LeftLegActive = isActive;
                Animator.SetBool(_animHash_LeftLeg, false);
                break;
            case Bodypart.PartType.RightLeg:
                RightLegActive = isActive;
                Animator.SetBool(_animHash_RightLeg, false);
                break;
            case Bodypart.PartType.LeftHand:
                LeftHandActive = isActive;
                Animator.SetBool(_animHash_LeftHand, false);
                break;
            case Bodypart.PartType.RightHand:
                RightHandActive = isActive;
                Animator.SetBool(_animHash_RightHand, false);
                break;
        }
    }

}
