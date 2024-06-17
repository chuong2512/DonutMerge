using System;
using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int id;
    public int score;
    public GameObject nextLevelPrefab;
    public Action<Fruit, Fruit> OnLevelUp;
    public Action OnGameOver;
    private Rigidbody2D rigid;
    private bool isTouchRedline;
    private float timer;

    public ParticleSystem _fx;

    void Update()
    {
        if (isTouchRedline == false)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > 1)
        {
            Debug.Log("Game Over");
            OnGameOver?.Invoke();
        }
    }

    public void SetSimulated(bool b)
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        rigid.simulated = b;
    }

    public void PlayFx()
    {
        _fx.Simulate(0.0f, true, true);
        _fx.Play();
    }

    public void InitFx(float scale = 2f)
    {
        var newFx = Instantiate(_fx, null);
        newFx.transform.position = _fx.transform.position;
        newFx.transform.localScale = transform.localScale * scale;
        newFx.Play();
        DOVirtual.DelayedCall(newFx.main.duration + 0.1f, () =>
        {
            try
            {
                Destroy(newFx.gameObject);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        var fruit = obj.GetComponent<Fruit>();
        if (obj.CompareTag("Fruit"))
        {
            if (obj.name == gameObject.name)
            {
                if (nextLevelPrefab != null)
                {
                    OnLevelUp?.Invoke(this, fruit);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Redline"))
        {
            isTouchRedline = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Redline"))
        {
            isTouchRedline = false;
            timer = 0;
        }
    }

    public void NextObj()
    {
        if (nextLevelPrefab != null)
        {
            OnLevelUp?.Invoke(this, this);
        }
        else
        {
            InitFx(1.5f);
            Destroy(gameObject);
        }
    }
}