﻿using cs2.Game.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game.Objects
{
    internal class Weapon
    {
        public Weapon()
        {
            //CCSWeaponBaseVData
            //C_CSWeaponBase 
        }

        public void Update(IntPtr ptr)
        {
            WeaponPtr = ptr;
            WeaponIndex = (WeaponDefIndex)Memory.Read<short>(WeaponPtr + 0x1098 + 0x50 + 0x1BA); // C_EconEntity.m_AttributeManager + m_Item + m_iItemDefinitionIndex 0x1158

        }

		public bool IsSniperRifle
		{
			get => WeaponIndex == WeaponDefIndex.Awp ||
                WeaponIndex == WeaponDefIndex.Ssg08 ||
                WeaponIndex ==  WeaponDefIndex.Scar20 ||
                WeaponIndex == WeaponDefIndex.G3Sg1;
		}

        public char ToIcon()
        {
			if (WeaponIndex == WeaponDefIndex.Deagle) return '\uE001';
			else if (WeaponIndex == WeaponDefIndex.Elite) return '\uE002';
			else if (WeaponIndex == WeaponDefIndex.Fiveseven) return '\uE003';
			else if (WeaponIndex == WeaponDefIndex.Glock) return '\uE004';
			else if (WeaponIndex == WeaponDefIndex.Ak47) return '\uE007';
			else if (WeaponIndex == WeaponDefIndex.Aug) return '\uE008';
			else if (WeaponIndex == WeaponDefIndex.Awp) return '\uE009';
			else if (WeaponIndex == WeaponDefIndex.Famas) return '\uE00a';
			else if (WeaponIndex == WeaponDefIndex.G3Sg1) return '\uE00b';
			else if (WeaponIndex == WeaponDefIndex.Galilar) return '\uE00d';
			else if (WeaponIndex == WeaponDefIndex.M249) return '\uE03c';
			else if (WeaponIndex == WeaponDefIndex.M4A1) return '\uE00e';
			else if (WeaponIndex == WeaponDefIndex.Mac10) return '\uE011';
			else if (WeaponIndex == WeaponDefIndex.P90) return '\uE024';
			else if (WeaponIndex == WeaponDefIndex.Mp5) return '\uE021';
			else if (WeaponIndex == WeaponDefIndex.Ump45) return '\uE018';
			else if (WeaponIndex == WeaponDefIndex.Xm1014) return '\uE019';
			else if (WeaponIndex == WeaponDefIndex.Bizon) return '\uE01a';
			else if (WeaponIndex == WeaponDefIndex.Mag7) return '\uE01b';
			else if (WeaponIndex == WeaponDefIndex.Negev) return '\uE01c';
			else if (WeaponIndex == WeaponDefIndex.Sawedoff) return '\uE01d';
			else if (WeaponIndex == WeaponDefIndex.Tec9) return '\uE01e';
			else if (WeaponIndex == WeaponDefIndex.Taser) return '\uE01f';
			else if (WeaponIndex == WeaponDefIndex.Hkp2000) return '\uE013';
			else if (WeaponIndex == WeaponDefIndex.Mp7) return '\uE021';
			else if (WeaponIndex == WeaponDefIndex.Mp9) return '\uE022';
			else if (WeaponIndex == WeaponDefIndex.Nova) return '\uE023';
			else if (WeaponIndex == WeaponDefIndex.P250) return '\uE020';
			else if (WeaponIndex == WeaponDefIndex.Shield) return '?';
			else if (WeaponIndex == WeaponDefIndex.Scar20) return '\uE026';
			else if (WeaponIndex == WeaponDefIndex.Sg556) return '\uE027';
			else if (WeaponIndex == WeaponDefIndex.Ssg08) return '\uE028';
			else if (WeaponIndex == WeaponDefIndex.Knifegg) return '?';
			else if (WeaponIndex == WeaponDefIndex.Knife) return '\uE02a';
			else if (WeaponIndex == WeaponDefIndex.Flashbang) return '\uE02b';
			else if (WeaponIndex == WeaponDefIndex.Hegrenade) return '\uE02c';
			else if (WeaponIndex == WeaponDefIndex.Smokegrenade) return '\uE02d';
			else if (WeaponIndex == WeaponDefIndex.Molotov) return '\uE02e';
			else if (WeaponIndex == WeaponDefIndex.Decoy) return '\uE02f';
			else if (WeaponIndex == WeaponDefIndex.Incgrenade) return '\uE030';
			else if (WeaponIndex == WeaponDefIndex.C4) return '\uE031';
			else if (WeaponIndex == WeaponDefIndex.KnifeT) return '\uE03b';
			else if (WeaponIndex == WeaponDefIndex.M4A1Silencer) return '\uE010';
			else if (WeaponIndex == WeaponDefIndex.UspSilencer) return '\uE03d';
			else if (WeaponIndex == WeaponDefIndex.Cz75A) return '\uE03f';
			else if (WeaponIndex == WeaponDefIndex.Revolver) return '\uE040';
			else if (WeaponIndex == WeaponDefIndex.Bayonet) return '\uE1f4';
			else if (WeaponIndex == WeaponDefIndex.KnifeFlip) return '\uE1f9';
			else if (WeaponIndex == WeaponDefIndex.Knifegg) return '\uE1fa';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_KARAMBIT) return '\uE1fb';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_M9_BAYONET) return '\uE1fc';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_TACTICAL) return '\uE1fd';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_FALCHION) return '\uE200';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_SURVIVAL_BOWIE) return '\uE202';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_BUTTERFLY) return '\uE203';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_PUSH) return '\uE204';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_URSUS) return '\uE02a';
			else if (WeaponIndex == WeaponDefIndex.KNIFE_WIDOWMAKER) return '\uE02a';
            return '\uE02a';
        }

        public WeaponDefIndex WeaponIndex { get; set; }

        private IntPtr WeaponPtr { get; set; }
    }
}
