using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    float timeLeft = 10.0f; // Время, оставшееся до окончания таймера
    public Text text; // Ссылка на компонент Text для отображения времени

    public GameManagerX gmX; // Ссылка на объект GameManagerX

    // Метод Update вызывается один раз за кадр
    void Update()
    {
        timeLeft -= Time.deltaTime; // Уменьшаем оставшееся время на время, прошедшее с последнего кадра
        text.text = "Time Left: " + Mathf.Round(timeLeft); // Обновляем текст с оставшимся временем
        if (timeLeft < 0) // Если время истекло
        {
            gmX.GameOver(); // Завершаем игру
        }
    }
}
