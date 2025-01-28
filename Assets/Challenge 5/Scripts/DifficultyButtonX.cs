using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonX : MonoBehaviour
{
    private Button button; // Ссылка на компонент Button
    private GameManagerX gameManagerX; // Ссылка на объект GameManagerX
    public int difficulty; // Уровень сложности, связанный с этой кнопкой

    // Метод Start вызывается перед первым кадром
    void Start()
    {
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>(); // Получаем ссылку на GameManagerX
        button = GetComponent<Button>(); // Получаем компонент Button
        button.onClick.AddListener(SetDifficulty); // Добавляем слушатель нажатия на кнопку
    }

    /* Когда кнопка нажата, вызывается метод StartGame()
     * и передается значение сложности (1, 2, 3) от кнопки 
    */
    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked"); // Выводим в консоль имя нажатой кнопки
        gameManagerX.StartGame(difficulty); // Запускаем игру с выбранным уровнем сложности
    }
}
