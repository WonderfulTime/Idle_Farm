using Godot;
using System;


public partial class ParentInventoryGUI : Node2D, IBaseUI
{
    

    public void ShowWindow(InventoryPaths invpaths, GameProfileManager profileManager)
    {
        GD.Print(profileManager.playerPos);


        //Vector2 playerPosition = profileManager.playerPos;
        //Vector2 inventorySize = UIScene.Size;
        //Vector2 inventoryPosition = playerPosition;
        //UIScene.Position = inventoryPosition;


        invpaths.UIScene.Visible = true;
        profileManager.isInventoryOpen = true; // юишка активна


        GD.Print("Позиция инвентаря: "+ invpaths.UIScene.Position);


    }

    public void HideWindow(InventoryPaths invpaths, GameProfileManager profileManager)
    {



        invpaths.UIScene.Visible = false;
        profileManager.isInventoryOpen = false;

    }

    public void ShowAgricultureInv(InventoryPaths invpaths, GameProfileManager profileManager)
    {



        invpaths.BaseInvPath.Visible = false;
        invpaths.AgricultureInvPath.Visible = true;
        profileManager.isInventoryOpen = true; // юишка активна


    }

    public void ShowBaseInv(InventoryPaths invpaths, GameProfileManager profileManager)
    {



        invpaths.BaseInvPath.Visible = true;
        invpaths.AgricultureInvPath.Visible = false;
        profileManager.isInventoryOpen = true; // юишка активна


    }

    public void SendParamsToGameProfileManager()
    {


    }

}



public class InventoryPaths
{
    // пути для инвентаря
    public Control UIScene { get; set; }
    public NinePatchRect BaseInvPath { get; set; }
    public NinePatchRect AgricultureInvPath { get; set; }
    public Button InvButtonBaseInv { get; set; }
    public Button InvButtonAgriculture { get; set; }


    public InventoryPaths(Control uIScene, NinePatchRect baseInvPath, NinePatchRect agricultureInvPath, Button invButtonBaseInv, Button invButtonAgriculture)
    {
        UIScene = uIScene;
        BaseInvPath = baseInvPath;
        AgricultureInvPath = agricultureInvPath;
        InvButtonBaseInv = invButtonBaseInv;
        InvButtonAgriculture = invButtonAgriculture;

    }
}



public partial class InventoryGUI : Node
{

    private ParentInventoryGUI _inventoryGUI;
    private Control _uIScene;
    private NinePatchRect _baseInvPath;
    private NinePatchRect _aggricultureInvPath;
    private Button _invButtonBaseInv;
    private Button _invButtonAgriculture;
    


    private InventoryPaths invpaths;


    private bool _uiIsVisible;

    private GameProfileManager profileManager;



    public override void _Ready()
	{
        _uIScene = GetNode<Control>("InventoryGUI");
        _baseInvPath = _uIScene.GetNode<NinePatchRect>("BaseInventory");
        _aggricultureInvPath = _uIScene.GetNode<NinePatchRect>("AgricultureInventory");
        _invButtonBaseInv = _uIScene.GetNode<Button>("ChangeOptionButtonContainer/BaseInvShowButton");
        _invButtonAgriculture = _uIScene.GetNode<Button>("ChangeOptionButtonContainer/AgricultureButton");


        invpaths = new InventoryPaths(_uIScene, _baseInvPath, _aggricultureInvPath, _invButtonBaseInv, _invButtonAgriculture);


        invpaths.UIScene.Visible = false;
        invpaths.AgricultureInvPath.Visible = false;



        invpaths.InvButtonAgriculture.Pressed += OnAgricultureButtonPressed;
        invpaths.InvButtonBaseInv.Pressed += OnBaseInvButtonPressed;


        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");

        _inventoryGUI = new ParentInventoryGUI();

    }




    private void OnBaseInvButtonPressed()
    {
        // нажата кнопка базового инвентаря
        _inventoryGUI.ShowBaseInv(invpaths, profileManager);
    }




    private  void OnAgricultureButtonPressed()
    {
        //нажата кнопка инвентаря с сельскохозяйственными культурами
        GD.Print("Нажата кнопка показа культур");
        _inventoryGUI.ShowAgricultureInv(invpaths, profileManager);
    }






    public override void _Input(InputEvent @event)
    {
        // Проверка, что событие - это нажатие клавиши, и если кнопка взаимодействия была нажата
        if (@event.IsActionPressed("inventory"))
        {
            if (_uiIsVisible)
            {
                // Скрываем UI
                _inventoryGUI.HideWindow(invpaths, profileManager);
                _uiIsVisible = false;
            }
            else
            {
                if (profileManager.isUIActive == false)
                {
                    // Показываем UI
                    _inventoryGUI.ShowWindow(invpaths, profileManager);
                    _uiIsVisible = true;
                }
                    
                

            }
        }


    }






    public override void _Process(double delta)
    {

        //if (Input.IsMouseButtonPressed(MouseButton.Left))
        //{
        //    GD.Print("Mouse Left Button Pressed");
        //}

    }
}
