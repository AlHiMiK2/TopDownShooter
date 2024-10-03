using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Player PlayerPrefab;
    public Transform PlayerSpawnPoint;
    public Inventory Inventory;
    public GUI GUI;

    public override void InstallBindings()
    {
        BindGUI();
        BindEnemyFactory();
        BindInventory();
        BindPlayer();
    }

    private void BindEnemyFactory()
    {
        Container
            .BindFactory<UnityEngine.Object, Enemy, Enemy.Factory>()
            .FromFactory<EnemyFactory>();
    }

    private void BindGUI()
    {
        Container
            .Bind<GUI>()
            .FromInstance(GUI)
            .AsSingle();
    }

    private void BindPlayer()
    {
        Player player = Container
            .InstantiatePrefabForComponent<Player>(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity, null);

        Container
            .Bind<Player>()
            .FromInstance(player) 
            .AsSingle(); 
    }

    private void BindInventory()
    {
        Container
            .Bind<Inventory>()
            .FromInstance(Inventory)
            .AsSingle();
    }
}
