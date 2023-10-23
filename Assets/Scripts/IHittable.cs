public interface IHittable
{
    void onHit(Bullet bullet);

    void onHit(IDamage explosion);
}

public interface IDamage
{
    float damage { get; }
}