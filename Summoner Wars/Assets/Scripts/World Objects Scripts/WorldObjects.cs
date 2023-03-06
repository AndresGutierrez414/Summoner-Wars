using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjects : MonoBehaviour
{

    public string objectName;
    public Texture2D buildImage;
    public int cost, sellValue, hitPoints, maxHitPoints;
    protected Player player;
    protected string[] actions = {};
    protected bool currentlySelected = false;
    private Outline outline;

    public void SetSelection(bool selected) {
        currentlySelected = selected;
    }

    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller) {
        //only handle input if currently selected
        if(currentlySelected && hitObject && hitObject.name != "Ground") {
            WorldObjects worldObject = hitObject.GetComponent< WorldObjects >();
            //clicked on another selectable object
            if(worldObject) ChangeSelection(worldObject, controller);
        }
    }

    private void ChangeSelection(WorldObjects worldObject, Player controller) {
        //this should be called by the following line, but there is an outside chance it will not
        SetSelection(false);
        if(controller.SelectedObject) controller.SelectedObject.SetSelection(false);
        controller.SelectedObject = worldObject;
        worldObject.SetSelection(true);
    }

    public string[] GetActions() {
        return actions;
    }
    
    public virtual void PerformAction(string actionToPerform) {
        //it is up to children with specific actions to determine what to do with each of those actions
    }

    protected virtual void Awake() {
        
        outline = GetComponent<Outline>();
        outline.enabled = currentlySelected;
    }
    
    protected virtual void Start () {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
    }
    
    protected virtual void Update () {
    
    }
    
    protected virtual void OnGUI() {

    }
}
