using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is improved version of NGUI's UISpriteAnimation.
/// </summary>

public class NGUISpriteAnimation : UISpriteAnimation
{
    [SerializeField]
    private int _framerate = 30;
    [SerializeField]
    private string _namePrefix = "";
    [SerializeField]
    private bool _loop = true;
    [SerializeField]
    private bool _pixelSnap = true;

    private ICommand command;
    private bool oneShot = true;

    void Awake()
    {
        mFPS = _framerate;
        mPrefix = _namePrefix;
        mLoop = _loop;
        mSnap = _pixelSnap;
    }

    protected override void Update()
    {
        if (mActive && mSpriteNames.Count > 1 && Application.isPlaying && mFPS > 0)
        {
            mDelta += RealTime.deltaTime;
            float rate = 1f / mFPS;

            if (rate < mDelta)
            {

                mDelta = (rate > 0f) ? mDelta - rate : 0f;

                if (++mIndex >= mSpriteNames.Count)
                {
                    mIndex = 0;
                    mActive = mLoop;

                    if (command != null)
                    {
                        command.Execute();
                        if (oneShot)
                            command = null;
                    }
                }

                if (mActive)
                {
                    mSprite.spriteName = mSpriteNames[mIndex];
                    if (mSnap) mSprite.MakePixelPerfect();
                }
            }
        }
    }

    public void Play(string animName)
    {
        namePrefix = animName;
        mLoop = true;
        oneShot = false;
        Play();
    }

    public void Play(string animName, ICommand command)
    {
        this.command = command;
        Play(animName);
    }

    public void PlayOneShot(string animName)
    {
        namePrefix = animName;
        mLoop = false;
        oneShot = true;
        Play();
    }

    public void PlayOneShot(string animName, ICommand command)
    {
        this.command = command;
        PlayOneShot(animName);
    }
}