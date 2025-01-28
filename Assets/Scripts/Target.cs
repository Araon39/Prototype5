using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager; // Ссылка на объект GameManager для управления игрой
    private Rigidbody targetRb; // Ссылка на компонент Rigidbody для управления физикой объекта
    private float minSpeed = 12; // Минимальная скорость, с которой объект будет двигаться вверх
    private float maxSpeed = 16; // Максимальная скорость, с которой объект будет двигаться вверх
    private float maxTorque = 10; // Максимальный крутящий момент, который будет применен к объекту
    private float ySpawnPos = -2; // Y-координата позиции появления объекта
    private float xSpawnPos = 4; // Максимальная X-координата позиции появления объекта

    public ParticleSystem explosionParticle; // Ссылка на систему частиц для эффекта взрыва
    public int pointValue; // Количество очков, которое дает уничтожение этого объекта

    // Метод Start вызывается перед первым кадром
    void Start()
    {
        // Получаем ссылку на GameManager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Получаем компонент Rigidbody и применяем к нему случайную силу и крутящий момент
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        // Устанавливаем случайную позицию появления объекта
        transform.position = RandomSpawnPos();
    }

    // Метод Update вызывается один раз за кадр
    void Update()
    {
        // Пустой метод, можно удалить или использовать для будущих обновлений
    }

    // Метод вызывается при нажатии на объект мышью
    private void OnMouseDown()
    {
        if (gameManager.isGameActive) // Проверяем, активна ли игра
        {
            Destroy(gameObject); // Уничтожаем объект
            gameManager.UpdateScore(pointValue); // Обновляем счет в GameManager
            Instantiate(explosionParticle, transform.position, transform.rotation); // Создаем эффект взрыва
        }
    }

    // Метод вызывается при столкновении с другим объектом
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // Уничтожаем объект
        if (!gameObject.CompareTag("Bad")) // Если объект не имеет тег "Bad"
        {
            gameManager.GameOver(); // Завершаем игру
        }
    }

    // Метод для генерации случайной силы
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Метод для генерации случайного крутящего момента
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Метод для генерации случайной позиции появления объекта
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnPos, xSpawnPos), ySpawnPos, 0);
    }
}
