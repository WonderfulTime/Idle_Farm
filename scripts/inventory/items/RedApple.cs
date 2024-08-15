using Godot;
using System;
namespace inventory;

public partial class RedApple : DropItem
{

    public override void _Ready()
    {
        ItemName = "RedApple";  // Устанавливаем имя предмета
        base._Ready();  // Вызываем базовый метод _Ready()
    }

    protected override void OnPickUpAreaBodyEntered(Node body)
    {
        base.OnPickUpAreaBodyEntered(body);  // Вызываем базовую логику при подборе
                                             // Можно добавить дополнительную логику, специфичную для WoodLog_tier1
    }
}
