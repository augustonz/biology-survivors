public interface IHittable {
    void onHit(Bullet bullet);

    void onHit(ExplosionEfetivation explosion);
}