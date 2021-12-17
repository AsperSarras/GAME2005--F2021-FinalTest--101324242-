using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    //public List<GameObject> sPrefabs;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;
    
    public bool pause = true;
    private float cd = 1.0f;
    private float timer = 0.0f;


    public FizziksObj body;

    public Camera playerCam;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        _Fire();
        _Move();
        _Options();
    }

    private void _Move()
    {
            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity = playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity = -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity = playerCam.transform.forward * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") < 0.0f) 
            {
                // move Back
                body.velocity = -playerCam.transform.forward * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.9f);
            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y
            
            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
               if (timer>cd)
               {
                   body.velocity.y = speed * 2.0f * Time.deltaTime;
                   timer = 0.0f;
               }

            }

            transform.position += body.velocity;
    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
                tempBullet.GetComponent<FizziksObj>().velocity = bulletSpawn.forward * 5;
            }
        }
    }

    private void _Options()
    {
        if (Input.GetAxisRaw("MainM") > 0.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (Input.GetAxisRaw("Start") > 0.0f)
        {
            pause = !pause;
        }

    }

}
