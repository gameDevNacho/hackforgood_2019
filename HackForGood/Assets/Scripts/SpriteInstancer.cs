using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInstancer : MonoBehaviour
{
  public Sprite good, bad;

  public static SpriteInstancer Instance
  {
    get;
    private set;
  }

  // Start is called before the first frame update
  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    //InvokeRepeating("Trial", 0, 2);
  }

  void Trial()
  {
    InstantiateAt(transform.position, false);
  }

  public void Update()
  {
   
  }

  public void InstantiateAt(Vector3 position, bool good)
  {
    GameObject sprite = new GameObject();
    sprite.transform.position = position;
    SpriteRenderer renderer = sprite.AddComponent<SpriteRenderer>();
    renderer.sprite = (good) ? this.good : bad;
    sprite.AddComponent<SpriteFunctionality>();
  }

}
