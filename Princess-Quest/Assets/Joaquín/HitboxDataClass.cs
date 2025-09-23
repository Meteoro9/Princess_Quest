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

    Vector3 offset;
    public Vector3 Offset
    {
        get { return offset; }
    }
    Vector3 size;
    public Vector3 Size
    {
        get { return size; }
    }
    Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
    }
    int damage;
    public int Damage
    {
        get { return damage; }
    }
    int pushForce;
    public int PushForce
    {
        get { return pushForce; }
    }

    public static void Set(AttackSO attackSO, GameObject hitbox)
    {
        BoxCollider hitboxColl = hitbox.GetComponent<BoxCollider>();
        hitboxColl.center = attackSO.offset;
        hitboxColl.size = attackSO.size;
    }
}
