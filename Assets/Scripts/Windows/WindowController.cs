using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIWindow
{
    public class WindowController : MonoBehaviour
    {
        [SerializeField] private List<Window> _windows;

        public void Init()
        {
            _windows.ForEach(w => w.Init());
        }
    }

}


