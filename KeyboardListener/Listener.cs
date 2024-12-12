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

        private bool started = false;

        private Dictionary<Action<bool>, KeyState> handlers = [];
        private Dictionary<Action<bool>, List<KeyState>> hotKeysHandlers = [];
        private static Listener listener = new();
        public static Listener GetListener() => listener;

        public async void Start()
        {
            if (!started)
            {
                started = true;
                await Task.Run(() =>
                {
                    while (started)
                    {
                        CatchKeys();
                        CatchHotKeys();
                    }
                });
            }
        }

        public void Stop() => started = false;

        public bool AddKeyHook(WinKeys.Keys key, Action<bool> handler)
        {
            if (handler != null)
            {
                handlers.Add(handler, new KeyState(key, false));
                return true;
            }

            return false;
        }
        public bool AddHotKeyHook(HotKey hotKey, Action<bool> handler)
        {
            if (handler != null)
            {
                hotKeysHandlers.Add(handler, new List<KeyState>{
                    new KeyState((Keys)hotKey.Modifier,false),
                    new KeyState(hotKey.Key,false)});

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

        public bool RemoveHotKeyHook(Action<bool> handler)
        {
            if (hotKeysHandlers != null)
                return hotKeysHandlers.Remove(handler);

            return false;
        }

        private void CatchHotKeys()
        {
            foreach (var hotKeyHandler in hotKeysHandlers)
            {
                bool modifierWasRelease = !hotKeyHandler.Value[0].State;
                bool modifierIsPressed =
                    GetAsyncKeyState(((int)hotKeyHandler.Value[0].Key)) < 0;

                bool keyWasRelease = !hotKeyHandler.Value[1].State;
                bool keyIsPressed =
                    GetAsyncKeyState(((int)hotKeyHandler.Value[1].Key)) < 0;

                if ((modifierWasRelease && modifierIsPressed)
                    && (keyWasRelease && keyIsPressed))
                {
                    foreach (var key in hotKeyHandler.Value)
                        key.Switch();
                    hotKeyHandler.Key(true);
                }
                else if ((!modifierWasRelease && !modifierIsPressed)
                    && (!keyWasRelease && !keyIsPressed))
                {
                    foreach (var key in hotKeyHandler.Value)
                        key.Switch();
                    hotKeyHandler.Key(false);
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