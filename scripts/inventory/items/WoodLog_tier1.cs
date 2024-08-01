using Godot;
using System;
namespace inventory;

public partial class WoodLog_tier1 : DropItem
{
    public override void _Ready()
    {
        GD.Print("Появилось бревно на позиции: " + Position);
        GD.Print("Parent node: " + GetParent());
        Visible = true;
        SetProcess(true);
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        // Дополнительная логика для WoodLog
        //player.AddWood(Value);
        QueueFree(); // Удаляем предмет после поднятия
    }
}

