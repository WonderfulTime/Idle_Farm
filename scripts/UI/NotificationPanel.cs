using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace inventory;

public partial class NotificationPanel : Control
{
    [Export]
    public Font NotificationFont { get; set; } // Экспортируемое свойство для шрифта

    [Export]
    public int fontSize { get; set; } //размер шрифта

    [Export]
    public Color ShadowColor { get; set; } = Colors.Black; // Цвет тени

    private VBoxContainer _notificationContainer; // Контейнер для уведомлений
    private Dictionary<string, Label> _notificationLabels = new Dictionary<string, Label>(); // Хранит ссылки на уведомления для объединения
    private Dictionary<string, int> _itemCounts = new Dictionary<string, int>(); // Хранит количество предметов

    public override void _Ready()
    {
        _notificationContainer = GetNode<VBoxContainer>("GridContainer/Notifier1");
        DropItem.NotificationItemPickedUp += ShowPickupNotification;
    }

    public void ShowPickupNotification(string itemName, int MaxStack, Texture itemIcon, int Value, int itemID)
    {
        // Если уведомление для данного предмета уже существует, обновляем его
        if (_notificationLabels.ContainsKey(itemName))
        {
            _itemCounts[itemName] += Value;
            _notificationLabels[itemName].Text = $"Игрок подобрал x{_itemCounts[itemName]} {itemName}";
            return; // Прерываем выполнение, т.к. уведомление уже обновлено
        }

        // Проверяем и преобразуем Texture в Texture2D, если это необходимо
        Texture2D itemIcon2D = itemIcon as Texture2D;

        // Создаем новый HBoxContainer для размещения иконки и текста горизонтально
        HBoxContainer notification = new HBoxContainer();

        // Добавляем текстовое уведомление
        // самое первое уведомление при вызове
        Label label = new Label();
        label.Text = $"Игрок подобрал x{Value} {itemName}";

        // Применяем шрифт, если он задан
        if (NotificationFont != null)
        {
            label.AddThemeFontOverride("font", NotificationFont);
            label.AddThemeFontSizeOverride("font_size", fontSize);
            //label.AddThemeConstantOverride("shadow", 7); // не робит
            label.AddThemeColorOverride("font_shadow_color", ShadowColor);
        }
        notification.AddChild(label);

        // Добавляем отступ
        Control spacer = new Control();
        spacer.CustomMinimumSize = new Vector2(3, 0);
        notification.AddChild(spacer);

        // Добавляем иконку предмета
        TextureRect icon = new TextureRect();
        icon.Texture = itemIcon2D;
        icon.Scale = new Vector2(2, 2);
        notification.AddChild(icon);

        // Добавляем уведомление в контейнер
        _notificationContainer.AddChild(notification);

        // Сохраняем ссылку на Label и счетчик предметов, позволяет избавиться от дубликатов
        _notificationLabels[itemName] = label;
        _itemCounts[itemName] = Value;


        // Создаем Tween для анимации
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(notification, "scale", new Vector2(1.2f, 1.2f), 0.2f)
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.InOut);

        tween.TweenProperty(notification, "scale", new Vector2(1.0f, 1.0f), 0.2f)
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.InOut);




        // Удаляем уведомление через 8 секунд
        var timer = new Timer();
        timer.WaitTime = 8.0f;
        timer.OneShot = true;
        //timer.Autostart = true; // Устанавливаем AutoStart в true
        timer.Connect("timeout", new Callable(this, nameof(OnNotificationTimeout)));
        AddChild(timer);
        timer.Start();
    }

    private void OnNotificationTimeout()
    {
        // Находим уведомление, которое нужно удалить
        foreach (var notification in _notificationContainer.GetChildren())
        {
            if (notification is Control control)
            {
                _notificationContainer.RemoveChild(control);
                control.QueueFree();
                //break; // Удаляем только первое уведомление
            }
        }

        // Удаляем уведомление из списка
        if (_notificationLabels.Count > 0)
        {
            var firstItem = _notificationLabels.Keys.First();
            _notificationLabels.Remove(firstItem);
            _itemCounts.Remove(firstItem);
        }
    }
}
