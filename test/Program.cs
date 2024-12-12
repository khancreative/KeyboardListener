using KeyboardListener;
using KeyboardListener.WinKeys;
namespace test
{
    internal class Program
    {
        private class KeyState(int keyCode, bool state)
        {
            public int Key = keyCode;
            public bool State = state;

            public void Switch() => State = !State;
        }
        static void Main(string[] args)
        {
            var listener = Listener.GetListener();
            listener.AddKeyHook(Keys.F4, HandleKey);
            listener.Start();
            for (int h = 0, m = 0, s = 0; ; s++)
            {
                if (s == 60)
                {
                    ++m;
                    s = 0;

                    if (m == 60)
                    {
                        ++h;
                        m = 0;
                    }
                }
                Console.Write($"\r{h:00}:{m:00}:{s:00}\t");
                Thread.Sleep(1000);
            }
        }

        static void HandleKey(bool pressed)
        {
            if (pressed)
                Console.WriteLine("pressed");
            else
                Console.WriteLine("released");
        }
    }
}
