using System.Runtime.InteropServices;

namespace KeyboardListener
{
    public class Listener
    {
        #region WIN32 API NATIVE METHODS
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int keyCode);
        #endregion

        private bool _switch = true;
        private bool started = false;

        private Dictionary<Action<bool>, int> handlers = [];
        private Dictionary<Action<HotKey, bool>, HotKey> hotKeysHandlers = [];
        private static Listener listener = new();
        public static Listener GetListener() => listener;

        public async void Start()
        {
            _switch = started = true;
            await Task.Run(() =>
            {
                while (_switch)
                {
                    CatchKeys();
                    CatchHotKeys();
                }
            }
        );
        }

        public void Stop() => _switch = started = false;

        public bool AddKeyHook(int keyCode, Action<bool> handler)
        {
            if (handler != null)
            {
                handlers.Add(handler, keyCode);
                return true;
            }

            return false;
        }
        public bool AddHotKeyHook(HotKey hotKey, Action<HotKey, bool> handler)
        {
            if (handler != null)
            {
                hotKeysHandlers.Add(handler, hotKey);
                return true;
            }

            return false;
        }

        public bool RemoveKeyHook(Action<bool> handler)
        {
            if (handler != null)
                return handlers.Remove(handler);

            return false;
        }

        public bool RemoveHotKeyHook(Action<HotKey, bool> handler)
        {
            if (hotKeysHandlers != null)
                return hotKeysHandlers.Remove(handler);

            return false;
        }

        private void CatchHotKeys()
        {
            throw new NotImplementedException();
        }
        private void CatchKeys()
        {
            foreach (var handle in handlers)
            {
                if (GetAsyncKeyState(handle.Value) < 0)
                {
                    Thread.Sleep(100);
                    if (GetAsyncKeyState(handle.Value) == 0)
                        handle.Key(false);
                    else handle.Key(true);
                }
            }
        }
        private Listener() { }
    }
}
