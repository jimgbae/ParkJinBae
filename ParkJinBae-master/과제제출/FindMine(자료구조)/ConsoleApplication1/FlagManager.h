#pragma once
#include"Main.h"
#include"Draw.h"
#include"Node.h"
typedef Node Flag;

class FlagManager
{
private:
	Draw m_DrawManager;
	Flag* m_pFlag;
	Flag** m_pSaveFlag;
	bool m_bCheckFlag;
	int m_iFlagList;
	int m_iPlayingFlag;
public:
	FlagManager();
	void DrawFlag();
	void CheckFlag(Flag* tmp);
	void SettingFlagList(int FlagList);
	void SearchFlag(Flag* tmp, int x, int y);
	void SettingFlag(int ix, int iy);
	void MakeFlag(int ix, int iy);
	void ResetFlag();
	void DeleteFlag();
	void Delete(Flag* tmp);
	~FlagManager();
};

