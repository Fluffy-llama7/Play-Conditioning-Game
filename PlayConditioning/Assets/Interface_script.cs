using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMechanic
{
    GameObject Player { get; }
    void Damage(float damage);
    void OnLeftClick();
    void OnRightClick();
    void Update();
}
