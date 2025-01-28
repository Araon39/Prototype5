using UnityEngine;

public class DestroyObjectX : MonoBehaviour
{
    // Метод Start вызывается перед первым кадром
    void Start()
    {
        Destroy(gameObject, 2); // Уничтожаем объект через 2 секунды
    }
}
