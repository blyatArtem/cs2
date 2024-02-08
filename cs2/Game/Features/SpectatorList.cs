﻿using cs2.Game.Objects;
using cs2.GameOverlay;
using GameOverlay.Drawing;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Features
{
    internal static class SpectatorList
    {
        public static void Update()
        {
            if (!Enabled)
                return;
            _spectators.Clear();
            foreach (var entity in Program.Entities)
            {
                if (entity.AddressBase == 0 || entity.IsAlive() || entity.Team != Program.LocalPlayer.Team)
                    continue;

                IntPtr obs = Memory.Read<IntPtr>(entity.AddressBase + C_BasePlayerPawn.m_pObserverServices);
                IntPtr playerPawn = Memory.Read<IntPtr>(obs + CPlayer_ObserverServices.m_hObserverTarget);
                IntPtr addressBase = ReadAddressBase(playerPawn);

                if (addressBase == Program.LocalPlayer.AddressBase)
                    _spectators.Add(entity.Nickname);
            }
        }

        public static void Draw(Graphics g)
        {
            if (!Enabled)
                return;

            g.DrawTextWithBackground(Fonts.Consolas, Brushes.White, Brushes.HalfBlack, new Point(10, 10), $"{string.Join("\n", _spectators)}");
        }

        private static IntPtr ReadAddressBase(IntPtr playerPawn)
        {
            IntPtr entityList = Memory.Read<IntPtr>(Memory.ClientPtr + Client.dwEntityList);
            var listEntrySecond = Memory.Read<IntPtr>(entityList + 0x8 * ((playerPawn & 0x7FFF) >> 9) + 16);
            return listEntrySecond == IntPtr.Zero
                ? IntPtr.Zero
                : Memory.Read<IntPtr>(listEntrySecond + 120 * (playerPawn & 0x1FF));
        }

        public static bool Enabled
        {
            get; set;
        } = true;

        private static List<string> _spectators = new List<string>();
    }
}
