using Godot;
using System;
namespace inventory;

public partial class DropSystem : Node2D
{
    /// <summary>
    /// система отвечающая за дроп предметов
    /// </summary>
    /// <param name="dropItemScene"></param>
    /// <param name="position"></param>
    /// <param name="itemCount"></param>
    /// <param name="dropChance"></param>

    private GameProfileManager profileManager;

    public void DropItems(PackedScene dropItemScene, Vector2 position, int itemCount, float dropChance)
    {
        Random random = new Random();

        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");
        Node2D dropsNode = GetNode<Node2D>(profileManager.GlobalItemDropsNode);

        for (int i = 0; i < itemCount; i++)
        {
            if (random.NextDouble() <= dropChance)
            {
                DropItem drop = (DropItem)dropItemScene.Instantiate();
                //drop.Position = position + new Vector2((float)GD.RandRange(-10, 10), (float)GD.RandRange(-10, 10));
                //drop.Position = position;
                drop.GlobalPosition = position + new Vector2((float)GD.RandRange(0, 40), (float)GD.RandRange(0, 10));
                dropsNode.CallDeferred("add_child", drop);
                //GD.Print(drop.Position);

                
            }
        }
    }
}
