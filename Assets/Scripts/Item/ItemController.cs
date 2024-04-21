using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class ItemController : TimeControlled
{
    public Vector2 directory;
    public GameObject startPos;
    public GameObject endPos;
    public bool isOneWay;

    // private Rigidbody2D rb;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        if(directory.x==0){
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }else if(directory.y==0){
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void Update() {
        if(directory.x==0){
            if(Mathf.Abs(transform.position.y-endPos.transform.position.y) <= 0.01 || Mathf.Abs(transform.position.y-startPos.transform.position.y) <= 0.01)
            {
                if(Mathf.Abs(transform.position.y-endPos.transform.position.y)<Mathf.Abs(transform.position.y-startPos.transform.position.y))
                {
                    transform.position = endPos.transform.position;
                    if(isOneWay){
                        transform.position = startPos.transform.position;
                        directory = new Vector2(0,1);
                    }else{
                        directory = new Vector2(0,-1);
                    }
                }
                else
                {
                    transform.position = startPos.transform.position;
                    if(isOneWay){
                        transform.position = startPos.transform.position;
                        directory = new Vector2(0,1);
                    }else{
                        directory = new Vector2(0,1);
                    }
                }
            }
            if(transform.position.y>endPos.transform.position.y || transform.position.y<startPos.transform.position.y)
            {
                if(MathF.Min(Mathf.Abs(transform.position.y-endPos.transform.position.y),Mathf.Abs(transform.position.y-startPos.transform.position.y)) > 0.5){
                    if(Mathf.Abs(transform.position.y-endPos.transform.position.y)<Mathf.Abs(transform.position.y-startPos.transform.position.y))
                        {
                            transform.position = endPos.transform.position;
                            if(isOneWay){
                                transform.position = startPos.transform.position;
                                directory = new Vector2(0,1);
                            }else{
                                directory = new Vector2(0,-1);
                            }
                        }
                    else
                        {
                            transform.position = startPos.transform.position;
                            if(isOneWay){
                                transform.position = startPos.transform.position;
                                directory = new Vector2(0,1);
                            }else{
                                directory = new Vector2(0,1);
                            }
                        }
                }
            }
        }
        else if(directory.y==0){
            if(Mathf.Abs(transform.position.x-endPos.transform.position.x) <= 0.01 || Mathf.Abs(transform.position.x-startPos.transform.position.x) <= 0.01){
                if(Mathf.Abs(transform.position.x-endPos.transform.position.x)<Mathf.Abs(transform.position.x-startPos.transform.position.x))
                    {
                        transform.position = endPos.transform.position;
                        if(isOneWay){
                            transform.position = startPos.transform.position;
                            directory = new Vector2(1,0);
                        }else{
                            directory = new Vector2(-1,0);
                        }
                    }
                else
                    {
                        transform.position = startPos.transform.position;
                        if(isOneWay){
                            transform.position = startPos.transform.position;
                            directory = new Vector2(1,0);
                        }else{
                            directory = new Vector2(1,0);
                        }
                    }
            }
            if(transform.position.x>endPos.transform.position.x || transform.position.x<startPos.transform.position.x)
            {
                if(MathF.Min(Mathf.Abs(transform.position.x-endPos.transform.position.x),Mathf.Abs(transform.position.x-startPos.transform.position.x)) > 0.5){
                    if(Mathf.Abs(transform.position.x-endPos.transform.position.x)<Mathf.Abs(transform.position.x-startPos.transform.position.x))
                    {
                        transform.position = endPos.transform.position;
                        if(isOneWay){
                            transform.position = startPos.transform.position;
                            directory = new Vector2(1,0);
                        }else{
                            directory = new Vector2(-1,0);
                        }
                    }
                    else
                    {
                        transform.position = startPos.transform.position;
                        if(isOneWay){
                            transform.position = startPos.transform.position;
                            directory = new Vector2(1,0);
                        }else{
                            directory = new Vector2(1,0);
                        }
                    }
                }
            }
        }
    }

    public override void TimeUpdate()
    {
        rb.velocity = new Vector2(directory.x*speed*Time.deltaTime,directory.y*speed*Time.deltaTime);
    }

    public void Rest() {
        // speed = defaultSpeed;
    }
}
