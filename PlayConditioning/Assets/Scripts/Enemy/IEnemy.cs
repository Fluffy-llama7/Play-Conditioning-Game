using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public interface IEnemy
    {
        void Update(); //action when enemy is updated
        void Attack(); //action when attacking
    }
}