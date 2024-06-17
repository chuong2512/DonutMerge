using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isHammer, isX2;
    Vector3 pos;
    public float speed = 1;
    public ParticleSystem _fx;
    private GameObject _fruitObj;

    void Update()
    {
        pos = Input.mousePosition;
        pos.z = speed;
        transform.position = Camera.main.ScreenToWorldPoint(pos);

        if (Input.GetMouseButtonUp(0) && isHammer && _fruitObj != null)
        {
            FindObjectOfType<Game>().isHammer = false;
            FindObjectOfType<Game>().RemoveHammer();
            var FxObj = Instantiate(_fx);
            FxObj.transform.position = this.transform.position;
            FxObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            _fruitObj.SetActive(false);
            AudioManager.Instance.PlaySFX("boom");
            Destroy(this.gameObject);
        }

        if (Input.GetMouseButtonUp(0) && isX2 && _fruitObj != null)
        {
            FindObjectOfType<Game>().isX2 = false;
            FindObjectOfType<Game>().RemoveX2();
            var FxObj = Instantiate(_fx);
            FxObj.transform.position = this.transform.position;
            FxObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _fruitObj.GetComponent<Fruit>().NextObj();
            AudioManager.Instance.PlaySFX("levelup");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Fruit")
        {
            _fruitObj = collision.gameObject;
        }
    }
}