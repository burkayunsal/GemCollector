﻿using System;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class TouchHandler : Singleton<TouchHandler>
{

    #region UI
    [Foldout("Joystick Images",false)]
    [SerializeField] Image img_outerCircle, img_innerCircle;
    private float outerSize;

    public bool useJoystick;
    #endregion


    public enum TouchTypes
    {
        NONE = -1,
        Core = 0,
        Joystick = 1
    }

    TouchTypes activeTouch;

    private delegate void OnDownAction();
    private OnDownAction OnDown = null;
    public delegate void OnUpAction();
    public OnUpAction OnUp = null;
    private delegate void OnDragAction();
    private OnDragAction OnDrag = null;

    public bool isDragging = false;
    private bool canPlay = false;

    private Vector3 fp, lp, dif;

    public bool IsActive() => GameManager.isRunning && canPlay;
    public void Enable(bool isActive) => canPlay = isActive;

    private void Start()
    {
        Initialize(useJoystick ? TouchTypes.Joystick : TouchTypes.Core, isStart: true);
    }

    private void Update()
    {
        if (IsActive())
            HandleTouch();
    }

    public void OnGameStarted()
    {
        Enable(true);
    }

    void HandleTouch()
    {
        if (!isDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnDown?.Invoke();
                isDragging = true;
            }
        }
        else
        {
            OnDrag?.Invoke();

            if (Input.GetMouseButtonUp(0))
            {
                OnUp?.Invoke();
                isDragging = false;
            }
        }
    }

    public void Initialize(TouchTypes tt = TouchTypes.Core, bool isStart = false)
    {
        isDragging = false;
        switch(tt)
        {
            case TouchTypes.NONE:
                OnDown = null;
                OnUp = null;
                OnDrag = null;
                Enable(false);
                break;
            case TouchTypes.Core:
                OnDown = CoreDown;
                OnUp = CoreUp;
                OnDrag = CoreDrag;
                break;
            case TouchTypes.Joystick:
                outerSize = (img_outerCircle.rectTransform.rect.width / 2f) - (img_innerCircle.rectTransform.rect.width / 2f);
                OnDown = JoystickDown;
                OnUp = JoystickUp;
                OnDrag = JoystickDrag;
                break;
            default:
                OnDown = CoreDown;
                OnUp = CoreUp;
                OnDrag = CoreDrag;
                break;
        }

        if(isStart)
            UIManager.I.Initialize();
    }

    #region CORE


    void CoreDown()
    {
    }
    void CoreUp()
    {
        
    }

    void CoreDrag()
    {
    }


    #endregion
    
    #region JOYSTICK
    void JoystickDown()
    {
        fp = Input.mousePosition;
        img_outerCircle.transform.position = fp;
        img_outerCircle.gameObject.SetActive(true);
    }
    
    void JoystickDrag()
    {
        lp = Input.mousePosition;
        dif = lp - fp;

        lp = fp;
        SetImagePositions();
        SetPlayerSpeed();
        SetPlayerRotation();
    }
    void JoystickUp()
    {
        img_outerCircle.gameObject.SetActive(false);
        
        PlayerController.I.dragSpeed = 0f;
    }

    #region PLayerMovement Methods

    void SetImagePositions()
    {
        if (dif.magnitude >= outerSize)
            img_innerCircle.transform.localPosition = dif.normalized * outerSize;
        else
            img_innerCircle.transform.localPosition = dif;

        SetHighlights();
    }

    void SetPlayerSpeed()
    {
        PlayerController.I.dragSpeed = Mathf.Clamp(dif.magnitude / outerSize, 0.1f, 1f);
    }

    void SetPlayerRotation()
    {
       // if (dif == Vector3.zero) return;
        
            Vector3 normPos = dif.normalized;
        float rot = (Mathf.Atan2(normPos.y, normPos.x) * Mathf.Rad2Deg) - 90f;
        
        PlayerController.I.Rotate(Quaternion.Euler(Vector3.down * rot));
    }

    void SetHighlights()
    {
        if (dif.x >= 0)
        {
            if (dif.y >= 0)
                UIManager.I.ShowJoystickHighlights(0);
            else
                UIManager.I.ShowJoystickHighlights(3);
        }
        else
        {
            if (dif.y >= 0)
                UIManager.I.ShowJoystickHighlights(1);
            else
                UIManager.I.ShowJoystickHighlights(2);
        }
    }
    
    #endregion

    #endregion

}
