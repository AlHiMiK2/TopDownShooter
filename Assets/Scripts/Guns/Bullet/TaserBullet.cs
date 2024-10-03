using UnityEngine;

public class TaserBullet : Bullet
{
    private void Update()
    {
        ElapsedLifeTime += Time.deltaTime;

        if (ElapsedLifeTime > LifeTime)
        {
            Destroy(gameObject);
        }
        
        Move();
        
        if(!TryGetHit(out var hit)) return;
        Damage(hit);
    }
}
