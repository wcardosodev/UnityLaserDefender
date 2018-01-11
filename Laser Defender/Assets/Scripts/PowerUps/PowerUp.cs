using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

    [SerializeField]
    protected GameObject pickupEffect;
    [SerializeField]
    protected float buffTime = 5f, fallSpeed = 2.5f;

    [SerializeField]
    protected AudioClip pickupSound;

    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>())
        {
            Pickup(collision);
        }
    }

    void Pickup(Collider2D collision)
    {
        GameObject effect = null;

        if (pickupEffect)
        {
            effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        }
        if (pickupSound)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        StartCoroutine(PickupAbility(collision));

        if (effect != null)
        {
            Destroy(effect);
        }
    }

    protected abstract IEnumerator PickupAbility(Collider2D collision);
}
