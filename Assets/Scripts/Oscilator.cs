using UnityEngine;

public class Oscilator : MonoBehaviour
{
    // config
    [Range(0, 1)]
    [SerializeField] float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    // state
    const float tau = Mathf.PI * 2;
    Vector3 startingPos;

    // Start is called before the first frame update
    void Awake()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscilate();
    }

    private void Oscilate()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // cycles keep growing over time
        float rawSinWave = Mathf.Sin(cycles * tau); // based on cycles it will return value between -1 and 1
        movementFactor = (rawSinWave + 1f) / 2f;    // recalculate the factor

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
