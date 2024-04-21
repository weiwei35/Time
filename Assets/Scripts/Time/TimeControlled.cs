using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    
    [HideInInspector]
    public Rigidbody2D rb;
    public bool isStepback = false;
    public bool isSpeedUp = false;
    public virtual void TimeUpdate(){
        
    }
}
