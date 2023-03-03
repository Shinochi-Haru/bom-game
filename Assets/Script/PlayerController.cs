using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public UnityEvent onDieCallback = new UnityEvent();
    /// <summary>持っているアイテムのリスト</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    public int life = 100;
    public bool invaded;//追いかけられる領域の判定
    public Slider hpBar;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (hpBar != null)
        {
            hpBar.value = life;
        }
    }
    private void Update()
    {
        if (_itemList.Count > 0)
        {
            // リストの先頭にあるアイテムを使って、破棄する
            ItemBase item = _itemList[0];
            _itemList.RemoveAt(0);
            item.Activate();
            Destroy(item.gameObject);
        }
    }

    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

    public void Damage(int damage)
    {
        if (life <= 0) return;

        life -= damage;
        if (hpBar != null)
        {
            hpBar.value = life;
        }
        if (life <= 0)
        {
            OnDie();
        }
    }
    void OnDie()
    {
        anim.SetBool("Die", true);
        onDieCallback.Invoke();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "CautionArea")
        {
            invaded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "CautionArea")
        {
            invaded = false;
        }
    }
}