using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] bool direction;
    [SerializeField] int vidas = 3;
    [SerializeField] Sprite vidaVacia;
    private float maxX;
    private float minX;
    void Start()
    {
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        maxX = esquinaInfDer.x;

        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esquinaInfIzq.x;
    }
    void Update()
    {
        if (direction)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }
        else
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        }

        if (transform.position.x >= maxX)
            direction = false;
        else if (transform.position.x <= minX)
            direction = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            transform.GetChild(vidas - 1).gameObject.GetComponent<SpriteRenderer>().sprite = vidaVacia;
            vidas = vidas - 1;
        }
        if (collision.gameObject.CompareTag("powerBullet"))
        {
            if (vidas == 3)
            {
                transform.GetChild(vidas - 1).gameObject.GetComponent<SpriteRenderer>().sprite = vidaVacia;
                transform.GetChild(vidas - 2).gameObject.GetComponent<SpriteRenderer>().sprite = vidaVacia;
                vidas = vidas - 2;
            }
            else if (vidas > 0 && vidas <=2)
            {
                transform.GetChild(vidas - 1).gameObject.GetComponent<SpriteRenderer>().sprite = vidaVacia;
                vidas = vidas - 2;
            }
        }
        if (vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
