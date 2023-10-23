public interface IHittable
{
    void onHit(Bullet bullet);

    void onHit(IDamage explosion);
    void onHit(float damage);

}

public interface IDamage
{
    float damage { get; }
}