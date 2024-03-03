using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mech
{
    public interface IMechanic
    {
        void Execute(); //execute function of mechanic
        void Update(); //function to call on every frame
    }
}