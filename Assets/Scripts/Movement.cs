using UnityEngine;

public class Movement : MonoBehaviour
{
    // config
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    // cache
    Rigidbody _rigidbody;
    AudioSource _audioSource;

    void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            
            if(!_audioSource.isPlaying) 
            {
                _audioSource.Play();
            }
        }
        else if(_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }

    }

    private void ApplyRotation(float rotationValue)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }
}
