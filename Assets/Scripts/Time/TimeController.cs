using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool stepBack = false;
    public float keepTime;
    TimeControlled[] timeObj;

    public struct RecordedData
    {
        public Vector2 pos;
        public Vector2 vol;
    }
    RecordedData[,] datas;
    int recordMax = 100000;
    int recordCount;//所有记录数据数量
    int recordIndex;//当前记录的索引

    void Start(){
        timeObj = GameObject.FindObjectsOfType<TimeControlled>();
        datas = new RecordedData[timeObj.Length,recordMax];
    }

    void FixedUpdate()
    {
        if(stepBack && recordIndex>0){//时间回溯
            recordIndex--;
            for(int index = 0; index < timeObj.Length; index++) {
                TimeControlled time = timeObj[index];
                if(time.isStepback){
                    RecordedData data = datas[index,recordIndex];
                    time.transform.position = data.pos;
                    time.rb.velocity = data.vol;
                }
            }
        }
        else{//正常记录
            for(int index = 0; index < timeObj.Length; index++) {
                TimeControlled time = timeObj[index];
                RecordedData data = new RecordedData();
                data.pos = time.transform.position;
                data.vol = time.rb.velocity;
                datas[index,recordCount] = data;
            }
            recordCount++;
            recordIndex = recordCount;

            foreach (var item in timeObj)
            {
                item.TimeUpdate();
            }
        }
    }
    public void KeepEffect(TimeControlled item, int state) {
        StartCoroutine(KeepEffectIE(item,state));
    }

    IEnumerator KeepEffectIE(TimeControlled item, int state){
        yield return new WaitForSeconds(keepTime); 
        if(state == 1 && item.isSpeedUp){
            Debug.Log("开始减速"+item.name);
            item.speed = item.speed/2;
            item.jumpForce = item.jumpForce/2;
        }
        else if(state == 3){
            Debug.Log("结束回溯"+item.name);
            item.isStepback = false;
        }
    }

        public void SetStepBack() {
        StartCoroutine(ResetStepBack());
    }

    IEnumerator ResetStepBack(){
        yield return new WaitForSeconds(keepTime); 
        stepBack = false;
    }
}
