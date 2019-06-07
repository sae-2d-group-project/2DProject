//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	30.05.2019		//
//		last time edited:	30.05.2019		//
//		times edited:		1				//
//------------------------------------------//
//notes:
//

public enum EFireMode : byte
{
    Single = 0,
    SemiAuto = 1 << 0,
    FullAuto = 1 << 1,
}