using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour {

    public GameObject bulletSpawn1;
    public GameObject bulletSpawn2;
    public GameObject Bullet;
    private int arrayIndex = 50;
    private float speed = 100.0F;
    private List<GameObject> bulletPool;
    private float timer = 0.5f;
    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = 0.3f;
    private float volHighRange = 0.6f;
    public GameUI AccessGameUI;

    void Start ()
    {
        source = GetComponent<AudioSource>();
        bulletPool = new List<GameObject>();
        for (int k = 0; k < arrayIndex; k++)
        {
            GameObject obj = (GameObject)Instantiate(Bullet);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }
	
	void Update ()
    {
		if(Input.GetMouseButtonDown(0) && timer <= 0 && AccessGameUI.Quit == false)
        {
            spawnBullet();
        }
        else
        {
            timer -= 1.0f * Time.deltaTime;
        }
    }

    void spawnBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(shootSound, vol);
                bulletPool[i].transform.position = bulletSpawn1.transform.position;
                bulletPool[i].GetComponent<Rigidbody>().velocity = (transform.forward * speed);
                bulletPool[i].SetActive(true);
                i++;
                bulletPool[i].transform.position = bulletSpawn2.transform.position;
                bulletPool[i].GetComponent<Rigidbody>().velocity = (transform.forward * speed);
                bulletPool[i].SetActive(true);
                break;
            }
        }
    }
}
