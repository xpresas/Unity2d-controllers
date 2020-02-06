using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public GameObject gun;
    public Rigidbody2D rb;

    public float speed = 5f;

    public GameObject crossHair;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public int bulletDmg = 5;
    bool isAiming;
    bool endOfAiming;
    // Start is called before the first frame update
    // Update is called once per frame
    Vector3 movement;
    Vector3 aim;

    public AudioSource[] bulletSounds;

    void Start()
    {
        
    }
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        ProcessInputs();
        AimAndShoot();
        Animate();
        Move();
    }

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);

        aim = aim + mouseMovement;
        if (aim.magnitude > 1f)
        {
            aim.Normalize();
        }
        isAiming = Input.GetButton("Fire1");
        endOfAiming = Input.GetButtonUp("Fire1");

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
        
    }

    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        if (aim.magnitude > 0.0f)
        {
                aim.Normalize();

                crossHair.transform.localPosition = aim;
                crossHair.SetActive(true);
            shootingDirection.Normalize();
            if (endOfAiming)
            {
                bulletSounds[0].Play();

                bulletPrefab.GetComponent<Bullet>().damage = bulletDmg;

                GameObject bullet = Instantiate(bulletPrefab, crossHair.transform.position, Quaternion.identity);//crossHair.transform.position.x, crossHair.transform.position.y, 1f
                bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
                bullet.transform.Rotate(0f, 0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(bullet, 2f);
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }
    private void Animate()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if (isAiming)
        {
            gun.GetComponent<SpriteRenderer>().enabled = true;
            gun.GetComponent<Animator>().SetBool("Aim", isAiming);
            gun.GetComponent<Animator>().SetFloat("AimHorizontal", aim.x);
            gun.GetComponent<Animator>().SetFloat("AimVertical", aim.y);
            gun.GetComponent<Animator>().SetFloat("AimMagnitude", aim.magnitude);
            animator.SetFloat("Horizontal", aim.x);
            animator.SetFloat("Vertical", aim.y);
            animator.SetFloat("Magnitude", aim.magnitude);

        }
        else
        {
            gun.GetComponent<SpriteRenderer>().enabled = false;
        }
        
        
    }
    void Move()
    {
        
        transform.position = transform.position + (movement * speed) * Time.deltaTime;
    }
    public void SwapSounds()
    {
        AudioSource temp = bulletSounds[0];
        bulletSounds[0] = bulletSounds[1];
        bulletSounds[1] = temp;
        
    }

   


}
