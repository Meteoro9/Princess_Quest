public interface IHurtbox
{
    bool IHurtboxActive { get; set; }
    void OnHurtboxHit(HitboxData hitboxData);
}
