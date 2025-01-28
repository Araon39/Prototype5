using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button; // Ссылка на компонент Button
    private GameManager gameManager; // Ссылка на объект GameManager
    public int difficulty; // Уровень сложности, связанный с этой кнопкой

    // Метод Start вызывается перед первым кадром
    void Start()
    {
        button = GetComponent<Button>(); // Получаем компонент Button
        button.onClick.AddListener(SetDifficulty); // Добавляем слушатель нажатия на кнопку
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Получаем ссылку на GameManager
    }

    // Метод Update вызывается один раз за кадр
    void Update()
    {
        // Пустой метод, можно удалить или использовать для будущих обновлений
    }

    // Метод для установки уровня сложности
    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked"); // Выводим в консоль имя нажатой кнопки
        gameManager.StartGame(difficulty); // Запускаем игру с выбранным уровнем сложности
    }
}
