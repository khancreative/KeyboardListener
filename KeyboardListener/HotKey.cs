using KeyboardListener.WinKeys;

namespace KeyboardListener
{


    public class HotKey
    {
        public WinKeys.Modifiers Modifier { get; private set; }
        public WinKeys.Keys Key { get; private set; }

        public bool SetHotKey(WinKeys.Modifiers modifier, WinKeys.Keys key)
        {
            if (((int)key) < ((int)Modifiers.L_SHIFT) || ((int)key) > ((int)Modifiers.R_ALT))
            {
                Modifier = modifier;
                Key = key;
                return true;
            }

            return false;
        }

        public override string ToString() => 
            $"{Enum.GetName(typeof(WinKeys.Modifiers), ((int)Modifier))} + " +
            $"{Enum.GetName(typeof(WinKeys.Keys), ((int)Key))}";
    }
}
