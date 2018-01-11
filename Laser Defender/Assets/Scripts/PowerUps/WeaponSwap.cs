using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : PowerUp {

    public int highestPlayerWeapon = 3;

    private void Start()
    {
        StopCoroutine(PickupAbility(null));
    }

    protected override IEnumerator PickupAbility(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        player.weapon = Random.Range(2, highestPlayerWeapon + 1);

        yield return new WaitForSeconds(buffTime);

        player.weapon = 1;
    }
}
