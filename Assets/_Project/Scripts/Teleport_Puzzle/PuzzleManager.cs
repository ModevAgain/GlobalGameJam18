using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PuzzleManager : MonoBehaviour {

    [Header("References")]
    [Space]
    public RectTransform[] Puzzle_Tiles;
    private Dictionary<RectTransform, int> TileIndexMap;
    public Image FillImg;

    [Header("Data")]
    [Space]
    public float PuzzleTime;

	// Use this for initialization
	void Start () {

        TileIndexMap = new Dictionary<RectTransform, int>();

        for (int i = 0; i < Puzzle_Tiles.Length-1; i++)
        {
            TileIndexMap.Add(Puzzle_Tiles[i], i);
        }


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.S))
        {
            ShufflePuzzle();
            StartCoroutine(StartPuzzle());
        }

	}

    public void ShufflePuzzle(int level = 1)
    {
        
        if(level == 1)
        {
            //Right Hand to top left
            Puzzle_Tiles[5].SetSiblingIndex(0);

            //head to top middle
            Puzzle_Tiles[1].SetSiblingIndex(1);

            //bottom middle to right top
            Puzzle_Tiles[10].SetSiblingIndex(2);

            //right leg to left hand
            Puzzle_Tiles[11].SetSiblingIndex(3);

            //torso top to middle
            Puzzle_Tiles[4].SetSiblingIndex(4);

            //left leg to right hand
            Puzzle_Tiles[9].SetSiblingIndex(5);

            //left middle stay
            Puzzle_Tiles[6].SetSiblingIndex(6);

            //torso top to middle
            Puzzle_Tiles[7].SetSiblingIndex(7);

            //left hand to right middle
            Puzzle_Tiles[3].SetSiblingIndex(8);

            //first tile to bottom left
            Puzzle_Tiles[0].SetSiblingIndex(9);

            //right middle to bottom middle
            Puzzle_Tiles[8].SetSiblingIndex(10);

            //top right to right leg
            Puzzle_Tiles[2].SetSiblingIndex(11);
        }
        
    }

    public IEnumerator StartPuzzle()
    {
        bool onTimer = false;

        DOTween.To(x => FillImg.fillAmount = x, 1, 0, PuzzleTime).OnComplete(() =>
        {
            onTimer = true;
        });

        //Start tweeningTimer

        while (!onTimer)
        {
            yield return 0;
        }

        //lookup Hand
        //Debug.Log(Puzzle_Tiles[TileIndexMap[Puzzle_Tiles[3]]].GetComponentInChildren<Bodypart>().Part.ToString());


        if (transform.GetChild(3).GetComponentInChildren<Bodypart>().Part != Bodypart.PartType.LeftHand)
        {
            ProcessDmg(3);
        }
        if (transform.GetChild(5).GetComponentInChildren<Bodypart>().Part != Bodypart.PartType.RightHand)
        {
            ProcessDmg(5);
        }
        if (transform.GetChild(9).GetComponentInChildren<Bodypart>().Part != Bodypart.PartType.LeftLeg)
        {
            ProcessDmg(9);
        }
        if (transform.GetChild(11).GetComponentInChildren<Bodypart>().Part != Bodypart.PartType.RightLeg)
        {
            ProcessDmg(11);
        }
    }

    public void ProcessDmg(int index)
    {
        switch (index)
        {
            case 3: // Loose left hand
                Debug.Log("Loose left hand");
                break;
            case 5: // Loose right hand
                Debug.Log("Loose right hand");
                break;
            case 9: // Loose left leg
                Debug.Log("Loose left leg");
                break;
            case 11: // Loose right leg
                Debug.Log("Loose right leg");
                break;
        }
    }
}
