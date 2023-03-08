using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;
using System;
using Settings;

public class UserInput : MonoBehaviour
{
    // variables //
    private Player player;
    private BindingSettings bindings;
    private GameObject menu;


    private bool cameraRotate()
    {
        if(bindings.cameraRotate.getBinding() is KeyCode){
            if (Input.GetKeyDown((KeyCode)bindings.cameraRotate.getBinding()))
            {
                return true;
            }
        }
        
        if(bindings.cameraRotate.getBinding() is int){
            if (Input.GetMouseButton((int)bindings.cameraRotate.getBinding()))
            {
                return true;
            }
        }

        return false;
    }


    private bool cameraForward()
    {
        //Debug.Log(bindings.cameraForward.getBinding());


        if(bindings.cameraForward.getBinding() is KeyCode){
            if (Input.GetKey((KeyCode)bindings.cameraForward.getBinding()))
            {
                return true;
            }
        }

        if(bindings.cameraForward.getBinding() is int){
            if (Input.GetMouseButton((int)bindings.cameraForward.getBinding()))
            {
                return true;
            }
        }

        return false;
    }

    private bool cameraLeft()
    {
        if(bindings.cameraLeft.getBinding() is KeyCode){
            if (Input.GetKey((KeyCode)bindings.cameraLeft.getBinding()))
            {
                return true;
            }
        }

        if(bindings.cameraLeft.getBinding() is int){
            if (Input.GetMouseButton((int)bindings.cameraLeft.getBinding()))
            {
                return true;
            }
        }

        return false;
    }

    private bool cameraRight()
    {
        if(bindings.cameraRight.getBinding() is KeyCode){
            if (Input.GetKey((KeyCode)bindings.cameraRight.getBinding()))
            {
                return true;
            }
        }

        if(bindings.cameraRight.getBinding() is int){
            if (Input.GetMouseButton((int)bindings.cameraRight.getBinding()))
            {
                return true;
            }
        }

        return false;
    }

    private bool cameraBackward()
    {
        if(bindings.cameraBackward.getBinding() is KeyCode){
            if (Input.GetKey((KeyCode)bindings.cameraBackward.getBinding()))
            {
                return true;
            }
        }

        if(bindings.cameraBackward.getBinding() is int){
            if (Input.GetMouseButton((int)bindings.cameraBackward.getBinding()))
            {
                return true;
            }
        }

        return false;
    }

    private void showMenu(){

        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Menu Open");
            CanvasGroup menuC = menu.GetComponent<CanvasGroup>();

            if(menuC.alpha == 0){
                menuC.alpha = 1;
            }
            else{
                menuC.alpha = 0;
            }
        }
    }


    private void MoveCamera() {

        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0,0,0);

        //horizontal camera movement
        if(!cameraRotate() && xpos >= 0 && xpos < ResourceManager.ScrollWidth) {
            movement.x -= ResourceManager.ScrollSpeed;
        } else if(!cameraRotate() && xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth) {
            movement.x += ResourceManager.ScrollSpeed;
        }
        
        //vertical camera movement
        if(!cameraRotate() && ypos >= 0 && ypos < ResourceManager.ScrollWidth) {
            movement.z -= ResourceManager.ScrollSpeed;
        } else if(!cameraRotate() && ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth) {
            movement.z += ResourceManager.ScrollSpeed;
        }


        //horizontal camera movement
        if (cameraLeft()) {
            movement.x -= ResourceManager.ScrollSpeed;
        } else if (cameraRight()) {
            movement.x += ResourceManager.ScrollSpeed;
        }

        //vertical camera movement
        if (cameraBackward()) {
            movement.z -= ResourceManager.ScrollSpeed;
        } else if (cameraForward()) {
            movement.z += ResourceManager.ScrollSpeed;
        }

        //make sure movement is in the direction the camera is pointing
        //but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if(destination.y > ResourceManager.MaxCameraHeight) {
            destination.y = ResourceManager.MaxCameraHeight;
        } else if(destination.y < ResourceManager.MinCameraHeight) {
            destination.y = ResourceManager.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if(destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }

    }
 
    private void RotateCamera() {

        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;
        
        //detect rotation amount if ALT is being held and the Right mouse button is down
        if(Input.GetMouseButton(2)) {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }
        
        //if a change in position is detected perform the necessary update
        if(destination != origin) {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
 
    }

    private void MouseActivity() {
        if(Input.GetMouseButtonDown(0)) LeftMouseClick();
        else if(Input.GetMouseButtonDown(1)) RightMouseClick();
    }


    private void LeftMouseClick() {
        if(player.hud.MouseInBounds()) {
            GameObject hitObject = FindHitObject();
            Vector3 hitPoint = FindHitPoint();
            if(hitObject && hitPoint != ResourceManager.InvalidPosition) {
                if(player.SelectedObject) player.SelectedObject.MouseClick(hitObject, hitPoint, player);
                else if(hitObject.name!="Ground") {
                    WorldObjects worldObject = hitObject.transform.root.GetComponent<WorldObjects>();
                    Debug.Log(hitObject);
                    Debug.Log(worldObject);
                    if(worldObject) {
                        //we already know the player has no selected object
                        player.SelectedObject = worldObject;
                        worldObject.SetSelection(true);


                        Outline outline = hitObject.transform.root.GetComponent<Outline>();
                        outline.enabled = true;
                    }
                }
            }
        }
    }

    private void RightMouseClick() {
        if(player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject) {
            player.SelectedObject.SetSelection(false);

            Outline outline = player.SelectedObject.transform.root.GetComponent<Outline>();
            outline.enabled = false;

            player.SelectedObject = null;

            
        }
    }


    private GameObject FindHitObject() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) return hit.collider.gameObject;
        return null;
    }

    private Vector3 FindHitPoint() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) return hit.point;
        return ResourceManager.InvalidPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        bindings = new BindingSettings();
        menu = GameObject.Find("Menu");
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
    }

    void LateUpdate() {
        if(player.human) {
            MoveCamera();
            RotateCamera();
            MouseActivity();
            showMenu();
        }
    }

}
