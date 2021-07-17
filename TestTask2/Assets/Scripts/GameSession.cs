using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject _redCube, _blueCube, _panel;
    [SerializeField] TextMesh _redScore, _blueScore;

    int _count, _redCount, _blueCount;

    void Start()
    {
        _count = (int)(Random.Range(3, 7));

        GameObject obj;
        for (int i = 0; i < _count; i++) // Создаем случайное количество кубов
        {
            if ((int)(Random.Range(0, 2)) == 1)
                obj = Instantiate(_redCube);
            else
                obj = Instantiate(_blueCube);

            obj.transform.position = new Vector3(Random.Range(-4, 5), Random.Range(-1, 2), Random.Range(-3, 2));
        }
    }

    /// <summary>
    /// Вызываестя при уничтожении куба о стенку. Увеличивает счетчики.
    /// </summary>
    /// <param name="isRed">Проверка на цвет куба, если он красный == true.</param>
    public void CubeDestroyed(bool isRed)
    {
        if (isRed)
        {
            _redCount++;
            _redScore.text = _redCount.ToString();
        }
        else
        {
            _blueCount++;
            _blueScore.text = _blueCount.ToString();
        }

        _count--;

        if(_count == 0)
        {
            _panel.SetActive(true);
        }
    }
}
