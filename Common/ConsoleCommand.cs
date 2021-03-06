﻿using UnityEngine;
#pragma warning disable CS1591

namespace Common
{
    public abstract class ConsoleCommand : MonoBehaviour
    {        
        public abstract bool AvailableInStartScreen { get; }
        public abstract bool AvailableInGame { get; }        

        public abstract void RegisterCommand();        
    }
}
