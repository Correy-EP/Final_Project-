using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollecter : MonoBehaviour

{

    PlayerStats player;
    CircleCollider2D playercollector;

    public float pullSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playercollector = GetComponent<CircleCollider2D>();
    }


    private void Update()
    {
        playercollector.radius = player.CurrentMagnet;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            // pull items toward the plaeyr
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

            Vector2 forceDirection = (transform.position - col.transform.position).normalized;

            rb.AddForce(forceDirection * pullSpeed);







            collectible.Collect();
        }
    }
}

