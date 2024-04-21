using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    Collider2D[] colliders;
    public int state;
    bool isActivate = false;
    public TimeController timeController;
    public float keepTime;
    private void OnDisable() {
        isActivate = false;
        Array.Clear(colliders, 0, colliders.Length);
    }

    private void Update() {
        colliders = Physics2D.OverlapCircleAll(transform.position,1f);
        foreach (var item in colliders)
        {
            if(item.tag == "Player" && !isActivate){
                // Debug.Log(item.name);
                if(state == 1){
                    Faster(item.GetComponent<PlayerController>());
                }
                if(state == 2){
                    Pause(item.GetComponent<PlayerController>());
                }
                if(state == 3){
                    timeController.stepBack = true;
                    StepBack(item.GetComponent<PlayerController>());
                }
            }
            if(item.tag == "Touchable" && !isActivate){
                // Debug.Log(item.name);
                if(state == 1){
                    FasterItem(item.GetComponent<ItemController>());
                }
                if(state == 2){
                    Pause(item.GetComponent<ItemController>());
                }
                if(state == 3){
                    timeController.stepBack = true;

                    StepBack(item.GetComponent<ItemController>());
                }
            }
        }
    }

    public void Faster(PlayerController item){
        // item.velocity = new Vector2(item.velocity.x*200,item.velocity.y*200);
        isActivate = true;
        item.speed = item.speed*2;
        item.jumpForce = item.jumpForce*2;
        item.isSpeedUp = true;
    }
    public void FasterItem(ItemController item){
        // item.velocity = new Vector2(item.velocity.x*200,item.velocity.y*200);
        isActivate = true;
        item.speed = item.speed*2;
        item.isSpeedUp = true;
    }
    public void Pause(TimeControlled item){
        isActivate = true;
        item.speed = 0;
    }
    public void StepBack(TimeControlled item){
        item.isStepback = true;
    }

    public void Rest() {
        timeController.stepBack = false;
        var timeObj = GameObject.FindObjectsOfType<TimeControlled>();
        foreach (var item in timeObj)
        {
            item.isStepback = false;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("离开领域"+other.gameObject.name);
        timeController.KeepEffect(other.GetComponent<TimeControlled>(),state);
    }

}
