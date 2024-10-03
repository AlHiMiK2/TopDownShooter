using Zenject;

public class EnemyFactory : IFactory<UnityEngine.Object, Enemy>
{
    private DiContainer _container;

    public EnemyFactory(DiContainer container)
    {
        _container = container;
    }

    public Enemy Create(UnityEngine.Object prefab)
    {
        return _container.InstantiatePrefabForComponent<Enemy>(prefab);
    }
}
