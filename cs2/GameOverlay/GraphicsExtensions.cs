﻿using GameOverlay.Drawing;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay
{
    internal static class GraphicsExtensions
    {
        public static void DrawLineWorld(this Graphics g, SolidBrush brush, float stroke, params Vector3[] verticesWorld)
        {

            var verticesScreen = verticesWorld
                .Select(v => Program.LocalPlayer.MatrixViewProjectionViewport.Transform(v))
                .Where(v => v.Z < 1)
                .Select(v => new Vector2(v.X, v.Y)).ToArray();
            if (verticesScreen.Length < 2 || verticesScreen.Length % 2 != 0) return;
            g.DrawLine(brush,
                new Point(verticesScreen[0].X, verticesScreen[0].Y),
                new Point(verticesScreen[1].X, verticesScreen[1].Y), stroke);
        }

        public static Vector3 ToScreenPos(this Vector3 dot)
        {
            Vector3 screenPos = Program.LocalPlayer.MatrixViewProjectionViewport.Transform(dot);
            if (screenPos.IsValidScreen())
                return new Vector3(screenPos.X, screenPos.Y, screenPos.Z);
            return Vector3.Zero;
        }

        //public static float Distance(Vector2 v1, Vector2 v2)
        //{
        //    return MathF.Sqrt(((v2.X - v1.X) * (v2.X - v1.X)) + ((v2.Y - v1.Y) * (v2.Y - v1.Y)));
        //}

        public static Rectangle GetTextRect(this Graphics g, global::GameOverlay.Drawing.Font font, float fontSize, string text, float x = 0, float y = 0)
        {
            float num = (x < 0f) ? ((float)g.Width + x) : ((float)g.Width - x);
            float num2 = (y < 0f) ? ((float)g.Height + y) : ((float)g.Height - y);
            if (num <= fontSize)
            {
                num = (float)g.Width;
            }
            if (num2 <= fontSize)
            {
                num2 = (float)g.Height;
            }
            TextLayout textLayout = new TextLayout(Fonts.FontFactory, text, font.TextFormat, num, num2);
            float num3 = font.FontSize * 0.25f;
            RawRectangleF rect = new RawRectangleF(x - num3, y - num3, x + textLayout.Metrics.Width + num3, y + textLayout.Metrics.Height + num3);

            textLayout.Dispose();

            return new Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static bool IsValidScreen(this Vector3 value)
        {
            return !value.X.IsInfinityOrNaN() && !value.Y.IsInfinityOrNaN() && value.Z >= 0 && value.Z < 1;
        }

        public static bool IsInfinityOrNaN(this float value)
        {
            return float.IsNaN(value) || float.IsInfinity(value);
        }

        public static bool IsMouseOn(this Rectangle rect)
        {
            Point pos = Input.CursorPos;
            return pos.X > rect.Left && pos.X < rect.Right & pos.Y > rect.Top && pos.Y < rect.Bottom;
        }

        public static bool Touching(this Vector2 p, Circle c, float centerX, float centerY)
        {
            float distX = p.X - centerX;
            float distY = p.Y - centerY;
            float distance = MathF.Sqrt((distX * distX) + (distY * distY));

            return distance <= c.Radius;
        }

        private const float _PI_Over_180 = (float)Math.PI / 180.0f;

        private const float _180_Over_PI = 180.0f / (float)Math.PI;

        public static float DegreeToRadian(this float degree)
        {
            return degree * _PI_Over_180;
        }

        public static double RadianToDegree(this double radian)
        {
            return radian * _180_Over_PI;
        }
    }
}
