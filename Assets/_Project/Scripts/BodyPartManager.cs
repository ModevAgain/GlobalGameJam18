using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour {

    public bool RightLegActive;
    public bool LeftLegActive;
    public bool RightHandActive;
    public bool LeftHandActive;


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
                break;
            case Bodypart.PartType.RightLeg:
                RightLegActive = isActive;
                break;
            case Bodypart.PartType.LeftHand:
                LeftHandActive = isActive;
                break;
            case Bodypart.PartType.RightHand:
                RightHandActive = isActive;
                break;
        }
    }

}
