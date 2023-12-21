using Raylib_CsLo;
using static Raylib_CsLo.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RotMCDmgCalc
{
    public class TextBox
    {
        public Rectangle rect;
        public string text = "";
        string title;
        public bool canHaveInput;
        public TextBox(Rectangle rect, string title = "", bool canHaveInput = true)
        {
            this.rect = rect;
            this.canHaveInput = canHaveInput;
            this.title = title;
        }
        public void Draw()
        {
            DrawRectangleLinesEx(rect, 2, DARKGRAY);
            DrawTextEx(GetFontDefault(), title + "\n" + text, new Vector2(rect.x, rect.y) + new Vector2(1, 1), 20, 1, GRAY);
        }
    }
}
