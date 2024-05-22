using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IEnemy
    {
        float Damage { get; } //damage of enemy
        void Update(); //action when enemy is updated
        void Attack(); //action when attacking
    }
}