using UnityEngine;

public class Movement : MonoBehaviour
{
    // config
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngineSfx = null;

    [SerializeField] ParticleSystem mainEngineParticles = null;
    [SerializeField] ParticleSystem leftThrustParticles = null;
    [SerializeField] ParticleSystem rightThrustParticles = null;

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
                _audioSource.PlayOneShot(mainEngineSfx);
            }

            if(!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }
        else 
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            if (mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Stop();
            }
        }

    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);

            if(!rightThrustParticles.isPlaying)  rightThrustParticles.Play();
            if(leftThrustParticles.isPlaying)   leftThrustParticles.Stop();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            
            if(!leftThrustParticles.isPlaying) leftThrustParticles.Play();
            if(rightThrustParticles.isPlaying) rightThrustParticles.Stop();
        }
        else
        {
            if (rightThrustParticles.isPlaying) rightThrustParticles.Stop();
            if (leftThrustParticles.isPlaying) leftThrustParticles.Stop();
        }

    }

    private void ApplyRotation(float rotationValue)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }
}
