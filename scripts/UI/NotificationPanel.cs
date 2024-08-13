using Godot;
using System;
using System.Collections.Generic;

namespace inventory;

public partial class NotificationPanel : Control
{
    [Export]
    public Font NotificationFont { get; set; } // Экспортируемое свойство для шрифта

    private VBoxContainer _notificationContainer; // Контейнер для уведомлений
    private List<Control> _notifications = new List<Control>();

    public override void _Ready()
    {
        _notificationContainer = GetNode<VBoxContainer>("GridContainer/Notifier1"); // Используем VBoxContainer
        DropItem.ItemPickedUp += ShowPickupNotification;
    }

    public void ShowPickupNotification(Texture itemIcon, string itemName)
    {
        // Проверяем и преобразуем Texture в Texture2D, если это необходимо
        Texture2D itemIcon2D = itemIcon as Texture2D;
        

        // Создаем новый HBoxContainer для размещения иконки и текста горизонтально
        HBoxContainer notification = new HBoxContainer();
        


        // Добавляем текстовое уведомление
        Label label = new Label();
        label.Text = $"Игрок подобрал {itemName}";

        // Применяем шрифт, если он задан
        if (NotificationFont != null)
        {
            label.AddThemeFontOverride("font", NotificationFont);
        }
        notification.AddChild(label);


        // Добавляем отступ
        Control spacer = new Control();
        spacer.CustomMinimumSize = new Vector2(3, 0); // Устанавливаем минимальный размер для создания отступа
        notification.AddChild(spacer);

        // Добавляем иконку предмета
        TextureRect icon = new TextureRect();
        icon.Texture = itemIcon2D; // Установка текстуры
        icon.Scale = new Vector2(2, 2); // Установка масштаба (можно изменить по необходимости)
        notification.AddChild(icon);


        // Создаем контейнер для уведомления и добавляем в него HBoxContainer
        VBoxContainer notificationWrapper = new VBoxContainer(); // Используем VBoxContainer для вертикального размещения
        notificationWrapper.AddChild(notification);

        // Добавляем уведомление в контейнер
        _notificationContainer.AddChild(notificationWrapper);
        _notifications.Add(notificationWrapper);

        // Удаляем уведомление через 4 секунды
        var timer = new Timer();
        timer.WaitTime = 8.0f;
        timer.OneShot = true;
        timer.Connect("timeout", new Callable(this, nameof(OnNotificationTimeout)));
        AddChild(timer);
        timer.Start();
    }

    private void OnNotificationTimeout()
    {
        if (_notifications.Count > 0)
        {
            var notification = _notifications[0];
            _notificationContainer.RemoveChild(notification);
            notification.QueueFree();
            _notifications.RemoveAt(0);
        }
    }
}
