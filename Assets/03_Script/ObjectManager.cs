﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Gamesystem;

public class ObjectManager : MonoBehaviour
{
    public Camera mainCam;
    Vector3 beforePos;
    public ViewMode viewMode;
    RaycastHit raycastHitObject;
    ObjectInfo targetInfo;
    PlayerControler playerControler;
    [Header("Level1 Object")]
    public GameObject diary;
    public GameObject leftDoor;
    public GameObject leftDoorWall;
    public GameObject rightDoor;
    public GameObject rightDoorWall;
    public GameObject pianoFlower;
    public GameObject mirror;
    public GameObject crackingMirror;
    public GameObject mnq;
    public MNQPositionCondition OMNQTrigger;

    [Header("Level2 Object")]
    public GameObject TPicture;
    public GameObject blackHand;
    public GameObject fallenLeaves;
    public GameObject insence;
    public GameObject acceptance;
    public GameObject[] TtargetPoint;
    public GameObject chair;
    public Transform photoFrameSet;
    public Transform photoSet;
    public Transform electronicSet;
    public GameObject television;

    public GameObject candle;

    [Header("Level3 Object")]
    public GameObject THPicture;
    public GameObject playerCam;
    public GameObject picture;
    public GameObject firstTrapWall;
    public GameObject THMNQ;
    public GameObject pill;
    public GameObject water;
    [Space (10f)]
    public GameObject secondTrapWall;
    public GameObject cosmetics;
    public GameObject mask;
    [Space (10f)]
    public GameObject thirdTrapWall;
    public GameObject photo;

    [Header ("Component")]
    public TraceMNQSpawner traceMNQSpawner;
    public LassoMNQSpawner lassoMNQSpawner;
    public EyeSpawner eyeSpawner;
    public THMNQSpawner THMNQSpawner;
    public Spawner pictureSpawner;
    void Start()
    {
        playerControler = GameManager.instance.playerControler;
    }
    public void InteractionObject(RaycastHit target)
    {
        raycastHitObject = target;
        targetInfo = target.transform.GetComponent<ObjectInfo>();
        if (targetInfo != null && targetInfo.viewModeFlag)
        {
            InteractionViewModeObject();
        }
        switch (raycastHitObject.collider.tag)
        {
            case "MNQ":
                MNQ_Interactive();//마네킹 상호작용
                break;
            case "Diary":
                Diary_Interactive();//일기장 함수
                break;
            case "PianoFlower":
                PianoFlower_Interactive();                      // 피아노 꽃 함수
                break;
            case "Door":
                LeftDoor_Interactive();                             // 문 열고 닫기 함수
                break;
            case "Soju":
                Soju_Interactive();                             // 소주 함수
                break;
            case "Sheet":
                Sheet_Interactive();
                break;
            case "Piano":
                Piano_Interactive();
                break;
            case "FallenLeaves":
                FallenLeaves_Interactive();                     // 낙엽 함수
                break;
            case "Flower":
                Flower_Interactive();
                break;
            case "Acceptance_Mobile":
                Acceptance_Mobile_Interactive();                // 모바일 합격장 함수            
                break;
            case "Candle":
                Candle_Interactive();
                break;
            case "RightDoor":
                RightDoor_Interactive();
                break;
            case "Lasso":
                Lasso_Interactive();
                break;
            case "Electronic":
                Electronic_Interactive();
                break;
            case "PlayerCam":
                PlayerCam_Interactive();
                break;
            case "Picture":
                Picture_Interactive();
                break;
            case "THMNQ":
                THMNQ_Interactive();
                break;
            case "Bed":
                Bed_Interactive();
                break;
            case "MakeupDesk":
                MakeupDesk_Interactive();
                break;
            case "Test":
                viewMode.InitViewMode(target.transform.gameObject, false);
                break;

        }
    }

