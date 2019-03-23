using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFunctionality : MonoBehaviour
{
  float lifetime = 3;
  public float currentLifeTime = 0;

  // Update is called once per frame
  void Update()
  {

    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - currentLifeTime / lifetime);

    if (currentLifeTime >= lifetime)
    {
      Destroy(gameObject);
    }

    transform.Translate(Vector3.up * 2 * Time.deltaTime,Space.World);

    transform.LookAt(Camera.main.transform);

    currentLifeTime += Time.deltaTime;
  }
}
