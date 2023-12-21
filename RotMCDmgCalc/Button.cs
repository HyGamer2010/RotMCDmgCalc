using Raylib_CsLo;
using static Raylib_CsLo.Raylib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RotMCDmgCalc
{
    public class Button
    {
        string text;
        Rectangle rect;
        Color color;
        Color textColor;
        Color pressColor;
        Color currentColor;
        public bool pressed;
        public Button(string text, Rectangle rect, Color color, Color textColor, Color pressColor) 
        {
            this.text = text;
            this.rect = rect;
            this.color = color;
            this.textColor = textColor;
            this.pressColor = pressColor;
        }
        public void Draw()
        {
            DrawRectanglePro(rect, Vector2.Zero, 0, currentColor);
            DrawTextPro(GetFontDefault(), text, new Vector2(rect.X + 1, rect.Y), Vector2.Zero, 0, 30, 1, textColor);
            var mousePos = GetMousePosition();
            if (IsMouseButtonDown(0) && CheckCollisionPointRec(mousePos, rect))
            {
                currentColor = pressColor;
                pressed = true;
            } else
            {
                currentColor = color;
                pressed = false;
            }
        }
    }
}
