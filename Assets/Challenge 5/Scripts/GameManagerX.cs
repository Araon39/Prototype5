using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Текст для отображения счета
    public TextMeshProUGUI gameOverText; // Текст для отображения сообщения о завершении игры
    public GameObject titleScreen; // Экран заголовка, отображаемый перед началом игры
    public Button restartButton; // Кнопка для перезапуска игры

    public List<GameObject> targetPrefabs; // Список префабов целей

    private int score; // Текущий счет игрока
    private float spawnRate = 1.5f; // Частота появления целей
    public bool isGameActive; // Флаг, указывающий, активна ли игра

    private float spaceBetweenSquares = 2.5f; // Расстояние между центрами клеток на игровом поле
    private float minValueX = -3.75f; // X-координата центра самой левой клетки
    private float minValueY = -3.75f; // Y-координата центра самой нижней клетки

    // Метод для начала игры, скрытия экрана заголовка, сброса счета и настройки частоты появления целей в зависимости от выбранной сложности
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty; // Устанавливаем частоту появления целей в зависимости от сложности
        isGameActive = true; // Устанавливаем флаг активности игры в true
        StartCoroutine(SpawnTarget()); // Запускаем корутин для появления целей
        score = 0; // Сбрасываем счет
        UpdateScore(0); // Обновляем текст счета
        titleScreen.SetActive(false); // Скрываем экран заголовка
    }

    // Корутин для появления целей, пока игра активна
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate); // Ждем заданное время
            int index = Random.Range(0, targetPrefabs.Count); // Выбираем случайный индекс из списка префабов целей

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation); // Создаем цель в случайной позиции
            }
        }
    }

    // Метод для генерации случайной позиции появления объекта на основе случайного индекса от 0 до 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares); // Вычисляем X-координату
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares); // Вычисляем Y-координату

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0); // Создаем вектор позиции
        return spawnPosition;
    }

    // Метод для генерации случайного индекса клетки от 0 до 3
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Метод для обновления счета
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; // Добавляем очки к текущему счету
        scoreText.text = "Score: " + score; // Обновляем текст счета
    }

    // Метод для завершения игры
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true); // Показываем текст завершения игры
        restartButton.gameObject.SetActive(true); // Показываем кнопку перезапуска
        isGameActive = false; // Устанавливаем флаг активности игры в false
    }

    // Метод для перезапуска игры
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружаем текущую сцену
    }
}
