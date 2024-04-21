using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : TimeControlled
{
    [Header("运动参数")]
    // public float speed;
    // public float length;
    // public float jumpForce;

    [Header("预制体")]
    public GameObject watch;
    public GameObject watchUI;

    public PlayerControlInput playerInput;
    private Vector2 inputDir;
    // private Rigidbody2D rb;
    private Animator anim;
    private PhysicsCheck physicsCheck;
    private WatchController watchController;
    private string fire;
    private bool isInTime = false;
    private bool isInOpen = false;

    private Vector3 startPos;
    private Vector3 currentPos;
    private int state = 0;
    private float defaultSpeed;
    private float defaultjumpForce;

    private void Awake()
    {
        defaultSpeed = speed;
        defaultjumpForce = jumpForce;
        playerInput = new PlayerControlInput();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        watchController = watch.GetComponent<WatchController>();
        anim = GetComponent<Animator>();
        ResetWatch();
        playerInput.Player.Jump.started += Jump;
    }

    private void OnEnable() {
        playerInput.Enable();
    }
    private void OnDisable() {
        playerInput.Disable();
    }
    void Update()
    {
        anim.SetFloat("velocityX",MathF.Abs(rb.velocity.x));
        anim.SetBool("isGround",physicsCheck.isGround);
        if(playerInput.Player.Fire.IsPressed()){
            fire = playerInput.Player.Fire.activeControl.name.ToString();
            if(fire == "1" && !isInTime){
                //暂停
                isInTime = true;
                state = 1;
                GoWatch();
            }else if(fire == "2" && !isInTime){
                //加速
                isInTime = true;
                state = 2;
                GoWatch();
                // Debug.Log("加速");
            }else if(fire == "3" && !isInTime){
                //回退
                isInTime = true;
                state = 3;
                GoWatch();
                // Debug.Log("回退");
            }
            if(fire == "q" && isInTime && !isInOpen){
                isInOpen = true;
                // Debug.Log("展开");
                watchController.OpenWatch(state);
                watch.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        currentPos = watch.transform.localPosition;
        // if(isInTime && Vector3.Distance(currentPos,startPos) > length){
        //     Debug.Log("over~~~");
        //     isInTime = false;
        //     ResetWatch();
        // }
    }
    private void FixedUpdate() {
        // Move(speed);
    }
    public void ResetWatch(){
        watch.SetActive(false);
        watch.GetComponent<WatchController>().Rest();
        watchUI.SetActive(true);

        //重置状态
        state = 0;
        isInOpen = false;
        isInTime = false;
        
        //重置速度
        // speed = defaultSpeed;
        // jumpForce = defaultjumpForce;
    }

    public override void TimeUpdate(){
        inputDir = playerInput.Player.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputDir.x*speed*Time.deltaTime,rb.velocity.y);
        int focus = (int)inputDir.x;

        //处理朝向
        if(focus > 0){
            transform.localScale = new Vector3(1,1,1);
        }
        else if(focus < 0){
            transform.localScale = new Vector3(-1,1,1);
        }
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGround){
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
        }
    }

    void GoWatch(){
        watch.transform.position = watchUI.transform.position;
        watch.SetActive(true);
        watchUI.SetActive(false);
        // 获取鼠标在屏幕上的位置
        Vector3 mousePosition = Mouse.current.position.ReadValue();
 
        // 转换为世界坐标
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
 
        // 计算鼠标的方向（相对于游戏对象的正前方）
        Vector3 direction = worldMousePosition - watch.transform.position;
 
        // 获取鼠标的角度
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        startPos = watch.transform.localPosition;
        watch.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x,direction.y)*80,ForceMode2D.Force);
    }
}
