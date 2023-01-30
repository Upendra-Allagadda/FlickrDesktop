using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrDesktop.Helper
{
    public class Navigation<T>
    {
        private Stack<T> _previousActions = new Stack<T>();
        private Stack<T> _nextActions = new Stack<T>();

        public void GoBack()
        {
            if (_previousActions.Count > 0)
            {
                _nextActions.Push(CurrentAction);
                CurrentAction = _previousActions.Pop();
            }
        }

        public void GoForward()
        {
            if (_nextActions.Count > 0)
            {
                _previousActions.Push(CurrentAction);
                CurrentAction = _nextActions.Pop();
            }
        }

        public void ExecuteAction(T action)
        {
            _previousActions.Push(CurrentAction);
            _nextActions.Clear();
            CurrentAction = action;
        }

        public T? CurrentAction { get; private set; }
        public bool CanGoBack => _previousActions.Count > 0;
        public bool CanGoForward => _nextActions.Count > 0;
    }
}
