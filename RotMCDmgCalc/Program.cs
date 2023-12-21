using static Raylib_CsLo.Raylib;
using Raylib_CsLo;
using RotMCDmgCalc;

public static class Program
{
    public static List<Button> buttons = new List<Button>
        {
            new Button("Calculate", new Rectangle(400, 200, 250, 50), GRAY, YELLOW, DARKGRAY)
        };
    static TextBox mainDmgBox = new TextBox(new Rectangle(0, 0, 170, 50), "mainAtkDmg");
    static TextBox mainDmgCooldownBox = new TextBox(new Rectangle(180, 0, 170, 50), "mainAtkCooldown");
    static TextBox spellDmgBox = new TextBox(new Rectangle(360, 0, 170, 50), "spellDmg");
    static TextBox spellDmgCooldownBox = new TextBox(new Rectangle(540, 0, 170, 50), "spellCooldown");
    static TextBox attackStatBox = new TextBox(new Rectangle(720, 0, 170, 50), "attackStatDecimal");
    static TextBox ccBox = new TextBox(new Rectangle(900, 0, 170, 50), "critChance");
    static TextBox enemyHpBox = new TextBox(new Rectangle(1080, 0, 170, 50), "enemyHealth");
    static TextBox mainDpsBox = new TextBox(new Rectangle(0, 400, 170, 50), "mainDps", false);
    static TextBox spellDpsBox = new TextBox(new Rectangle(180, 400, 170, 50), "spellDps", false);
    static TextBox bothDpsBox = new TextBox(new Rectangle(360, 400, 170, 50), "bothDps", false);
    static TextBox timeToKillBox = new TextBox(new Rectangle(540, 400, 170, 50), "timeToKill", false);
    public static List<TextBox> textBoxes = new List<TextBox>
        {
            mainDmgBox,
            mainDmgCooldownBox,
            spellDmgBox,
            spellDmgCooldownBox,
            attackStatBox,
            ccBox,
            enemyHpBox,
            mainDpsBox,
            spellDpsBox,
            timeToKillBox,
            bothDpsBox
        };
    static void Main()
    {
        TextBox? currentTextBox = null;
        InitWindow(1280, 720, "RotMC Dmg Calculator");
        while(!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(WHITE);
            foreach(Button button in buttons)
            {
                button.Draw();
            }
            foreach (TextBox button in textBoxes)
            {
                button.Draw();
            }
            if(IsMouseButtonDown(0))
            {
                bool selected = false;
                foreach (var item in textBoxes)
                {
                    if(CheckCollisionPointRec(GetMousePosition(), item.rect) && item.canHaveInput)
                    {
                        currentTextBox = item;
                        selected = true;
                    }
                }
                if(!selected)
                {
                    currentTextBox = null;
                }
            }
            KeyboardKey pressed = GetKeyPressed_();
            string num = pressed switch
            {
                KeyboardKey.KEY_ONE => "1",
                KeyboardKey.KEY_ZERO => "0",
                KeyboardKey.KEY_TWO => "2",
                KeyboardKey.KEY_THREE => "3",
                KeyboardKey.KEY_FOUR => "4",
                KeyboardKey.KEY_FIVE => "5",
                KeyboardKey.KEY_SIX => "6",
                KeyboardKey.KEY_SEVEN => "7",
                KeyboardKey.KEY_EIGHT => "8",
                KeyboardKey.KEY_NINE => "9",
                KeyboardKey.KEY_PERIOD => ".",
                _ => ""
            };
            if (currentTextBox != null)
                currentTextBox.text += num;
            EndDrawing();
            if(ccBox.text == "" ||
                mainDmgBox.text == "" ||
                spellDmgBox.text == "" ||
                mainDmgCooldownBox.text == ""||
                spellDmgCooldownBox.text == ""||
                attackStatBox.text == ""||
                enemyHpBox.text == "")
            {
                continue;
            }
            if (buttons[0].pressed)
            {
                var mainDps = Weapon(float.Parse(ccBox.text), float.Parse(attackStatBox.text), float.Parse(mainDmgBox.text), float.Parse(mainDmgCooldownBox.text));
                var abilityDps = Weapon(float.Parse(ccBox.text), float.Parse(attackStatBox.text), float.Parse(spellDmgBox.text), float.Parse(spellDmgCooldownBox.text));
                var bothDps = mainDps + abilityDps;
                mainDpsBox.text = mainDps.ToString();
                spellDpsBox.text = abilityDps.ToString();
                bothDpsBox.text = bothDps.ToString();
                timeToKillBox.text = (float.Parse(enemyHpBox.text) / bothDps).ToString();
            }
        }
        CloseWindow();
    }
    static float Weapon(float cc, float atkMulti, float dmg, float spd)
    {
        float cdpa = dmg * cc * 2;
        float ndpa = dmg * (1 - cc);
        float fdpa = (cdpa + ndpa) * (atkMulti + 1);
        return (float)(fdpa / spd);
    }
}