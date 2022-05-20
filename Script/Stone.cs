using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int hits = 1;
    public int points = 100;
    public Vector3 rotator;
    public Material stonehit;

    Material _orgMaterial;
    Renderer _renderer;

    void Start()
    {
        transform.Rotate(rotator * (transform.position.x + transform.position.y)*0.1f);
        _renderer = GetComponent<Renderer>();
        _orgMaterial = _renderer.sharedMaterial;

    }


    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        if (hits == 0)
        {
           GameManager.instance.Score += points;
           Destroy(gameObject);
        }
        _renderer.sharedMaterial = stonehit;
        Invoke("RestoreMaterial", 0.05f);
    }
    void RestoreMaterial()
    {
        _renderer.sharedMaterial = _orgMaterial;

    }
}
