#pragma once
#include"Main.h"
#include"Node.h"
#include"Mine.h"
typedef Node SaveMine;

enum PLAY
{
	PLAY_NOMINE,
	PLAY_MINE
};

enum DIRECTION
{
	DIRECTION_UP,
	DIRECTION_DOWN
};


struct MineMap
{
	string m_str;
	int m_ix;
	int m_iy;
	bool m_bCheck;
};

class MineManager
{
private:
	Draw m_DrawManager;
	Mine* m_Mine;
	SaveMine* m_SaveMine;
	MineMap* m_MineMap;
	int m_iMineList;
	int m_iNoMineList;
	int m_iDirection;
public:
	MineManager();
	void SettingMineList(int MineList);
	void SetMine(int Width, int Height);
	void DrawMine();
	void DrawMap(int x, int y);
	void CheckMine(int x, int y, int Width, int Height);
	void CheckMineMap(int x, int y, int Stack);
	void SearchMine();
	void ShowSaveMine(int Width, int Height);
	bool CheckMap(int x, int y);
	int SearchMine(int x, int y);
	bool CheckMine(int x, int y, int i);
	void SetFindMine(int x, int y, int Width, int Height);
	int FindMine(Mine m_Mine, int x, int y);
	bool CheckWin();
	void DeleteMine();
	void DeleteSaveMine(SaveMine* tmp);
	~MineManager();
};