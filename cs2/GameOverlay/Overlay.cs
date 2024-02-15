﻿using cs2.Game;
using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay.UI;
using cs2.GameOverlay.UI.Controls;
using cs2.GameOverlay.UI.Forms;
using cs2.Offsets;
using cs2.Offsets.Interfaces;
using cs2.PInvoke;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static cs2.PInvoke.User32;

namespace cs2.GameOverlay
{
    internal class Overlay : IDisposable
    {
        public Overlay()
        {
            Graphics g = new Graphics()
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true
            };

            Window = new GraphicsWindow(0, 0, 1920, 1080, g)
            {
                FPS = 90,
                IsTopmost = true,
                IsVisible = true
            };

            Window.DestroyGraphics += Window_DestroyGraphics;
            Window.DrawGraphics += Window_DrawGraphics;
            Window.SetupGraphics += Window_SetupGraphics;

            Window.Create();
            Window.Join();
        }

        private void Update()
        {
            GlobalVars.Update();
            Program.Entities.Clear();
            LocalPlayer.Current.Update();
            for (int i = 0; i < 12; i++)
            {
                Entity entity = new(i);
                entity.Update();
                Program.Entities.Add(entity);
            }

            Input.Update();
            Scoreboard.Update();
            keyHome.Update();

            if (keyHome.state == Input.KeyState.PRESSED)
            {
                drawUI = !drawUI;
                if (drawUI)
                {
                    var attributes = ExtendedWindowStyle.Topmost | ExtendedWindowStyle.Transparent /*| ExtendedWindowStyle.Layered | ExtendedWindowStyle.NoActivate*/;
                    Input.PressKey(Input.ScanCodeShort.ESCAPE);
                    User32.SetWindowLong(Window.Handle, -20, (int)attributes);
                    User32.SendMessage(Window.Handle, 0x0020, IntPtr.Zero, IntPtr.Zero);
                    // WM_SETCURSOR 
                    // костыльное обновление курсора :)
                }
                else
                {
                    Input.PressKey(Input.ScanCodeShort.ESCAPE);
                    var attributes = ExtendedWindowStyle.Topmost | ExtendedWindowStyle.Transparent | ExtendedWindowStyle.Layered | ExtendedWindowStyle.NoActivate;
                    User32.SetWindowLong(Window.Handle, -20, (int)attributes);
                }
            }

            if (drawUI)
            {
                UIForm.FocusedFrame = false;
                Forms.OrderByDescending(x => x.Layer).ToList().ForEach(x => x.Update());
            }
        }

        private void Draw(Graphics g)
        {
            WallHack.Draw(g);
            AimAssist.Draw(g);
            Crosshair.Draw(g);
            Scoreboard.Draw(g);
        }

        private void OnDraw(Graphics g)
        {
            Update();
            g.ClearScene();

            Draw(g);

            if (drawUI)
            {
                g.FillRectangle(Brushes.HalfBlack, new Rectangle(0, 0, g.Width, g.Height));
            }
            Forms.OrderBy(x => x.Layer).ToList().ForEach(x => x.Draw(g));
        }

        #region Events

        private void Window_SetupGraphics(object? sender, SetupGraphicsEventArgs e)
        {
            Program.Log($"Window_SetupGraphics {e.Graphics.Width}, {e.Graphics.Height}");
            Brushes.Initialize(e.Graphics);
            Fonts.Initialize(e.Graphics);
            InitializeForms(e.Graphics);
        }

        private void Window_DestroyGraphics(object? sender, DestroyGraphicsEventArgs e)
        {
            Brushes.Dispose();
            Fonts.Dispose();
            Program.Log("setup graphics");
        }

        private void Window_DrawGraphics(object? sender, DrawGraphicsEventArgs e) => OnDraw(e.Graphics);

        #endregion

        #region Forms

        private void InitializeForms(Graphics g)
        {
            UIControl.initGraphics = g;
            UIForm f = new FormVisuals();
            Forms = new List<UIForm>()
            {
                new FormSpectators(),
                new FormRadar(),
                f,
                new FormAimAssist((int)f.Rect.Right + 10)
            };

            UIControl.initGraphics = null;
        }

        #endregion

        #region Dispose

        ~Overlay()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                Window.Dispose();

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public GraphicsWindow Window
        {
            get; private set;
        }

        public List<UIForm> Forms
        {
            get; private set;
        }

        public static bool drawUI;

        private Input.Key keyHome = new Input.Key(36);
        private bool _disposed;
    }
}