using UnityEngine;
using System.Collections;

public class ShootLeft : MonoBehaviour
{
    public GameObject mainCamera;

    public GameObject Bullet;
    public GameObject BulletPos;
    
    public int wMode;               //what kind of weapon we will use (initial pistal
    private float nextFire;
    public float Bullet_forward_force;
    public float fireRate;
    public float ShotgunFireRate;

    public AudioClip PistalSE;
    public AudioClip ShotgunSE;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        Bullet_forward_force = 300.0f;
        wMode = 0;
        fireRate = 0.1f;
        ShotgunFireRate = 0.5f;
        audio = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (Screen.lockCursor)
            switch (wMode)
        {
            case 0:
                Pistal();
                break;
            case 1:
                Machinegun();
                break;
            case 2:
                Shotgun();
                break;
            default:
                break;
        }
    }

    void Pistal()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audio.PlayOneShot(PistalSE, 1.0F);
            GameObject temp_bullet;
            Bullet.transform.Rotate(0, 90, 0);
            temp_bullet = Instantiate(Bullet, BulletPos.transform.position, Bullet.transform.rotation) as GameObject;
            Rigidbody temp_rigid;
            temp_rigid = temp_bullet.GetComponent<Rigidbody>();
            temp_rigid.AddForce(mainCamera.transform.forward * Bullet_forward_force * 100);
            Destroy(temp_bullet, 0.6f);
        }
    }

    void Machinegun()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            audio.PlayOneShot(PistalSE, 1.0F);
            nextFire = Time.time + fireRate;
            GameObject temp_bullet;
            Bullet.transform.Rotate(0, 90, 0);
            temp_bullet = Instantiate(Bullet, BulletPos.transform.position, Bullet.transform.rotation) as GameObject;
            Rigidbody temp_rigid;
            temp_rigid = temp_bullet.GetComponent<Rigidbody>();
            temp_rigid.AddForce(mainCamera.transform.forward * Bullet_forward_force * 100);
            Destroy(temp_bullet, 0.6f);
        }
    }

    void Shotgun()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            audio.PlayOneShot(ShotgunSE, 1.0F);
            nextFire = Time.time + ShotgunFireRate;
            GameObject[] temp_bullet = new GameObject[9];
            Rigidbody[] temp_rigid = new Rigidbody[9];
            Bullet.transform.Rotate(0, 90, 0);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp_bullet[i * 3 + j] = Instantiate(
                        Bullet,
                        BulletPos.transform.position,
                        Bullet.transform.rotation) as GameObject;
                    temp_rigid[i * 3 + j] = temp_bullet[i * 3 + j].GetComponent<Rigidbody>();
                    temp_rigid[i * 3 + j].AddForce((
                            Quaternion.AngleAxis(5 * j - 5, mainCamera.transform.up) *
                            Quaternion.AngleAxis(5 * i - 5, mainCamera.transform.right) *

                            mainCamera.transform.forward
                            ) * Bullet_forward_force * 50);
                    Destroy(temp_bullet[i * 3 + j], 1.0f);
                }
            }
        }
    }
}
