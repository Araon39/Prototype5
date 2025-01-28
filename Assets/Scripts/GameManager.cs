using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets; // Список целей, которые будут появляться в игре
    private float spawnRate = 1.0f; // Частота появления целей
    public TextMeshProUGUI scoreText; // Текст для отображения счета
    public TextMeshProUGUI gameOverText; // Текст для отображения сообщения о завершении игры
    private int score; // Текущий счет игрока
    public bool isGameActive; // Флаг, указывающий, активна ли игра
    public Button restartButton; // Кнопка для перезапуска игры
    public GameObject titleScreen; // Экран заголовка, отображаемый перед началом игры

    // Метод для обновления счета
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; // Добавляем очки к текущему счету
        scoreText.text = "Score: " + score; // Обновляем текст счета
    }

    // Корутин для появления целей
    IEnumerator SpawnTarget()
    {
        while (isGameActive) // Пока игра активна
        {
            yield return new WaitForSeconds(spawnRate); // Ждем заданное время
            int index = Random.Range(0, targets.Count); // Выбираем случайный индекс из списка целей
            Instantiate(targets[index]); // Создаем цель
        }
    }

    // Метод для завершения игры
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true); // Показываем кнопку перезапуска
        gameOverText.gameObject.SetActive(true); // Показываем текст завершения игры
        isGameActive = false; // Устанавливаем флаг активности игры в false
    }

    // Метод для перезапуска игры
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружаем текущую сцену
    }

    // Метод для начала игры
    public void StartGame(int difficulty)
    {
        spawnRate = spawnRate / difficulty; // Устанавливаем частоту появления целей в зависимости от сложности
        isGameActive = true; // Устанавливаем флаг активности игры в true
        UpdateScore(0); // Сбрасываем счет
        StartCoroutine(SpawnTarget()); // Запускаем корутин для появления целей
        score = 0; // Сбрасываем счет
        scoreText.text = "Score: " + score; // Обновляем текст счета
        titleScreen.gameObject.SetActive(false); // Скрываем экран заголовка
    }
}
