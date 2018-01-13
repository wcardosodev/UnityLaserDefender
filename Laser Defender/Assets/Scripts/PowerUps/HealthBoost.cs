using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : PowerUp {

    protected override IEnumerator PickupAbility(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        player.SetToMaxHealth();

        yield return new WaitForEndOfFrame();

        Destroy(gameObject);
    }
}
