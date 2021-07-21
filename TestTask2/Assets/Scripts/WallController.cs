using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] bool _isRed;
    [SerializeField] GameSession _gameSession;
    [SerializeField] MoveObjects _moveObjects;
    [Range(10, 50)] [SerializeField] float _force = 30;

    private void OnCollisionEnter(Collision collision)
    {
        if ((_isRed && collision.gameObject.CompareTag("RedCube")) || (!_isRed && collision.gameObject.CompareTag("BlueCube")))
        {
            Destroy(collision.gameObject);
            _gameSession.CubeDestroyed(_isRed);
        }
        else
        {
            _moveObjects.IsHit(collision.gameObject.GetComponent<Rigidbody>());
            collision.gameObject.GetComponent<Rigidbody>().AddForce((transform.up + transform.right) * _force, ForceMode.Impulse);
        }
    }
}
