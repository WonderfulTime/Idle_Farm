using Godot;
using System;
namespace inventory;

public partial class WoodLog_tier1 : DropItem
{
    

    public override void _Ready()
    {
        ItemName = "WoodLog_tier1";  // Устанавливаем имя предмета
        base._Ready();  // Вызываем базовый метод _Ready()
    }

    protected override void OnPickUpAreaBodyEntered(Node body)
    {
        base.OnPickUpAreaBodyEntered(body);  // Вызываем базовую логику при подборе
                                             // Можно добавить дополнительную логику, специфичную для WoodLog_tier1
    }
}

