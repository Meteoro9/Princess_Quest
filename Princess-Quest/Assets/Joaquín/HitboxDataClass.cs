using UnityEngine;

public class HitboxData
{
    public HitboxData(Vector3 offset, Vector3 size, Vector3 direction, int damage, int pushForce)
    {
        this.offset = offset;
        this.size = size;
        this.direction = direction;
        this.damage = damage;
        this.pushForce = pushForce;
    }

    public HitboxData(AttackSO attackSO)
    {
        offset = attackSO.offset;
        size = attackSO.size;
        direction = attackSO.direction;
        damage = attackSO.damage;
        pushForce = attackSO.pushForce;
    }

    // Creates a HitboxData class instance with a damage of 1, all other values are null
    public HitboxData(GameObject hitter, int dmg = 1)
    {
        damage = dmg;
        this.hitter = hitter;
    }

    public readonly Vector3 offset;
    public readonly Vector3 size;
    public readonly Vector3 direction;
    public readonly int damage;
    public readonly int pushForce;

    GameObject hitter;

    public GameObject Hitter
    {
        get { return hitter; }
    }

    public static void Set(AttackSO attackSO, GameObject hitbox)
    {
        HitboxData newHitboxData = new HitboxData(attackSO);
        newHitboxData.hitter = hitbox.transform.parent.gameObject;

        BoxCollider hitboxColl = hitbox.GetComponent<BoxCollider>();
        hitboxColl.center = newHitboxData.offset;
        hitboxColl.size = newHitboxData.size;

        Hitbox newHitbox = hitbox.GetComponent<Hitbox>();
        newHitbox.hitboxData = newHitboxData;
    }
}
