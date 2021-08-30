using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField] int speed = 5;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject powerBullet;
    [SerializeField] float nextFire;
    [SerializeField] bool fire = false;
    [SerializeField] GameObject ui;
    public Sprite[] sprites;
    private float tamX;
    private float tamY;

    private int s = 0;

    float minX, maxX, minY, maxY, fireInterval;
    void Start()
    {
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;

        Vector2 esquinaSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esquinaSupDer.x - tamX / 2;
        maxY = esquinaSupDer.y - tamY / 2;

        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esquinaInfIzq.x + tamX / 2;
        minY = esquinaInfIzq.y + 7;

    }
    void Update()
    {
        Movement();
        Fire();
        if(Input.GetKeyDown(KeyCode.Z)){
            fire = !fire;
            ui.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[s];
            if(s==0){
                s=1;
            }else if(s==1){
                s=0;
            }
        }
        //if (Input.GetKey(KeyCode.RightArrow))
        //    transform.Translate(new Vector2(0.1f, 0));

        //if (Input.GetKey(KeyCode.LeftArrow))
        //    transform.Translate(new Vector2(-0.1f, 0));

        //if (Input.GetKey(KeyCode.DownArrow))
        //    transform.Translate(new Vector2(0, -0.1f));

        //if (Input.GetKey(KeyCode.UpArrow))
        //    transform.Translate(new Vector2(0, 0.1f));
    }

    void Movement()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(movH * Time.deltaTime * speed, movV * Time.deltaTime * speed));

        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y, minY, maxY);


        transform.position = new Vector2(newX, newY);
    }
    void Fire()
    {
        if (fire == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= fireInterval+2)
            {
                Instantiate(powerBullet, transform.position - new Vector3(0, tamY / 2, 0), transform.rotation);
                fireInterval = Time.time + nextFire;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= fireInterval)
            {
                Instantiate(bullet, transform.position - new Vector3(0, tamY / 2, 0), transform.rotation);
                fireInterval = Time.time + nextFire;
            }
        }
    }
}
