using Godot;
using System;


public partial class garden : Node2D
{
    private PlayerUIManager playerUIManager;
    private GameProfileManager profileManager;
    private string BodyBuildName = "Garden";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");

        playerUIManager = GetNode(profileManager.playerUIManagerPath) as PlayerUIManager;

        if (playerUIManager == null)
        {
            GD.PrintErr("PlayerUIManager не найден в сцене!");
            //GD.Print(GetTree());
            //GD.Print(GetPath());  // Выводит путь текущего узла
            //GD.Print(GetParent().GetPath());  // Выводит путь родителя
        }
        // Получение ссылки на Area2D и подключение сигнала
        var area = GetNode<Area2D>("InteractionArea");
        area.BodyEntered += OnPlayerHouseBodyEntered;
        area.BodyExited += OnPlayerHouseBodyExited;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void OnPlayerHouseBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            GD.Print("Player in house area");
            //OnBodyEntered(body);
            playerUIManager.ShowInterractButtonIcon(BodyBuildName);
        }

    }

    private void OnPlayerHouseBodyExited(Node body)
    {
        if (body.Name == "Player")
        {
            GD.Print("Player leave house area");
            //OnBodyExited(body);
            playerUIManager.HideInterractButtonIcon();

        }

    }
}
