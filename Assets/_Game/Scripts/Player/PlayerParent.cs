using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParent : MonoBehaviour
{
    enum Direct
    {
        Forward,
        Back,
        Right,
        Left,
        None
    }
    public Animator animationPlayer;
    public LayerMask eatBrick;
    public float speedMove;

    public Transform bricksHolder;
    public Transform brickPrefab;
    public Transform skinPlayer;
    public List<Transform> listBirkcs = new List<Transform>();

    private Vector3 targetPos;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    private Vector3 startPosSkinPlayer = new Vector3(0, 0.5f, 0);
    private Vector3 distancePlayerAndLastBrick = new Vector3(0, 0.3f, 0);
    private Vector3 firstPosBrick = new Vector3(0, 0.45f, 0);

    private bool isMoving = false;

    private void Start()
    {
        GameManager.OnWin += WinGameCallBack;
        GameManager.OnRetryLevel += ChangeAnimIdle;
        GameManager.OnNextLevel += ChangeAnimIdle;

    }
    private void OnDestroy()
    {
        GameManager.OnRetryLevel -= ChangeAnimIdle;
        GameManager.OnNextLevel -= ChangeAnimIdle;
        GameManager.OnWin -= WinGameCallBack;

    }

    private void ChangeAnimIdle()
    {
        ChangeAnim(Constants.ANIM_IDLE);
    }


    

    void Update()
    {

        if (!isMoving)
        {
            Swipe();
        }
        Move();
        //Debug.DrawRay(transform.position, Vector3.forward, Color.red);
    }

    private void WinGameCallBack()
    {
        ChangeAnim(Constants.ANIM_WIN);
    }
    public void OnInit()
    {
        isMoving = false;
        ClearListBricks();
        targetPos = Vector3.zero;
        skinPlayer.localPosition = startPosSkinPlayer;
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            DetectSwipe();
        }
    }


    private void DetectSwipe()
    {
        Vector3 directSwipe = Vector3.zero;
        float swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);
        if (swipeDistance > Constants.MIN_SWIPE_DISTANCE)
        {
            Vector2 swipeDirection = touchEndPos - touchStartPos;
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    directSwipe = GetDetectSwipe(Direct.Right);
                }
                else
                {
                    directSwipe = GetDetectSwipe(Direct.Left);
                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {

                    directSwipe = GetDetectSwipe(Direct.Forward);
                }
                else
                {
                    directSwipe = GetDetectSwipe(Direct.Back);

                }
            }

            isMoving = true;
            targetPos = GetLastBrickPos(directSwipe);

        }
    }

    private Vector3 GetDetectSwipe(Direct direct)
    {
        Vector3 detectSwipe = Vector3.zero;
        switch (direct)
        {
            case Direct.Left:
                detectSwipe = Vector3.left;
                break;
            case Direct.Right:
                detectSwipe = Vector3.right;
                break;
            case Direct.Back:
                detectSwipe = Vector3.back;
                break;
            case Direct.Forward:
                detectSwipe = Vector3.forward;
                break;
            default:
                break;
        }
        return detectSwipe;
    }
    private Vector3 GetLastBrickPos(Vector3 directionRay)
    {
        Vector3 lastBrickPos = transform.position;

        while (Physics.Raycast(lastBrickPos, directionRay, 1f, eatBrick))
        {
            lastBrickPos += directionRay;
        }
        return lastBrickPos;

    }
    private void Move()
    {
        if (targetPos != Vector3.zero && isMoving)
        {
            print(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speedMove * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                targetPos = Vector3.zero;
                isMoving = false;
            }
        }
    }

    public void AddBrick()
    {
        int index = listBirkcs.Count;
        Transform brickClone = Instantiate(brickPrefab, bricksHolder);
        listBirkcs.Add(brickClone);
        brickClone.localPosition = firstPosBrick + index * Constants.DISTANCE_BRICK * Vector3.up;
        //skinPlayer.localPosition += Vector3.up * 0.45f;\
        skinPlayer.localPosition = listBirkcs[listBirkcs.Count - 1].localPosition + distancePlayerAndLastBrick;
        ChangeAnim(Constants.ANIM_JUMP);
    }
    public void RemoveBrick()
    {
        if (CheckListBircks())
        {
            Destroy(listBirkcs[listBirkcs.Count - 1].gameObject);
            listBirkcs.RemoveAt(listBirkcs.Count - 1);
        }
        int index = listBirkcs.Count;
        skinPlayer.localPosition -= Vector3.up * Constants.DISTANCE_BRICK;
        if (listBirkcs.Count == 0)
        {
            skinPlayer.localPosition = startPosSkinPlayer;
        }
    }
    public bool CheckListBircks()
    {
        if (listBirkcs.Count > 0)
        {
            return true;
        }
        return false;
    }
    public void ClearListBricks()
    {
        if (CheckListBircks())
        {
            for (int i = 0; i < listBirkcs.Count; i++)
            {
                Destroy(listBirkcs[i].gameObject);
            }
            listBirkcs.Clear();
        }

    }
    private void ChangeAnim(string animName)
    {
        animationPlayer.ResetTrigger(animName);
        animationPlayer.SetTrigger(animName);
    }






}
