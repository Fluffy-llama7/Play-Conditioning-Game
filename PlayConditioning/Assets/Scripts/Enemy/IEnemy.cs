using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IEnemy
    {
        void TakeDamage(); //action when enemy is hit
        void Update(); //function to call on every frame
        void Attack(); //action when attacking
    }
}