﻿using Microsoft.Xna.Framework.Input;

namespace KlearUI.MonoGame
{
    public static class Input
    {
        /// <summary>
        /// Converts a key to its char representation
        /// That code had been borrowed from the MonoGame.Extended project
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isShiftDown"></param>
        /// <returns></returns>
        public static char? ToChar(this Keys key, bool isShiftDown)
        {
            switch (key)
            {
                case Keys.A:
                    return isShiftDown ? 'A' : 'a';
                case Keys.B:
                    return isShiftDown ? 'B' : 'b';
                case Keys.C:
                    return isShiftDown ? 'C' : 'c';
                case Keys.D:
                    return isShiftDown ? 'D' : 'd';
                case Keys.E:
                    return isShiftDown ? 'E' : 'e';
                case Keys.F:
                    return isShiftDown ? 'F' : 'f';
                case Keys.G:
                    return isShiftDown ? 'G' : 'g';
                case Keys.H:
                    return isShiftDown ? 'H' : 'h';
                case Keys.I:
                    return isShiftDown ? 'I' : 'i';
                case Keys.J:
                    return isShiftDown ? 'J' : 'j';
                case Keys.K:
                    return isShiftDown ? 'K' : 'k';
                case Keys.L:
                    return isShiftDown ? 'L' : 'l';
                case Keys.M:
                    return isShiftDown ? 'M' : 'm';
                case Keys.N:
                    return isShiftDown ? 'N' : 'n';
                case Keys.O:
                    return isShiftDown ? 'O' : 'o';
                case Keys.P:
                    return isShiftDown ? 'P' : 'p';
                case Keys.Q:
                    return isShiftDown ? 'Q' : 'q';
                case Keys.R:
                    return isShiftDown ? 'R' : 'r';
                case Keys.S:
                    return isShiftDown ? 'S' : 's';
                case Keys.T:
                    return isShiftDown ? 'T' : 't';
                case Keys.U:
                    return isShiftDown ? 'U' : 'u';
                case Keys.V:
                    return isShiftDown ? 'V' : 'v';
                case Keys.W:
                    return isShiftDown ? 'W' : 'w';
                case Keys.X:
                    return isShiftDown ? 'X' : 'x';
                case Keys.Y:
                    return isShiftDown ? 'Y' : 'y';
                case Keys.Z:
                    return isShiftDown ? 'Z' : 'z';
            }

            if (key == Keys.D0 && !isShiftDown || key == Keys.NumPad0)
            {
                return '0';
            }
            if (key == Keys.D1 && !isShiftDown || key == Keys.NumPad1)
            {
                return '1';
            }
            if (key == Keys.D2 && !isShiftDown || key == Keys.NumPad2)
            {
                return '2';
            }
            if (key == Keys.D3 && !isShiftDown || key == Keys.NumPad3)
            {
                return '3';
            }
            if (key == Keys.D4 && !isShiftDown || key == Keys.NumPad4)
            {
                return '4';
            }
            if (key == Keys.D5 && !isShiftDown || key == Keys.NumPad5)
            {
                return '5';
            }
            if (key == Keys.D6 && !isShiftDown || key == Keys.NumPad6)
            {
                return '6';
            }
            if (key == Keys.D7 && !isShiftDown || key == Keys.NumPad7)
            {
                return '7';
            }
            if (key == Keys.D8 && !isShiftDown || key == Keys.NumPad8)
            {
                return '8';
            }
            if (key == Keys.D9 && !isShiftDown || key == Keys.NumPad9)
            {
                return '9';
            }

            if (key == Keys.D0 && isShiftDown)
            {
                return ')';
            }
            if (key == Keys.D1 && isShiftDown)
            {
                return '!';
            }
            if (key == Keys.D2 && isShiftDown)
            {
                return '@';
            }
            if (key == Keys.D3 && isShiftDown)
            {
                return '#';
            }
            if (key == Keys.D4 && isShiftDown)
            {
                return '$';
            }
            if (key == Keys.D5 && isShiftDown)
            {
                return '%';
            }
            if (key == Keys.D6 && isShiftDown)
            {
                return '^';
            }
            if (key == Keys.D7 && isShiftDown)
            {
                return '&';
            }
            if (key == Keys.D8 && isShiftDown)
            {
                return '*';
            }
            if (key == Keys.D9 && isShiftDown)
            {
                return '(';
            }

            if (key == Keys.Space)
            {
                return ' ';
            }
            if (key == Keys.Tab)
            {
                return '\t';
            }
            if (key == Keys.Enter)
            {
                return (char)13;
            }
            if (key == Keys.Back)
            {
                return (char)8;
            }

            if (key == Keys.Add)
            {
                return '+';
            }
            if (key == Keys.Decimal)
            {
                return '.';
            }
            if (key == Keys.Divide)
            {
                return '/';
            }
            if (key == Keys.Multiply)
            {
                return '*';
            }
            if (key == Keys.OemBackslash)
            {
                return '\\';
            }
            if (key == Keys.OemComma && !isShiftDown)
            {
                return ',';
            }
            if (key == Keys.OemComma && isShiftDown)
            {
                return '<';
            }
            if (key == Keys.OemOpenBrackets && !isShiftDown)
            {
                return '[';
            }
            if (key == Keys.OemOpenBrackets && isShiftDown)
            {
                return '{';
            }
            if (key == Keys.OemCloseBrackets && !isShiftDown)
            {
                return ']';
            }
            if (key == Keys.OemCloseBrackets && isShiftDown)
            {
                return '}';
            }
            if (key == Keys.OemPeriod && !isShiftDown)
            {
                return '.';
            }
            if (key == Keys.OemPeriod && isShiftDown)
            {
                return '>';
            }
            if (key == Keys.OemPipe && !isShiftDown)
            {
                return '\\';
            }
            if (key == Keys.OemPipe && isShiftDown)
            {
                return '|';
            }
            if (key == Keys.OemPlus && !isShiftDown)
            {
                return '=';
            }
            if (key == Keys.OemPlus && isShiftDown)
            {
                return '+';
            }
            if (key == Keys.OemMinus && !isShiftDown)
            {
                return '-';
            }
            if (key == Keys.OemMinus && isShiftDown)
            {
                return '_';
            }
            if (key == Keys.OemQuestion && !isShiftDown)
            {
                return '/';
            }
            if (key == Keys.OemQuestion && isShiftDown)
            {
                return '?';
            }
            if (key == Keys.OemQuotes && !isShiftDown)
            {
                return '\'';
            }
            if (key == Keys.OemQuotes && isShiftDown)
            {
                return '"';
            }
            if (key == Keys.OemSemicolon && !isShiftDown)
            {
                return ';';
            }
            if (key == Keys.OemSemicolon && isShiftDown)
            {
                return ':';
            }
            if (key == Keys.OemTilde && !isShiftDown)
            {
                return '`';
            }
            if (key == Keys.OemTilde && isShiftDown)
            {
                return '~';
            }
            if (key == Keys.Subtract)
            {
                return '-';
            }

            return null;
        }
    }
}