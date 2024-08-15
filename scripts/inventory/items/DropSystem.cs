using Godot;
using System;
using System.Collections.Generic;
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

    public void DropItems(List<DropItemInfo> dropsItems,  Vector2 position)
    {
        if (dropsItems == null)
        {
            GD.PrintErr("Список dropsItems равен null!");
            return;
        }

        Random random = new Random();

        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");
        Node2D dropsNode = GetNode<Node2D>(profileManager.GlobalItemDropsNode);

        foreach (var dropItemInfo in dropsItems)
        {
            // Если шанс дропа срабатывает для текущего предмета
            if (random.NextDouble() <= dropItemInfo.DropChance)
            {
                // Определяем количество дропа в диапазоне от 1 до MaxCount
                int itemCount = random.Next(1, dropItemInfo.MaxCount + 1);

                for (int i = 0; i < itemCount; i++)
                {
                    DropItem drop = (DropItem)dropItemInfo.DropScene.Instantiate();
                    drop.GlobalPosition = position + new Vector2((float)GD.RandRange(0, 40), (float)GD.RandRange(0, 10));
                    dropsNode.CallDeferred("add_child", drop);
                }
            }
        }
    }
}
