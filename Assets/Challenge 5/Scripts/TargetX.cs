using System.Collections;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private Rigidbody rb; // Ссылка на компонент Rigidbody для управления физикой объекта
    private GameManagerX gameManagerX; // Ссылка на объект GameManagerX для управления игрой
    public int pointValue; // Количество очков, которое дает уничтожение этого объекта
    public GameObject explosionFx; // Ссылка на объект эффекта взрыва

    public float timeOnScreen = 1.0f; // Время, в течение которого объект будет на экране

    private float minValueX = -3.75f; // X-координата центра самой левой клетки
    private float minValueY = -3.75f; // Y-координата центра самой нижней клетки
    private float spaceBetweenSquares = 2.5f; // Расстояние между центрами клеток на игровом поле


    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>(); // Получаем ссылку на GameManagerX

        transform.position = RandomSpawnPosition(); // Устанавливаем случайную позицию появления объекта
        StartCoroutine(RemoveObjectRoutine()); // Запускаем таймер перед тем, как объект покинет экран
    }

    // Метод вызывается при нажатии на объект мышью
    private void OnMouseDown()
    {
        if (gameManagerX.isGameActive) // Проверяем, активна ли игра
        {
            Destroy(gameObject); // Уничтожаем объект
            gameManagerX.UpdateScore(pointValue); // Обновляем счет в GameManagerX
            Explode(); // Создаем эффект взрыва
        }
    }

    // Метод для генерации случайной позиции появления объекта
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

    // Метод вызывается при столкновении с другим объектом
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // Уничтожаем объект

        if (other.gameObject.CompareTag("Sensor") && !gameObject.CompareTag("Bad")) // Если объект столкнулся с сенсором и не имеет тег "Bad"
        {
            gameManagerX.GameOver(); // Завершаем игру
        }
    }

    // Метод для отображения эффекта взрыва в позиции объекта
    void Explode()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation); // Создаем эффект взрыва
    }

    // Корутин для удаления объекта после задержки
    IEnumerator RemoveObjectRoutine()
    {
        yield return new WaitForSeconds(timeOnScreen); // Ждем заданное время
        if (gameManagerX.isGameActive) // Проверяем, активна ли игра
        {
            transform.Translate(Vector3.forward * 5, Space.World); // Перемещаем объект за фон, чтобы он столкнулся с сенсором
        }
    }
}