    public void InteractMNQ()
    {
        Vector3 mouseDownPos;
        RaycastHit raycastHitObject;
        Ray ray;
        mouseDownPos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mouseDownPos);
        if (Physics.Raycast(ray, out raycastHitObject, playerControler.m_RayDistance))
        {
            if (!raycastHitObject.collider.tag.Equals("TMNQ"))
            {
                return;
            }

            switch (GameManager.instance.gameState)
            {
                case State.T_SHEET:
                    GameManager.instance.gameState++;
                    GameManager.instance.UnActiveItemUI("Sheet");
                    break;
                case State.T_FLOWER:
                    GameManager.instance.gameState++;
                    GameManager.instance.UnActiveItemUI("Flower");
                    blackHand.SetActive(false);
                    insence.SetActive(false);
                    acceptance.SetActive(true);
                    break;
                case State.T_ACCEPTANCE:
                    break;
            }
        }
        else
        {
            Debug.Log("ray fail");
        }
    }

    void InteractionViewModeObject()
    {
        if(targetInfo.gameStateCondition == GameManager.instance.gameState)
        {
            viewMode.InitViewMode(raycastHitObject.transform.gameObject, true);
        }
        else
        {
            viewMode.InitViewMode(raycastHitObject.transform.gameObject, false);
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    void RightDoor_Interactive()
    {
        if (GameManager.instance.gameState == State.T_CANDLE)
        {
            GameManager.instance.gameState++;
            rightDoor.transform.localEulerAngles = new Vector3(-90, 225, 45);
            traceMNQSpawner.enabled = true;
        }
        else if(GameManager.instance.gameState == State.T_TELEVISION)
        {
            GameManager.instance.gameState++;
            playerCam.SetActive(true);
            picture.SetActive(true);
            rightDoorWall.SetActive(false);
            rightDoor.transform.localEulerAngles = new Vector3(-90, 80, -45);
            TPicture.SetActive(false);
            THPicture.SetActive(true);
        }
    }

    void Candle_Interactive()
    {
        if (GameManager.instance.gameState == State.O_DOOR)
        {
            GameManager.instance.gameState++;
        }
        playerControler.EquipItem(raycastHitObject.transform.gameObject, GameManager.instance.candleEquipPostion);
    }

    void Diary_Interactive() //일기장 상호작용
    {
        if (GameManager.instance.gameState >= State.O_MNQ && GameManager.instance.gameState <= State.O_DOOR)
        {
            if (GameManager.instance.gameState == State.O_MNQ)
                GameManager.instance.gameState++;
            else if (GameManager.instance.gameState == State.O_FISH_SCARF)
                GameManager.instance.gameState++;
            else if (GameManager.instance.gameState == State.O_MEDICINE)
                GameManager.instance.gameState++;
            else if (GameManager.instance.gameState == State.O_SCHOOL)
                GameManager.instance.gameState++;
            GameManager.instance.InitUIMode(Puzzle.Diary);//UI모드(퍼즐모드)로 전환.
        }
    }

    void MNQ_Interactive()
    {
        if (CompareGamestate(State.START))
        {
            diary.transform.position = GameManager.instance.diaryDropPosition.position;
            diary.transform.Rotate(0, 0, 90);
            SoundManager.instance.diaryDropSound.Play();
            GameManager.instance.gameState++;
        }
        else if (CompareGamestate(State.O_DIARY_COMPLETE))
        {
            playerControler.EquipItem(raycastHitObject.transform.gameObject, GameManager.instance.mnqEquipPostion);
        }
    }

    void PianoFlower_Interactive() //피아노꽃 함수
    {
        if (GameManager.instance.gameState == State.O_MNQ_MOVE)
        {
            GameManager.instance.InitUIMode(Puzzle.Piano);
        }
    }

    void LeftDoor_Interactive()
    {
        if (GameManager.instance.gameState == State.O_PIANO_PUZZLE)
        {
            GameManager.instance.gameState++;
            leftDoorWall.SetActive(false);
            leftDoor.transform.localEulerAngles = new Vector3(-90, 0, -60);
        }
    }

    void Soju_Interactive()
    {
        if (GameManager.instance.gameState == State.T_DOOR)
        {
            GameManager.instance.gameState++;
            if(traceMNQSpawner.isSpawnMNQ == false)
                traceMNQSpawner.SpawnMNQ();
            blackHand.SetActive(true);
            blackHand.transform.position = blackHand.GetComponent<ObjectInfo>().spawnTransform[0].position;
            TtargetPoint[0].SetActive(true);
            GameManager.instance.SwapLightSetting(true);
            SoundManager.instance.pianoBGM.Stop();
        }
        //Debug.Log(randomTime);
    }

    void Sheet_Interactive()
    {
        if (GameManager.instance.gameState == State.T_SPOT_FIRST)
        {
            GameManager.instance.gameState++;
            GameManager.instance.DisableMainLight();
            GameManager.instance.ActiveItemUI("Sheet");
            raycastHitObject.transform.gameObject.SetActive(false);
        }
    }

    void Piano_Interactive()
    {
        if (GameManager.instance.gameState == State.T_MNQ_FIRST)
        {
            GameManager.instance.gameState++;
            fallenLeaves.SetActive(true);
            blackHand.SetActive(false);
            traceMNQSpawner.DisableMNQ();
            traceMNQSpawner.ChangeNextPosSet();
            TtargetPoint[0].SetActive(false);
        }
    }

    void FallenLeaves_Interactive()
    {
        if (GameManager.instance.gameState == State.T_PIANO)
        {
            GameManager.instance.gameState++;
            if(traceMNQSpawner.isSpawnMNQ == false)
                traceMNQSpawner.SpawnMNQ();
            blackHand.SetActive(true);
            blackHand.transform.position = blackHand.GetComponent<ObjectInfo>().spawnTransform[1].position;
            TtargetPoint[1].SetActive(true);
            raycastHitObject.transform.gameObject.SetActive(false);
            //GameManager.instance.SwapLightSetting(true);
        }
    }

    void Flower_Interactive()
    {
        if (GameManager.instance.gameState == State.T_SPOT_SECOND)
        {
            GameManager.instance.gameState++;
            GameManager.instance.DisableMainLight();
            GameManager.instance.ActiveItemUI("Flower");
            raycastHitObject.transform.gameObject.SetActive(false);
            //카메라 물이 일렁이는 듯한 화면 효과
        }
    }

    void Acceptance_Mobile_Interactive()
    {
        if (GameManager.instance.gameState == State.T_MNQ_SECOND)
        {
            GameManager.instance.gameState++;
            traceMNQSpawner.DisableMNQ();
            traceMNQSpawner.ChangeNextPosSet();
            if(traceMNQSpawner.isSpawnMNQ == false)
                traceMNQSpawner.SpawnMNQ();
            blackHand.SetActive(true);
            blackHand.transform.position = blackHand.GetComponent<ObjectInfo>().spawnTransform[2].position;
            raycastHitObject.transform.gameObject.SetActive(false);
            TtargetPoint[2].SetActive(true);
            TtargetPoint[1].SetActive(false);
            //GameManager.instance.SwapLightSetting(true);
        }
    }

    void Lasso_Interactive()
    {
        //마네킹 플래시
        if(GameManager.instance.gameState == State.T_SPOT_THIRD)
        {
            GameManager.instance.gameState++;
            playerControler.moveControlFlag = true;
            lassoMNQSpawner.DisableObject(0, GameManager.instance.TMNQSpawnNum + 1);
            eyeSpawner.DisableEye(0, eyeSpawner.spawnedObjectSet.childCount);
            chair.SetActive(false);
            candle.SetActive(false);
            traceMNQSpawner.DisableMNQ();
            playerControler.isEquip = false;
            raycastHitObject.collider.gameObject.SetActive(false);
            TtargetPoint[2].SetActive(false);
            SpawnObjectSet(photoFrameSet);
            SpawnObjectSet(photoSet);
            StartCoroutine("WaitAndSpawn", GameManager.instance.waitSpawnElectronicTime);
        }
    }

    void Electronic_Interactive()
    {
        if (GameManager.instance.gameState == State.T_LASSO)
        {
            raycastHitObject.transform.GetChild(0).gameObject.SetActive(false);
            raycastHitObject.transform.GetChild(1).gameObject.SetActive(true);
            if (Check_AllInteract_Electronic())
            {
                GameManager.instance.gameState++;
                television.transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine("WaitAndOpenDoor", GameManager.instance.waitOpenRightDoor);
            }
        }
    }

    void PlayerCam_Interactive()
    {
        if (CompareGamestate(State.T_END_DOOR))
        {
            GameManager.instance.gameState++;
            playerCam.SetActive(false);
            GameManager.instance.camUI.SetActive(true);
        }
    }

    void Picture_Interactive()
    {
        if (CompareGamestate(State.TH_CAM))
        {
            GameManager.instance.gameState++;
            leftDoor.transform.localEulerAngles = new Vector3(-90, 60, 0);
        }
    }

    void THMNQ_Interactive()
    {
        if(CompareGamestate(State.TH_FIRST_TRAP))
        {
            GameManager.instance.gameState++;
            pill.SetActive(true);
            water.SetActive(true);
        }
        else if (CompareGamestate(State.TH_SECOND_TRAP))
        {
            GameManager.instance.gameState++;
            mask.SetActive(true);
            cosmetics.SetActive(true);
        }
        else if (CompareGamestate(State.TH_THIRD_TRAP))
        {
            GameManager.instance.gameState++;
            raycastHitObject.transform.gameObject.SetActive(false);
            thirdTrapWall.SetActive(false);
            photo.SetActive(true);
            beforePos = mainCam.transform.position;
            StartCoroutine("WaitAndRollBackCam", GameManager.instance.zoomTime);
            mainCam.transform.position = GameManager.instance.zoomCamPos.position;
            mainCam.transform.eulerAngles = GameManager.instance.zoomCamPos.eulerAngles;
            playerControler.enabled = false;
            //카메라 사진으로 줌인
        }
        else if (CompareGamestate(State.TH_THIRD_MNQ))
        {
            raycastHitObject.transform.gameObject.SetActive(false);
            pictureSpawner.SpawnObject(1);
            pictureSpawner.SetObjectPosition(
                raycastHitObject.transform.position.x,
                pictureSpawner.spawnedPosSet.GetChild(0).transform.position.y,
                raycastHitObject.transform.position.z);
            GameManager.instance.interactMNQNum--;
            if(GameManager.instance.interactMNQNum == 0)
            {
                GameManager.instance.gameState++;
                GameManager.instance.camUI.SetActive(false);
                THMNQSpawner.DisableObject(0, THMNQSpawner.spawnedPosSet.childCount);
                THMNQSpawner.enabled = false;
            }
        }
    }
    
    IEnumerator WaitAndRollBackCam(float zoomTime)
    {
        yield return new WaitForSeconds(zoomTime);
        mainCam.transform.position = beforePos;
        mainCam.transform.localEulerAngles = Vector3.zero;
        playerControler.enabled = true;
        playerControler.transform.position = GameManager.instance.playerTargetPos.position;
        THMNQSpawner.enabled = true;
    }
    void Bed_Interactive()
    {
        if (CompareGamestate(State.TH_FIRST_MNQ))
        {
            GameManager.instance.InitUIMode(Puzzle.Drum);
        }
    }

    void MakeupDesk_Interactive()
    {
        if (CompareGamestate(State.TH_SECOND_MNQ))
        {
            Debug.Log("interact makeupDesk");
            GameManager.instance.InitUIMode(Puzzle.Mask);
        }
    }
    bool Check_AllInteract_Electronic()
    {
        for(int i = 0; i < electronicSet.childCount; i++)
        {
            if (electronicSet.GetChild(i).GetChild(0).gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnObjectSet(Transform objectSet)
    {
        for (int i = 0; i < objectSet.childCount; i++)
        {
            objectSet.GetChild(i).gameObject.SetActive(true);
        }
    }

    void DisableObjectSet(Transform objectSet)
    {
        for (int i = 0; i < objectSet.childCount; i++)
        {
            objectSet.GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator WaitAndOpenDoor(float time)
    {
        yield return new WaitForSeconds(time);
        OpenRightDoor();
    }

    IEnumerator WaitAndSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnObjectSet(electronicSet);
    }

    public void OpenLeftDoor()
    {
        leftDoor.transform.localEulerAngles = new Vector3(-90, 0, -15);
    }

    public void OpenRightDoor()
    {
        rightDoor.transform.localEulerAngles = new Vector3(-90, 22, -10);
    }

    public void DisableTObject()
    {
        DisableObjectSet(electronicSet);
        DisableObjectSet(photoSet);
        DisableObjectSet(photoFrameSet);
        traceMNQSpawner.DisableMNQ();
    }

    public void SpawnLassoMNQ(int num)
    {
        lassoMNQSpawner.SpawnObject(num);
    }

    public void StopLassoMNQ()
    {
        lassoMNQSpawner.StopMNQ();
    }

    public void SpawnEye()
    {
        eyeSpawner.SpawnObject(eyeSpawner.spawnedObjectSet.childCount);
    }

    bool CompareGamestate(int state)
    {
        if (GameManager.instance.gameState == state)
            return true;

        return false;
    }
}
