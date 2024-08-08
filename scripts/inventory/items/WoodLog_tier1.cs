using Godot;
using System;
namespace inventory;

public partial class WoodLog_tier1 : DropItem
{
    private string ItemName = "WoodLog_tier1";
    private Texture ItemTexture;

    //событие на основе делегата
    public static event Action<Texture, string> WoodLog_tier1PickedUp;

    public override void _Ready()
    {

        Texture ItemTexture = GetNode<Sprite2D>("ItemTexture").Texture;
        var ItemPickUpArea = GetNode<Area2D>("CollisionArea");

        ItemPickUpArea.BodyEntered += OnPickUpAreaBodyEntered;
        GD.Print("Появилось бревно на позиции: " + Position);
        GD.Print("Parent node: " + GetParent());
        Visible = true;
        SetProcess(true);
    }

    private void OnPickUpAreaBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            GD.Print("Player pickedUP wood");

            WoodLog_tier1PickedUp?.Invoke(ItemTexture, ItemName);

            //player.AddWood(Value);
            QueueFree(); // Удаляем предмет после поднятия

        }
            
        
    }
}

