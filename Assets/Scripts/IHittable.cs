public interface IHittable {
    void onHit(Bullet bullet);

    void onHit(BacteriophageMissile damage);
}