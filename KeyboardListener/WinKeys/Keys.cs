namespace KeyboardListener.WinKeys
{
    public enum Modifiers
    {
        L_SHIFT = 0xA0,
        R_SHIFT = 0xA1,
        L_CONTROL = 0xA2,
        R_CONTROL = 0xA3,
        L_ALT = 0xA4,
        R_ALT = 0xA5
    }

    public enum Keys : int
    {
        //MOUSE
        L_MOUSE = 0X01,
        R_MOUSE = 0X02,
        M_MOUSE = 0X04,

        //CONTROLS
        BREAK = 0X03,
        BACKSPACE = 0X08,
        TAB = 0X09,
        RETURN = 0X0D,
        PAUSE = 0X13,
        ESC = 0X1B,
        SPACE = 0X20,
        PAGE_UP = 0X21,
        PAGE_DOWN = 0X22,
        END = 0X23,
        HOME = 0X24,
        LEFT = 0X25,
        UP = 0X26,
        RIGHT = 0X27,
        DOWN = 0X28,
        PRINT = 0X2A,
        PRINT_SCREEN = 0X2C,
        INSERT = 0X2D,
        DELETE = 0X2E,
        L_WIN = 0X5B,
        R_WIN = 0X5C,
        L_SHIFT = 0XA0,
        R_SHIFT = 0XA1,
        L_CONTROL =0XA2,
        R_CONTROL =0XA3,
        L_ALT = 0XA4,
        R_ALT = 0XA5,
        NUMLOCK = 0X90,
        SCROLLOCK = 0X91,


        //DIGITS
        ZERO = 0X30,
        ONE = 0X31,
        TWO = 0X32,
        THREE = 0X33,
        FOUR = 0X34,
        FIVE = 0X35,
        SIX = 0X36,
        SEVEN = 0X37,
        EIGHT = 0X38,
        NINE = 0X39,

        //ALPHABET KEYPAD
        A = 0X41,
        B, C, D, E, F, G, H,
        I, J, K, L, M, N, O,
        P, Q, R, S, T, U, V,
        W, X, Y, Z,

        //NUMPAD
        NUM0 = 0X60,
        NUM1, NUM2, NUM3,
        NUM4, NUM5, NUM6,
        NUM7, NUM8, NUM9,

        MULTIPLY, PLUS, SEPARATOR, 
        MINUS, DECIMAL, DIVIDE,

        //F1-F12 BUTTONS
        F1 = 0X70,
        F2,F3, F4, F5, F6, F7, F8, F9,
        F10, F11, F12
    }
}
