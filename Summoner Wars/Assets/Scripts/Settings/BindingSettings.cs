using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System;
using UnityEngine;
using Settings;

public class BindingSettings : UserSettings
{
    public struct KeyBinding
    {
        public KeyCode keyCode;
        public int mouseButton;

        public bool IsKeyCode
        {
            get { return keyCode != KeyCode.None; }
        }

        public bool IsMouseButton
        {
            get { return mouseButton != 0; }
        }
    }

    public KeyBinding cameraRotate = 2;
    public KeyBinding cameraForward = KeyCode.W;
    public KeyBinding cameraBackward = KeyCode.S;
    public KeyBinding cameraRight = KeyCode.D;
    public KeyBinding cameraLeft = KeyCode.A;

    public void setCameraRotateKey(object newBinding)
    {
        cameraRotate = newBinding;
    }

    public dynamic getCameraRotateKey()
    {
        return cameraRotate;
    }

    public void setCameraForwardKey(object newBinding)
    {
        cameraForward = newBinding;
    }

    public dynamic getCameraForwardKey()
    {
        return cameraForward;
    }

    public void setCameraBackwardKey(object newBinding)
    {
        cameraBackward = newBinding;
    }

    public dynamic getCameraBackwardKey()
    {
        return cameraBackward;
    }

    public void setCameraRightKey(object newBinding)
    {
        cameraRight = newBinding;
    }

    public dynamic getCameraRightKey()
    {
        return cameraRight;
    }

    public void setCameraLeftKey(object newBinding)
    {
        cameraLeft = newBinding;
    }

    public dynamic getCameraLeftKey()
    {
        return cameraLeft;
    }
}
