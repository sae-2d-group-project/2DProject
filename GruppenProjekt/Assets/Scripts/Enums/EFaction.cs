//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	28.05.2019		//
//		last time edited:	29.05.2019		//
//		times edited:		2				//
//------------------------------------------//
//notes:
//transfered once from older project version

using System;

[Flags]
public enum EFaction : byte
{
    None            = 0,        // 00000000
    Neutral         = 1 << 0,   // 00000001
    Friendly        = 1 << 1,   // 00000010
    Hostile         = 1 << 2,   // 00000100
    Wild            = 1 << 3,   // 00001000
    Human           = 1 << 4,   // 00010000
    Animal          = 1 << 5,   // 00100000
    Synthetic       = 1 << 6,   // 01000000
    Invulnerable    = 1 << 7    // 10000000
}