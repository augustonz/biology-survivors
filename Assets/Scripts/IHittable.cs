public interface IHittable {
    void onHit(Bullet bullet);

    void onHit(float damage);
}