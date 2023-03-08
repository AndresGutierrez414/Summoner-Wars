using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System;
using UnityEngine;
using Settings;

public class BindingSettings : UserSettings
{
    public struct KeyBinding<T>
    {
        private T binding;

        public void setBinding(T newBinding)
        {
            binding = newBinding;
        }

        public T getBinding()
        {
            return binding;
        }
    }

    public KeyBinding<int> cameraRotate = new KeyBinding<int>();
    public KeyBinding<KeyCode> cameraForward = new KeyBinding<KeyCode>();
    public KeyBinding<KeyCode> cameraBackward = new KeyBinding<KeyCode>();
    public KeyBinding<KeyCode> cameraRight = new KeyBinding<KeyCode>();
    public KeyBinding<KeyCode> cameraLeft = new KeyBinding<KeyCode>();

    public BindingSettings()
    {
        cameraRotate.setBinding(2);
        cameraForward.setBinding(KeyCode.W);
        cameraBackward.setBinding(KeyCode.S);
        cameraRight.setBinding(KeyCode.D);
        cameraLeft.setBinding(KeyCode.A);
    }
}
