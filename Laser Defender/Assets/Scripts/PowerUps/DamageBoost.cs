using UnityEngine;
using System.Collections;

public class DamageBoost : PowerUp
{
    public float multiplier = 1.5f;

    protected override IEnumerator PickupAbility(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        for(int i = 0; i < player.projectiles.Length; i++)
        {
            player.projectiles[i].GetComponent<Projectile>().damage *= multiplier;
        }

        yield return new WaitForSeconds(buffTime);

        for (int i = 0; i < player.projectiles.Length; i++)
        {
            player.projectiles[i].GetComponent<Projectile>().damage /= multiplier;
        }

        Destroy(gameObject);
    }
}
