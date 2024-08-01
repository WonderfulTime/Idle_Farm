using Godot;
using System;

public partial class PlayerUIManager : Node2D
{

    private Sprite2D EButtonIcon;
    private GameProfileManager profileManager;

    //public CollisionShape2D PlayerBody;


    public override void _Ready()
    {
        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");
        //PlayerBody = GetNode<CollisionShape2D>("./CollisionShape2D");
        EButtonIcon = GetNode<Sprite2D>("EButtonIcon");
        EButtonIcon.Visible = false; // Скрываем иконку по умолчанию
    }

    public void ShowInterractButtonIcon(string BodyBuildName)
    {
        if (EButtonIcon != null)
        {

            EButtonIcon.Visible = true;
            profileManager.whatUIInArea = BodyBuildName;
        }
        else
        {
            GD.PrintErr("EButtonIcon is null!");
        }
    }

    public void HideInterractButtonIcon()
    {
        EButtonIcon.Visible = false;
        profileManager.whatUIInArea = "";
    }
}

