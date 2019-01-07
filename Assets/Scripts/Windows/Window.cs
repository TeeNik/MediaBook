using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIWindow
{
    public enum WindowType
    {
        ImageView,
    }

    public class Window : MonoBehaviour
    {
        protected CompositeDisposable _subscriptions;
        public GameObject WindowPanel;
        public WindowType Type;

        public virtual void Init()
        {
            _subscriptions = new CompositeDisposable();
        }

        public void OpenWindow()
        {
            WindowPanel.SetActive(true);
        }

        public void CloseWindow()
        {
            WindowPanel.SetActive(false);
        }

        void OnDestroy()
        {
            Utils.DisposeAndSetNull(ref _subscriptions);
        }
    }

} 


