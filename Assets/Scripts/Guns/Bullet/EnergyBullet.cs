using UnityEngine;

public class EnergyBullet : Bullet
{
    private void Update()
    {
        if(IsHit) return;
        
        ElapsedLifeTime += Time.deltaTime;

        if (ElapsedLifeTime > LifeTime)
        {
            Destroy(gameObject);
        }
        
        Move();
        
        if(!TryGetHit(out var hit)) return;
        Damage(hit);
        DieWithAnimation();
        
        IsHit = true;
    }
}
