using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {

    public GameObject player;   //指定主角物件
    public GameObject mainCamera;//指定攝影機物件
    public GameObject pausemenu;
    public GameObject head;
    public GameObject explode;

    private PlayerController PC;
    private float health;
    //public GameObject cameraCollisionBox;   //指定攝影機碰撞物件

    public CollisionCounter cameraProbe; //Camera 碰撞探測器
    public CollisionCounter probe;  //主角前方碰撞探測器

    public float Maxdistance;   //攝影機與主角的距離
    public float BaseSpeed; //主角的移動速度
    
    public float max_energy;
    public float cur_energy;
    public float CD_energy;
    public float Regenerate_energy;
    public float Boost_cost;
    public float Fly_cost;

    private bool active;
    private bool exploded;
    private float ex_time;

    public bool boost;
    public bool isground;
    public bool flymode;
    public bool godmode;
    
    private float distToGround;

    public float initJumpSpeed;
    private float JumpSpeed;
    public float gravity;
    public bool Falling;

    private bool isHoriMove;
    private bool isVertiMove;
    private Vector3 horiVelocity;
    private Vector3 vertiVelocity;
    private Vector3 yVelocity;

    private bool mouselock;
    private int score;

    // Use this for initialization
    void Start() {
        Screen.lockCursor = true;
        active = true;
        exploded = false;
        ex_time = 0;
        PC = GetComponentInChildren<PlayerController>();
        health = PC.cur_health;
        godmode = false;

        BaseSpeed = 30.0f;
        boost = false;
        flymode = false;

        initJumpSpeed = 35.0f;
        JumpSpeed = 0.0f;
        gravity = 20.0f;
        distToGround = player.GetComponent<Collider>().bounds.extents.y;
        //max_energy = 1000;
        cur_energy = max_energy;

        Falling = false;

        isHoriMove = false;
        isVertiMove = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    bool IsGrounded(){
        if (Physics.Raycast(head.transform.position, -Vector3.up, distToGround + 8))
        {
            RaycastHit hit;
            Ray downRay = new Ray(head.transform.position , -Vector3.up);
            if (Physics.Raycast(downRay, out hit))
            {
                player.transform.position += new Vector3(
                        0,
                        distToGround + 7 - hit.distance,
                        0);
            }
            return true;
        }
        return false;
        }


    void Draw() {
        float Speed;
        if (boost){
            cur_energy -= Boost_cost * Time.deltaTime;
            Speed = BaseSpeed * 3;
        }
        else 
            Speed = BaseSpeed; 
        
        if (cur_energy < 0){
                boost = false;
                flymode = false;
                cur_energy = 0.0f;
            }

        if (!boost && !flymode) {
            if (cur_energy < max_energy) {
                cur_energy += Regenerate_energy * Time.deltaTime;
                if (cur_energy > max_energy) cur_energy = max_energy;
            }
        }

        Vector3 movement = Vector3.zero;

        if (this.isHoriMove && this.isVertiMove) {
            movement = (this.horiVelocity + this.vertiVelocity).normalized * Speed;
        }
        else if (this.isHoriMove) {
            movement = this.horiVelocity.normalized * Speed;
        }
        else if (this.isVertiMove) {
            movement = this.vertiVelocity.normalized * Speed;
        }

        if (this.isVertiMove || this.isHoriMove) {
            // Caculate the player's degree from velocity
            float rotate = Mathf.Atan2(player.GetComponent<Rigidbody>().velocity.x, player.GetComponent<Rigidbody>().velocity.z);
            //player.transform.rotation = Quaternion.Euler(0, rotate / Mathf.PI * 180, 0);
        }
        player.GetComponent<Rigidbody>().velocity = movement;

    }

    void jump(){
        if (!isground) {
            if (Input.GetKeyDown(KeyCode.Space) && cur_energy > CD_energy) {
                flymode = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                flymode = false;
            }

            if (flymode)
            {
                cur_energy -= Fly_cost * Time.deltaTime;
                JumpSpeed = initJumpSpeed/2;
            }
            else
            {
                JumpSpeed -= gravity * Time.deltaTime * 3.5f;
                if (JumpSpeed < -40.0f) JumpSpeed = -40.0f;
            }
        }

        if (JumpSpeed <= 0)
            Falling = true;
        else Falling = false;

        if (Falling == true)
        {
            if (isground)
                JumpSpeed = 0.0f;
        }
        player.transform.position += Vector3.up * JumpSpeed * Time.deltaTime * 3;
        if (player.transform.position.y < 0)
            player.transform.position = new Vector3(player.transform.position.x,0,player.transform.position.z);
    }

    // Update is called once per frame
    void Update () {

        if (active || godmode)
        {
            health = PC.cur_health;
            if (health == 0 && !godmode)
            {
                active = false;
                return;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Screen.lockCursor = false;
                Time.timeScale = 0;
                pausemenu.active = true;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                godmode = !godmode;
            }

            // GET INPUT AXIS
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            // GET DIRECTION
            Vector3 forward = mainCamera.transform.forward;
            //Calibrate Y AXIS 
            forward.y = 0;
            // GET DIRECTION UNIT VECTOR
            forward.Normalize();

            Vector3 right = mainCamera.transform.right;
            right.y = 0;
            right.Normalize();

            if (isground = IsGrounded())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    JumpSpeed = initJumpSpeed;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && cur_energy > CD_energy)
                boost = true;
            if (Input.GetKeyUp(KeyCode.LeftShift)) boost = false;

            if (vertical > 0)
            {
                this.vertiVelocity = forward;
                isVertiMove = true;
            }
            else if (vertical < 0)
            {
                this.vertiVelocity = -forward;
                isVertiMove = true;
            }
            else { isVertiMove = false; }

            if (horizontal > 0)
            {
                this.horiVelocity = right;
                isHoriMove = true;
            }
            else if (horizontal < 0)
            {
                this.horiVelocity = -right;
                isHoriMove = true;
            }
            else { isHoriMove = false; }

            // perform the result on charactor
            Draw();
            jump();
        }
        else if(!godmode)
        {
            if (exploded == false)
            {
                GameObject temp_explode;
                temp_explode = Instantiate(explode, player.transform.position, transform.rotation) as GameObject;
                temp_explode.transform.localScale = new Vector3(5, 5, 5);
                exploded = true;
                ex_time = Time.time;
                Destroy(player, 0.5F);
            }
            if (Time.time > ex_time + 3.0f) Application.LoadLevel(Application.loadedLevel);
        }
    }


    public void KillRobots() {
        score++;
    }
}
