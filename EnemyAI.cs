using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public Animator animator;
    public float speed = 3f;
    public int health = 10;
    public float playerSearchDistance = 6f;
    // Start is called before the first frame update
    private Vector3 idleRightPos;
    private Vector3 idleLeftPos;
    private bool idleStatus;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        idleRightPos = transform.position + new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0f);
        idleLeftPos = transform.position + new Vector3(-Random.Range(0, 2), -Random.Range(0, 2), 0f);
    }
    private Vector3 previousPosition;
    private Vector3 direction;

    public AudioSource HitSound;
    public AudioSource DeathSound;

    // Update is called once per frame

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < playerSearchDistance)
        {
            FollowPlayer();
        }
        else
        {
            IdleWalk();
        }
        
    }

    public void FollowPlayer()
    {
        Animate();

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if ((Vector2.Distance(transform.position, target.position) < gameObject.GetComponent<CircleCollider2D>().radius + 0.3f)) //offset
        {
            animator.SetTrigger("Attacking");
        }
    }
    public void IdleWalk()
    {
        if (idleStatus)
        {
            Animate();
            transform.position = Vector2.MoveTowards(transform.position, idleRightPos, 1f * Time.deltaTime);
            if (Vector2.Distance(transform.position, idleRightPos) < 0.5f)
            {
                idleStatus = false;
            }
        }
        else
        {
            Animate();
            transform.position = Vector2.MoveTowards(transform.position, idleLeftPos, 1f * Time.deltaTime);
            if (Vector2.Distance(transform.position, idleLeftPos) < 0.5f)
            {
                idleStatus = true;
            }

        }
    }
    public void Animate()
    {
        direction = -(previousPosition - transform.position).normalized;



        animator.SetFloat("Horizontal", direction.normalized.x);
        animator.SetFloat("Vertical", direction.normalized.y);
        animator.SetFloat("Magnitude", direction.normalized.magnitude);
        previousPosition = transform.position;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        HitSound.Play();
        animator.SetTrigger("Damaged");
        if (health <= 0)
        {
            speed = 0f;
            animator.SetTrigger("Death");
            removeMe();
        }
    }
    
    // Call this when you want to kill the enemy
    void removeMe()
    {
        DeathSound.Play();
        Destroy(gameObject, 1f);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }


}
