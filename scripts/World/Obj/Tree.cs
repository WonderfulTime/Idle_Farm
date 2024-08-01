using Godot;
using inventory;
using System;

public partial class Tree : Node2D
{
    [Export]
    public PackedScene DropItemScene;

    [Export]
    public int ItemCount = 3;

    [Export]
    public float DropChance = 0.5f;

    private bool isChopped = false;
    private DropSystem dropSystem;


    private AnimatedSprite2D _animatedSprite_1;

    public override void _Ready()
    {
        _animatedSprite_1 = GetNode<AnimatedSprite2D>("TreeAnimation");
        var area = GetNode<Area2D>("ChopZone");
        area.BodyEntered += OnTreeBodyEntered;
        _animatedSprite_1.AnimationFinished += OnAnimationFinished;


        dropSystem = new DropSystem();
        AddChild(dropSystem);
    }

    private void OnTreeBodyEntered(Node body)
    {
        if (body.Name == "Player" && !isChopped)
        {
            GD.Print("OnTreeBodyEntered");
            ChopTree();
        }
    }

    private void ChopTree()
    {
        GD.Print("Срубил дерево");
        _animatedSprite_1.Animation = "falling";
        isChopped = true;
        //dropSystem.DropItems(DropItemScene, GlobalPosition, ItemCount, DropChance);
        //QueueFree();
    }


    private void OnAnimationFinished()
    {
        if (_animatedSprite_1.Animation == "falling" && isChopped)
        {
            dropSystem.DropItems(DropItemScene, Position, ItemCount, DropChance);
            //CallDeferred("queue_free");
        }
    }
}
