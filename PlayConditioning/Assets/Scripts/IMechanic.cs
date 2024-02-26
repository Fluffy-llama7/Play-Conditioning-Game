using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mech
{
    public interface IMechanic
    {
        void Damage(GameObject enemy);
        void OnLeftClick(GameObject player, string mechanic);
        void OnRightClick(GameObject player, string mechanic);

    }
}

