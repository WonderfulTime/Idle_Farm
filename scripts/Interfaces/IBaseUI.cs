using Godot;
using System;

public interface IBaseUI
{
    /// базовый интерфейс для "пользовательского интерфейса"
    ///
    ///

    //public Vector2 PlayerPosition { set;  }


    public void ShowWindow()
    {
        /// показ окна UI
        /// проверка на существование окна, если существует, ничего не делать
        /// детект положения пользователя и отображения интерфейса относительно пользователя
       

    }

    public void HideWindow() 
    {
        /// проверка на уже открытые окна, закрытие окна
    
    }

    public void SendParamsToGameProfileManager()
    {
        /// отправка данных в синглтон GameProfileManager для учета статистики и сохранений в будущем

    }

}
