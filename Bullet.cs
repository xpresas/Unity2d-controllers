using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public CircleCollider2D col;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            
        }
        Destroy(gameObject);
    } 
}
