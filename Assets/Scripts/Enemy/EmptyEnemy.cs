using UnityEngine.Events;

public class EmptyEnemy : Enemy
{
    public override event UnityAction OnAttack;
    
    protected override void OnSpawned()
    {
    }
}
