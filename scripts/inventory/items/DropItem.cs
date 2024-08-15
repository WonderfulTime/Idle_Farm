using Godot;
using System;

/// пространство имен инвентаря
namespace inventory;

public abstract partial class DropItem : Node2D
{
	/// <summary>
	///  родительский класс дропа предметов
	/// </summary>
	
    [Export]
    public int Value = 1; // Значение предмета, например, количество ресурсов

    [Export]
    public int MaxStack = 64; // Значение максимума стака этого предмета
    // Определяем событие на основе делегата в родительском классе
    public static event Action<string, int, Texture> ItemPickedUp;
    

    protected Texture ItemTexture;
    protected string ItemName;

    public override void _Ready()
    {
        // Общая логика для всех предметов
        ItemTexture = GetNode<Sprite2D>("ItemTexture").Texture;
        var ItemPickUpArea = GetNode<Area2D>("CollisionArea");

        ItemPickUpArea.BodyEntered += OnPickUpAreaBodyEntered;
    }


    protected virtual void OnPickUpAreaBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            //GD.Print($"{ItemName} picked up");

            // Вызываем событие
            ItemPickedUp?.Invoke(ItemName, MaxStack, ItemTexture);


            QueueFree(); // Удаляем предмет после поднятия
        }
    }


}
