//------------------------------------------//
//		script by:			gregor hempel	//
//		date of creation:	28.05.2019		//
//		last time edited:	29.05.2019		//
//		times edited:		2				//
//------------------------------------------//
//notes:
//transfered once from older project version

public interface IHasFaction
{
    void SetFlags(ref EFaction _enum, params EFaction[] _flags);
    void UnsetFlags(ref EFaction _enum, params EFaction[] _flags);
}