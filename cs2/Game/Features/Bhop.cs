﻿using cs2.Game.Objects;
using cs2.Offsets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cs2.Game.Features
{
    //internal class Bhop
    //{
    //    public static void Start()
    //    {
    //        _localPlayer = new LocalPlayer();
    //        _key = new Input.Key(32); // space

    //        new Thread(() =>
    //        {
    //            for (; ; )
    //            {
    //                _key.Update();
    //                Thread.Sleep(1);
    //                Update();
    //            }
    //        }).Start();
    //    }

    //    public static void Update()
    //    {
    //        _localPlayer.AddressBase = _localPlayer.ReadAddressBase();
    //        int flags = Memory.Read<int>(_localPlayer.AddressBase + OffsetsLoader.C_BaseEntity.m_fFlags);

    //        if (_key.state == Input.KeyState.DOWN)
    //        {
    //            if (flags == 65665 || flags == 65667)
    //            {
    //                Input.MouseMiddle();
    //            }
    //        }
    //        else
    //        {
    //            Thread.Sleep(10);
    //        }
    //    }

    //    private static Input.Key _key;
    //    private static LocalPlayer _localPlayer;
    //}
}
