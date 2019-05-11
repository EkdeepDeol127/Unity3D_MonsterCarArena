using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour {
    public ParticleSystem explosion;
    public GameObject bulletSpawn1;
    public GameObject Bullet;
    public float inputTime;
    private int arrayIndex = 10;
    private float speed = 250.0F;
    private List<GameObject> bulletPool;
    private float timer = 0.0f;
    public AudioClip shootSound;
    public AudioClip hurtSound;
    public AudioClip dieSound;
    private AudioSource source;
    private float volLowRange = 0.05f;
    private float volHighRange = 0.2f;
    public GameUI AccessGameUI;
    public Transform target;
    private float turnspeed = 10;
    private float step;
    private bool colOnce = false;
    private int turrtHealth = 100;
    private float vol;

    void Start ()
    {
        timer += inputTime;
        source = GetComponent<AudioSource>();
        bulletPool = new List<GameObject>();
        for (int k = 0; k < arrayIndex; k++)
        {
            GameObject obj = (GameObject)Instantiate(Bullet);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }
	
	void FixedUpdate ()
    {
        if (timer <= 0 && AccessGameUI.Quit == false)
        {
            timer = 1f + inputTime;
            spawnBullet();
        }
        else
        {
            timer -= 1.0f * Time.deltaTime;
        }
        lookAt();
    }

    void lookAt()
    {
        Vector3 targetDir = target.position - transform.position;
        step = turnspeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void spawnBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(shootSound, vol);
                bulletPool[i].transform.position = bulletSpawn1.transform.position;
                bulletPool[i].GetComponent<Rigidbody>().velocity = (transform.forward * speed);
                bulletPool[i].SetActive(true);
                break;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet" && colOnce == false)
        {
            colOnce = true;
            vol = Random.Range(volLowRange + 0.2f, volHighRange + 0.3f);
            source.PlayOneShot(hurtSound, vol);
            takeDamage(10);
        }
    }
    void OnCollisionExit()
    {
        colOnce = false;
    }

    void takeDamage(int dam)
    {
        if (turrtHealth <= 0)
        {
            explosion.Play();
            source.PlayOneShot(dieSound , volHighRange + 0.5f);
            AccessGameUI.UpdateTurret();
            //new WaitForSeconds(10);
            this.gameObject.SetActive(false);
        }
        else
        {
            turrtHealth -= dam;
        }
    }
}
