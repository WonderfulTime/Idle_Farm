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

    [Export] public int itemID;
    // Определяем событие на основе делегата в родительском классе
    //public static event Action<string, int, Texture, int, int> ItemPickedUp;

    public delegate int ItemPickedUpHandler(string ItemName, int MaxStack, Texture ItemTexture, int Value, int itemID);
    public static event ItemPickedUpHandler ItemPickedUp;
    public static event Action<string, int, Texture, int, int> NotificationItemPickedUp;


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
            int remainingItemValue = ItemPickedUp?.Invoke(ItemName, MaxStack, ItemTexture, Value, itemID) ?? Value;

            if (remainingItemValue == Value) // случай когда ни один предмет не взялся с пола
            {
                GD.Print($"Предмет не может быть подобран");

            }


            else if (remainingItemValue > 0) // если 
            {
                var addingValue = Value - remainingItemValue; // сколько предметов добавленно в инвентарь
                Value = remainingItemValue; // сколько предметов осталось на земле

                NotificationItemPickedUp?.Invoke(ItemName, MaxStack, ItemTexture, addingValue, itemID); // уведомление о поднятие предмета

                GD.Print($"Остаток {ItemName} не добавленный в инвентарь {Value}"); // дебаг сколько предметов осталось на земле
                
            }
            
            else /*(remainingItemValue <=0)*/
            {
                NotificationItemPickedUp?.Invoke(ItemName, MaxStack, ItemTexture, Value, itemID); // уведомление о поднятие предмета
                QueueFree();
            }

            //if (ispickedUP == true)
            //{ QueueFree(); }// Удаляем предмет после поднятия
            
        }
    }


}
