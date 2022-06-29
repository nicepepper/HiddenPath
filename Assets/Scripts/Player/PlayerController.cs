using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _gravityForce = 10f;
    private float _currentAttractionCharacter = 0.0f;
    private CharacterController _character;
    
    public void MovePlayer(Vector3 moveDirection)
    {
        moveDirection = moveDirection * _moveSpeed;
        moveDirection.y = _currentAttractionCharacter;
        _character.Move(moveDirection * Time.deltaTime);
    }

    public void RotatePlayer(Vector3 moveDirection)
    {
        if (_character.isGrounded)
        {
            if (Vector3.Angle(transform.forward, moveDirection) > 0)
            {
                var newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void Awake()
    {
        _character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GravityHandling();
    }

    private void GravityHandling()
    {
        if (!_character.isGrounded)
        {
            _currentAttractionCharacter -= _gravityForce * Time.deltaTime;
        }
        else
        {
            _currentAttractionCharacter = 0.0f;
        }
    }
}
