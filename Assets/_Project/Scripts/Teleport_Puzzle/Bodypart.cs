using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodypart : MonoBehaviour {

    public PartType Part;



    public enum PartType
    {
        None,
        LeftLeg,
        RightLeg,
        LeftHand,
        RightHand
    }
}
