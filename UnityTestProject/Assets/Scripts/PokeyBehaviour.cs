using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PokeyBehaviour : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)]
    private float _pokeForce = 5f;

    private Rigidbody _body;
    private AudioSource _audio;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                _body.AddForceAtPosition(ray.direction * _pokeForce, hit.point);
                _audio.Play();
            }
        }
    }
}
