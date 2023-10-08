using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuApplyForce : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Vector3 startRotation;

    public float force;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = new Vector3(0, 0, Random.Range(0, 361));

        rb = GetComponent<Rigidbody2D>();

        transform.Rotate(startRotation);

        rb.AddForce(transform.right * force);
    }
}
