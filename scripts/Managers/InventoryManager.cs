using Godot;
using System;

namespace inventory;
/// <summary>
/// Тут осуществляется хранение всех вещей в инвентаре игрока на постоянке
/// Также обработка событий, таких как удаление вещи из инвентаря/добавление в инвентарь
/// </summary>



public partial class InventoryManager: Node
{
    private GameProfileManager profileManager;
    public override void _Ready()
    {
        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");
        // Подписываемся на событие
        WoodLog_tier1.WoodLog_tier1PickedUp += AddItemToInventory;
    }

    public void AddItemToInventory(Texture ItemTexture, string ItemName)
    {
        GD.Print($"Текстура{ItemTexture} в инвентаре");
        GD.Print($"Предмет {ItemName} в инвентаре");
        
    }

    //public void RemoveItemFromInventory(Item item, int quantity)
    //{
    //    PlayerInventory.RemoveItem(item, quantity);
    //}

}
