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

        public object getBinding(){
            if(keyCode == KeyCode.None && mouseButton != 0){
                return mouseButton;
            }

            if(mouseButton == 0 && keyCode != KeyCode.None){
                return keyCode;
            }

            return null;
        }

        public void setBinding(object binding){

            if(binding is int){
                mouseButton = (int)binding;
            }

            if(binding is KeyCode){
                keyCode = (KeyCode)binding;
            }
        }

    }

    public KeyBinding cameraRotate = new KeyBinding {mouseButton = 2};
    public KeyBinding cameraForward = new KeyBinding {keyCode = KeyCode.W};
    public KeyBinding cameraBackward = new KeyBinding {keyCode = KeyCode.S};
    public KeyBinding cameraRight = new KeyBinding {keyCode = KeyCode.D};
    public KeyBinding cameraLeft = new KeyBinding {keyCode = KeyCode.A};
}
