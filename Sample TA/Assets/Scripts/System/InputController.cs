﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour {

    public FixedJoystick 
        fixedJoystick;

    public FixedTouchField 
        fixedTouchField;

    public Transform 
        mainCamera;

    public Rigidbody 
        rigidbody;

    public Animator 
        anim;

    public float
        smoothSpeed = 0.125f,
        cameraAngle,
        cameraAngleSpeed;

    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (ActionController.actionController.isAction == false) {
            var input = new Vector3(fixedJoystick.inputVector.x, 0f, fixedJoystick.inputVector.y);
            var velocity = Quaternion.AngleAxis(cameraAngle, Vector3.up) * input * 5f;

            rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);

            if (fixedJoystick.inputVector.x != 0 || fixedJoystick.inputVector.y != 0) {
                transform.rotation = Quaternion.AngleAxis(cameraAngle + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);
            }

            cameraAngleSpeed = SettingController.settingController.cameraSensitivity.cameraSensitivity.value / 100;

            cameraAngle += fixedTouchField.TouchDist.x * cameraAngleSpeed;

            mainCamera.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * new Vector3(0f, 3f, -4.2f);
            mainCamera.rotation = Quaternion.LookRotation(transform.position + Vector3.up - mainCamera.position, Vector3.up);

            if (Math.Abs(fixedJoystick.inputVector.x) > 0 || Math.Abs(fixedJoystick.inputVector.y) > 0) {
                if (Math.Abs(fixedJoystick.inputVector.x) > 0.6f || Math.Abs(fixedJoystick.inputVector.y) > 0.6f) {
                    anim.SetBool("param_idletorunning", true);
                    anim.SetBool("param_idletowalk", false);
                    anim.SetBool("param_toidle", false);

                    if (GameController.gameController.audio.audioEffect.clip == GameController.gameController.audio.audioPlayerWalk) {
                        GameController.gameController.audio.audioEffect.Stop();
                    }

                    if (GameController.gameController.audio.audioEffect.isPlaying == false) {
                        GameController.gameController.audio.audioEffect.clip = GameController.gameController.audio.audioPlayerRun;
                        GameController.gameController.audio.audioEffect.Play();
                    }
                } else {
                    anim.SetBool("param_idletowalk", true);
                    anim.SetBool("param_idletorunning", false);
                    anim.SetBool("param_toidle", false);

                    if (GameController.gameController.audio.audioEffect.clip == GameController.gameController.audio.audioPlayerRun) {
                        GameController.gameController.audio.audioEffect.Stop();
                    }

                    if (GameController.gameController.audio.audioEffect.isPlaying == false) {
                        GameController.gameController.audio.audioEffect.clip = GameController.gameController.audio.audioPlayerWalk;
                        GameController.gameController.audio.audioEffect.Play();
                    }
                }

                SaveController.saveController.isSaveState = true;
            } else {
                SetAllAnimationFlagsToFalse();
                anim.SetBool("param_toidle", true);
                GameController.gameController.audio.audioEffect.Stop();
            }
        }
    }

    void SetAllAnimationFlagsToFalse() {
        anim.SetBool("param_idletowalk", false);
        anim.SetBool("param_idletorunning", false);
        anim.SetBool("param_idletojump", false);
        anim.SetBool("param_idletowinpose", false);
        anim.SetBool("param_idletoko_big", false);
        anim.SetBool("param_idletodamage", false);
        anim.SetBool("param_idletohit01", false);
        anim.SetBool("param_idletohit02", false);
        anim.SetBool("param_idletohit03", false);
    }
}
