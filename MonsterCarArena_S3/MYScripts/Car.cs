using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour {

    public GameObject Turret;
    //public GameObject FWheel1;
    //public GameObject FWheel2;
    //public GameObject BWheel1;
    //public GameObject BWheel2;
    private float velocity = 20;
    private float rotationSpeed = 50.0F;
    private float translationCar;
    private float rotationCar;
    private float rotationTurretX;
    private float rotationTurretY;
    private Rigidbody rb;
    public bool touchingGround = false;
    public GameUI AccessGameUI;
    public AudioClip damSound;
    public AudioClip resetSound;
    public AudioClip hornSound;
    public AudioClip accelSound;
    public AudioClip skidSound;
    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    private bool once = false;
    private bool colOnce = false;
    private float vol;
    private float soundtimerHorn;
    private float soundsectimer = 0;
    private bool onceHorn = false;

    void Start ()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        soundtimerHorn = hornSound.length;
        source = GetComponent<AudioSource>();
        source.Play();
    }
	
	void FixedUpdate ()
    {
        if(Input.anyKey)
        {
            if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) && touchingGround == true)
            {
                translationCar = Input.GetAxis("Vertical") * velocity * Time.deltaTime;
                ForwardOrBack();
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && touchingGround == true)
            {
                rotationCar = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
                RightOrLeft();
            }
            if (Input.GetKeyDown(KeyCode.Space) && once == false)
            {
                rb.velocity = Vector3.zero;
                this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                touchingGround = false;
                once = true;
            }
            if (Input.GetKeyDown(KeyCode.H) && onceHorn == false)
            {
                source.PlayOneShot(hornSound, volHighRange);
                onceHorn = true;
                soundtimerHorn = 0.0f;
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                source.Stop();
                SceneManager.LoadScene("WinScreen");
            }
        }

        if (hornSound.length >= soundtimerHorn)
        {
            soundtimerHorn += 1.0f * Time.deltaTime;
        }
        else
        {
            onceHorn = false;
        }
        TurretAim();
    }

    private void RightOrLeft()
    {
        /*if (skidSound.length <= soundtimer)
        {
            soundtimer = 0;
            source.PlayOneShot(skidSound, 0.1f);
        }
        else
        {
            soundtimer += 0.01f * Time.deltaTime;
        }*/
        this.transform.Rotate(0, rotationCar, 0);
    }
    private void ForwardOrBack()
    {
        if(accelSound.length <= soundsectimer)
        {
            soundsectimer = 0;
            source.PlayOneShot(accelSound, 0.1f);
        }
        else
        {
            soundsectimer += 0.1f * Time.deltaTime;
        }
        this.transform.Translate(0, 0, translationCar);
    }

    void OnTriggerEnter(Collider ground)
    {
        if(ground.gameObject.tag == "ground")
        {
            touchingGround = true;
            once = false;
        }
    }

    private void TurretAim()//fix to add limits on rotation
    {
        rotationTurretX = Mathf.Clamp(Input.GetAxis("MouseX") * rotationSpeed * Time.deltaTime, -10, 10);
        rotationTurretY = Mathf.Clamp(Input.GetAxis("MouseY") * rotationSpeed * Time.deltaTime, -10, 10);

        if(Turret.transform.rotation.x < 10 || Turret.transform.rotation.x > -10)
        {
            Turret.transform.Rotate(0, rotationTurretX, 0);
        }
        if (Turret.transform.rotation.y < 10 || Turret.transform.rotation.y > -10)
        {
            Turret.transform.Rotate(-rotationTurretY, 0, 0);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Spikes" && colOnce == false)
        {
            colOnce = true;
            vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(resetSound, vol);
            this.gameObject.transform.position = new Vector3(150, 24, 180);
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            AccessGameUI.UpdateHealth(50);
        }
        if (other.gameObject.tag == "Water" && colOnce == false)
        {
            colOnce = true;
            vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(resetSound, vol);
            this.gameObject.transform.position = new Vector3(150, 24, 180);
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            AccessGameUI.UpdateHealth(50);
        }
        if (other.gameObject.tag == "Objects" && colOnce == false)
        {
            colOnce = true;
            vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(damSound, vol);
            AccessGameUI.UpdateHealth(5);
        }
        if (other.gameObject.tag == "EnemyBullet" && colOnce == false)
        {
            vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(damSound, vol);
            AccessGameUI.UpdateHealth(5);
        }
    }

    void OnCollisionExit()
    {
        colOnce = false;
    }
}