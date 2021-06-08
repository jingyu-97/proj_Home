﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamesystem;
public partial class GameManager
{
    public GameObject fKeyUI;
    public GameObject camUI;
    [Header ("ItemUI")]
    public GameObject ItemUISet;
    public Sprite sheet;
    public Sprite flower;
    public Sprite acceptance;

    [Header("PuzzleUI")]
    public GameObject puzzleUI;
    public AutoFlip diaryFlip;
    public Book diaryUI;
    private int activatedPuzzle;

    [Header("Flag")]
    private bool uiMode;

    public void CheckFKeyUI(GameObject target)
    {
        if (target.tag.Equals("TMNQ"))
        {
            if (gameState == State.T_SHEET || gameState == State.T_FLOWER)
            {
                if (fKeyUI.activeSelf == false)
                    fKeyUI.SetActive(true);
            }
            else
            {
                if (fKeyUI.activeSelf == true)
                    fKeyUI.SetActive(false);
            }
        }
        else
        {
            if (fKeyUI.activeSelf == true)
                fKeyUI.SetActive(false);
        }
    }

    public void InitUIMode(int type)
    {
        if (uiMode == true)
            return;
        if (camUI.activeSelf)
            camUI.SetActive(false);
        uiMode = true;
        playerControler.moveControlFlag = false; //유저 움직임 멈춤
        playerControler.rotateControlFlag = false; //유저 움직임 멈춤

        Cursor.visible = true; // 마우스 보임 
        Cursor.lockState = CursorLockMode.None; // 마우스 커서 이동 가능
        puzzleUI.SetActive(true); // UI 보이기
        puzzleUI.transform.GetChild(type).gameObject.SetActive(true);
        activatedPuzzle = type;
    }

    public void ExitUIMode()
    {
        if (uiMode == false)
            return;
        if (activatedPuzzle == Puzzle.Diary && diaryFlip.isFlipping == true)
            return;
        if(gameState >= State.TH_CAM)
        {
            camUI.SetActive(true);
        }
        puzzleUI.SetActive(false); //UI 숨기기
        puzzleUI.transform.GetChild(activatedPuzzle).gameObject.SetActive(false);
        playerControler.moveControlFlag = true; //유저 움직임 멈춤
        playerControler.rotateControlFlag = true; //유저 움직임 멈춤
        uiMode = false;
        Debug.Log("Puzzle Close");
    }

    public void ActiveItemUI(string tag)
    {
        GameObject itemUI;
        for(int i = 0; i < ItemUISet.transform.childCount; i++)
        {
            itemUI = ItemUISet.transform.GetChild(i).gameObject;
            if (itemUI.activeSelf == false)
            {
                itemUI.GetComponent<ItemUI>().SetProperty(tag);
                itemUI.SetActive(true);
            }
        }
    }

    public void UnActiveItemUI(string tag)
    {
        GameObject itemUI;
        for(int i = 0; i < ItemUISet.transform.childCount; i++)
        {
            itemUI = ItemUISet.transform.GetChild(i).gameObject;
            if (itemUI.GetComponent<ItemUI>().tag.Equals(tag))
            {
                itemUI.SetActive(false);
            }
        }
    }
}
