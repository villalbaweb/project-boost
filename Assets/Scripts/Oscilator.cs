using UnityEngine;

public class Oscilator : MonoBehaviour
{
    // config
    [Range(0, 1)]
    [SerializeField] float movementFactor;
    [SerializeField] Vector3 movementVector;

    // state
    Vector3 startingPos;

    // Start is called before the first frame update
    void Awake()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
