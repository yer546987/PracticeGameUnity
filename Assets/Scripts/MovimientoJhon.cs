using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoJhon : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    private new Rigidbody2D rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("Running", Horizontal != 0.0f );

        Debug.DrawRay(transform.position, Vector3.down * 0.1F, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

    private void Jump() 
    {
        rigidbody2D.AddForce(Vector2.up * JumpForce);
    
    }

    private void Shoot() 
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet =  Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(Horizontal * Speed, rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);

    }
}
