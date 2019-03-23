using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInstancer : MonoBehaviour
{
  public Sprite good, bad, star;

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

  public void InstantiateAt(Vector3 position, bool good, bool star = false)
  {
    GameObject sprite = new GameObject();
    sprite.transform.position = position;
    SpriteRenderer renderer = sprite.AddComponent<SpriteRenderer>();

    if (!star)
    {
      renderer.sprite = (good) ? this.good : bad;
    }
    else
    {
      renderer.sprite = this.star;
    }
      
    sprite.AddComponent<SpriteFunctionality>();
   // transform.localScale = new Vector3(-1, 1, 1);
  }

}
