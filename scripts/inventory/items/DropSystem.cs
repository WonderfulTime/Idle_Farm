using Godot;
using System;
namespace inventory;

public partial class DropSystem : Node2D
{
    public void DropItems(PackedScene dropItemScene, Vector2 position, int itemCount, float dropChance)
    {
        Random random = new Random();

        for (int i = 0; i < itemCount; i++)
        {
            if (random.NextDouble() <= dropChance)
            {
                DropItem drop = (DropItem)dropItemScene.Instantiate();
                //drop.Position = position + new Vector2((float)GD.RandRange(-10, 10), (float)GD.RandRange(-10, 10));
                //drop.Position = position;
                drop.Position = new Vector2((float)GD.RandRange(0, 40), (float)GD.RandRange(0, 10));
                GetParent().CallDeferred("add_child", drop);
                //GD.Print(drop.Position);
            }
        }
    }
}
