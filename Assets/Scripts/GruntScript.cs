using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GruntScript : MonoBehaviour
{
    public GameObject BulletPrefab;

    public GameObject Jhon;

    private float LastShoot;
    private int Health = 3;

    private void Update()
    {
        if (Jhon == null) return;

        Vector3 direction = Jhon.transform.position - transform.position;
        if (direction.x > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs( Jhon.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Stoot();
            LastShoot = Time.time;
        }
    }

    private void Stoot() 
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);

    }
}
