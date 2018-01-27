using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JumpManager : MonoBehaviour {

    public Image FillImg;
    public float FillSpeed;

    public bool CanJump = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!CanJump)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            CanJump = false;
            StartCoroutine(StartJumpFill());
        }

	}

    public IEnumerator StartJumpFill()
    {
        float finalFill = 0;

        yield return 0;

        DOTween.To(x => FillImg.fillAmount = x, 0, 1, FillSpeed)
            .SetId("JumpFill")
            .OnUpdate(() =>
            {
                if (Input.GetKeyDown(KeyCode.W))
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
        CanJump = true;
    }
}
