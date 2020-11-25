using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public interface IAction
    {
        //interface is a contract -- must implement listed methods
        void Cancel();
    }
}
