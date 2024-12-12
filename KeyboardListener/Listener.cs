using KeyboardListener.WinKeys;
using System.Collections;
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

        private Dictionary<Action<bool>, KeyState> handlers = [];
        private Dictionary<Action<HotKey, bool>, HotKey> hotKeysHandlers = [];
        private static Listener listener = new();
        public static Listener GetListener() => listener;

        public async void Start()
        {
            _switch = true;
            if (!started)
                started = true;
                await Task.Run(() =>
                {
                    while (_switch)
                    {
                        CatchKeys();
                        CatchHotKeys();
                    }
                });
        }

        public void Stop() => _switch = started = false;

        public bool AddKeyHook(WinKeys.Keys key, Action<bool> handler)
        {
            if (handler != null)
            {
                handlers.Add(handler, new KeyState(key, false));
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
            foreach (var hotKeyHandler in hotKeysHandlers)
            {
                while (GetAsyncKeyState(((int)hotKeyHandler.Value.Modifier)) < 0)
                {
                    if ((GetAsyncKeyState(((int)hotKeyHandler.Value.Key))) < 0)
                    {
                        hotKeyHandler.Key(hotKeyHandler.Value, true);
                        Thread.Sleep(100);

                        if (GetAsyncKeyState(((int)hotKeyHandler.Value.Key)) == 0)
                            hotKeyHandler.Key(hotKeyHandler.Value, false);
                    }
                }
            }
        }
        private void CatchKeys()
        {
            foreach (var handler in handlers)
            {
                if ((GetAsyncKeyState((int)handler.Value.Key) < 0) && !handler.Value.State)
                {
                    handler.Key(true);
                    handler.Value.Switch();
                }
                else if ((GetAsyncKeyState((int)handler.Value.Key) >= 0) && handler.Value.State)
                {
                    handler.Key(false);
                    handler.Value.Switch();
                }
            }
        }
        private Listener() { }

        private class KeyState(WinKeys.Keys key, bool state)
        {
            public Keys Key = key;
            public bool State = state;

            public void Switch() => State = !State;
        }
    }
}