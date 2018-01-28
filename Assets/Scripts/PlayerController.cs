using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Animator animator;
    public Slider HPBar;
    public Slider ENBar;
    public Text HPinfo;
    public Text ENinfo;

    public AudioClip GetMachinegunSE;
    public AudioClip GetShotgunSE;
    public AudioClip GetHealSE;
    private AudioSource audio;

    private bool shine;
    private float shining;

    private ThirdPersonController TPC;
    public float max_health;
    public float max_energy;
    public float cur_health;
    public float cur_energy;

    private bool active;

    ShootLeft BulltLeft;
    ShootRight BulltRight;

    private int count;


    // Use this for initialization
    void Start() {
        animator = gameObject.GetComponent<Animator>();
        BulltLeft = gameObject.GetComponentInChildren<ShootLeft>();
        BulltRight = gameObject.GetComponentInChildren<ShootRight>();
        TPC = gameObject.GetComponentInParent<ThirdPersonController>();
        audio = GetComponent<AudioSource>();
        animator.SetFloat("velocity", 0);

        shine = false;
        cur_health = max_health;
        count = 0;
    }

    // Update is called once per frame
    void Update() {
        // 把角色的速度值傳給 animator 中的 velocity
        animator.SetFloat("velocity", this.GetComponent<Rigidbody>().velocity.magnitude);
        HPBar.value = cur_health / max_health;
        ShowEN();
        SetText();
    }

    void ShowEN() {
        if (Time.time > shining)
        {
            shine = !shine;
            shining = Time.time + 0.2f;
        }
        if(!shine && TPC.cur_energy<TPC.CD_energy)
            ENBar.value = 0.0f;
        else
            ENBar.value = TPC.cur_energy / TPC.max_energy;
    }

    void OnTriggerEnter(Collider other)
    {
        //檢查碰到的物體是不是Pick Up
        if (other.gameObject.CompareTag("Pick Up"))
        {
            if (other.GetComponent<MeshRenderer>().enabled)
            {
                count++;
                BulltLeft.wMode = 1;
                BulltRight.wMode = 1;
                audio.PlayOneShot(GetMachinegunSE, 1.0F);
            }
            other.GetComponent<MeshRenderer>().enabled = false;
        }

        if (other.gameObject.CompareTag("Pick Shot"))
        {
            if (other.GetComponent<MeshRenderer>().enabled)
            {
                count++;
                BulltLeft.wMode = 2;
                BulltRight.wMode = 2;
                audio.PlayOneShot(GetShotgunSE, 1.0F);
            }
            other.GetComponent<MeshRenderer>().enabled = false;
        }

        if (other.gameObject.CompareTag("Pick Heal"))
        {
            if (other.GetComponent<MeshRenderer>().enabled)
            {
                count++;
                cur_health += 50;
                if (cur_health >= max_health) cur_health = max_health;
                audio.PlayOneShot(GetHealSE, 1.0F);
            }
            other.GetComponent<MeshRenderer>().enabled = false;
        }

        if (other.name == ("Enemy Bullet(Clone)") || other.name == ("Enemy Bullet2(Clone)")) {
            Destroy(other.gameObject);
            if (cur_health > 0) {
                cur_health -= 10;
                if (cur_health < 0) cur_health = 0;

            }

        }
    }

    void SetText() {
        HPinfo.text = cur_health.ToString() + "/" + max_health.ToString();
        ENinfo.text = ((int)TPC.cur_energy).ToString() + "/" + TPC.max_energy.ToString();
    }

    public void KillRobot() {
        TPC.KillRobots();
    }
}
