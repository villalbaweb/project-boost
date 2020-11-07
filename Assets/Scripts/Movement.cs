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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngineSfx);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
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

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightThrustParticles.isPlaying) rightThrustParticles.Play();
        if (leftThrustParticles.isPlaying) leftThrustParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftThrustParticles.isPlaying) leftThrustParticles.Play();
        if (rightThrustParticles.isPlaying) rightThrustParticles.Stop();
    }

    private void StopRotation()
    {
        if (rightThrustParticles.isPlaying) rightThrustParticles.Stop();
        if (leftThrustParticles.isPlaying) leftThrustParticles.Stop();
    }

    private void ApplyRotation(float rotationValue)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }
}
