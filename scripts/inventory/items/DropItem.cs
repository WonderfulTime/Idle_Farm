using Godot;
using System;

/// пространство имен инвентаря
namespace inventory;

public partial class DropItem : Node2D
{
	/// <summary>
	///  родительский класс дропа предметов
	/// </summary>
	
    [Export]
    public int Value = 1; // Значение предмета, например, количество ресурсов
                          
    

    public virtual void OnPickUp()
    {
        // Логика, которая выполняется при поднятии предмета
        GD.Print("Item picked up by player");
    }
}
