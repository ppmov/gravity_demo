using System.Collections.Generic;
using UnityEngine;

public static class Counter
{
    public static int Seconds => Mathf.FloorToInt(Time.time - startTime);
    public static int Score { get; private set; }
    public static float ContactLifetime => Time.fixedDeltaTime * 2;

    private static Queue<FigureContact> lastContacts;
    private static float startTime;

    static Counter() => Reset();

    public static void Reset()
    {
        startTime = Time.time;
        lastContacts = new Queue<FigureContact>();
        Score = 0;
    }

    /// <summary> Добавление контакта тел для ведения счёта. Контакт тех же тел будет невозможен еще ContactLifetime секунд. </summary>
    /// <param name="newContact"> Информация о столкновении фигур. Может быть неявно преобразована из ContactPoint. </param>
    public static void AddContact(FigureContact newContact)
    {
        RemoveLegacyContacts();

        // контакты сравниваются согласно FigureContact.Equals
        if (lastContacts.Contains(newContact))
            return;

        Score++;
        lastContacts.Enqueue(newContact);
    }

    /// <summary> Удаление устаревших записей в порядке очереди </summary>
    private static void RemoveLegacyContacts()
    {
        while (lastContacts.Count > 0)
        {
            if (lastContacts.Peek().TimeSpent <= ContactLifetime)
                break;

            lastContacts.Dequeue();
        }
    }
}
