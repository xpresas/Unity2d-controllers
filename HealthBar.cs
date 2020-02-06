using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool playerStatus = true;
    public int powerUpCounter = 0;
    public GameObject doubleBulletPrefab;

    public AudioSource oofSound;
    [Header("PowerUp Settings")]
    public int RndSpeed;
    public int RndDoubleShot;
    public int RndHealthMod;
    [Header("InstaKill?")]
    public bool instaKill = false;
    void Start()
    {
        RndSpeed = Random.Range(5, 10);
        RndDoubleShot = Random.Range(5, 10);
        RndHealthMod = Random.Range(5, 10);
    }
    // Update is called once per frame
    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        if (!playerStatus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].GetComponent<Animator>().SetBool("isFull", true);
                hearts[i].GetComponent<Animator>().SetBool("isEmpty", false);
                //hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<Animator>().SetBool("isFull", false);
                hearts[i].GetComponent<Animator>().SetBool("isEmpty", true);
                //hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                
                hearts[i].enabled = true;
            }
            else
            {
                
                hearts[i].enabled = false;
            }
        }
    }


    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        int damage = 1;
        EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            if (instaKill)
            {
                damage = 5;
            }
            health -= damage;
            oofSound.Play();
            if (health == 0)
            {
                health = 0; // uztikrinam
                
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                hearts[0].GetComponent<Animator>().SetBool("isFull", false);
                hearts[0].GetComponent<Animator>().SetBool("isEmpty", true);
                
                gameObject.GetComponent<Animator>().SetTrigger("Death");
                Destroy(gameObject,1f);

                playerStatus = false;
            }
        }
        else if (hitInfo.tag == "PowerUpSpeed")
        {
            powerUpCounter++;
            if (RndSpeed > powerUpCounter )
            {
                GetComponent<PlayerMovement>().speed = GetComponent<PlayerMovement>().speed + 0.2f;
                GetComponent<PlayerMovement>().bulletDmg = GetComponent<PlayerMovement>().bulletDmg + 1;
                hitInfo.gameObject.GetComponent<Animator>().SetTrigger("Taken");
                hitInfo.gameObject.GetComponent<AudioSource>().Play();
                Destroy(hitInfo.gameObject, 1f);
            }
            else if (powerUpCounter == RndDoubleShot)
            {
                GetComponent<PlayerMovement>().bulletPrefab = doubleBulletPrefab;
                GetComponent<PlayerMovement>().SwapSounds();
                hitInfo.gameObject.GetComponent<Animator>().SetTrigger("Taken");
                hitInfo.gameObject.GetComponent<AudioSource>().Play();
                Destroy(hitInfo.gameObject, 1f);
            }
            else
            {
                if (powerUpCounter % RndHealthMod == 0)
                {
                    health++;
                }
                hitInfo.gameObject.GetComponent<Animator>().SetTrigger("Taken");
                hitInfo.gameObject.GetComponent<AudioSource>().Play();
                Destroy(hitInfo.gameObject, 1f);
            }
            
        }

    }
}
