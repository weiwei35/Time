using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WatchController : MonoBehaviour
{
    public GameObject effectArea;
    public Animation anim;
    public PlayerController player;
    public float openTime;

    public AreaController area;
    public TimeController timeController;
    public void OpenWatch(int state){
        // Debug.Log(state);
        effectArea.SetActive(true);
        anim.Play("show");
        area.state = state;
        StartCoroutine(EndArea());
        StartCoroutine(CloseArea());
    }
    private IEnumerator CloseArea(){
        yield return new WaitForSeconds(openTime-0.5f);
        anim.Play("close");
    }

    private IEnumerator EndArea(){
        yield return new WaitForSeconds(openTime);
        player.ResetWatch();
        effectArea.SetActive(false);
        //重置物体
        ItemController[] item = GameObject.FindObjectsOfType<ItemController>();
        foreach(var obj in item) {
            obj.Rest();
        }
        timeController.SetStepBack();
    }

    public void Rest() {
        area.gameObject.SetActive(false);
        // area.Rest();
    }
}
