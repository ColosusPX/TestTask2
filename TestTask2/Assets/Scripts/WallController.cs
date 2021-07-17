using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] GameObject _cube;
    [SerializeField] bool _isRed;
    [SerializeField] GameSession _gameSession;
    [SerializeField] CubeMovment _cubeMovment;
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
            _cubeMovment.IsHit();
            collision.gameObject.GetComponent<Rigidbody>().AddForce((transform.up + transform.right) * _force, ForceMode.Impulse);
        }
    }
}
