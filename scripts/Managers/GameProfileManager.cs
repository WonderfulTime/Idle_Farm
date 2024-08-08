using Godot;
using System;

public interface IGameProfileManager
{



}

/// <summary>
/// синглтон для хранения всяких состояний, по типу:
/// активирован ли инвентарь, в какой области игрок
/// позиция игрока
/// ссылки на объекты в сцене для реюза
/// </summary>


public partial class GameProfileManager : Node, IGameProfileManager
{
	public Vector2 playerPos; // позиция игрока
	public bool isUIActive = false; // чек активен ли любой интерфейс для стопа игрока
	public string whatUIInArea; // чек в какой области здания находится игрок
	public bool isInventoryOpen = false; // спец булева для инвентаря


	public string playerUIManagerPath = "/root/Main/World/PlayerNode/Player/UI";
	public string inventoryGUIPath = "/root/Main/Camera2D/GUICanvas/InventoryNode/InventoryGUI";
}
