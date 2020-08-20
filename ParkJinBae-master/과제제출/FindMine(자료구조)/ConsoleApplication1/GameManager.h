#pragma once
#include"Main.h"
#include"MineManager.h"
#include"FlagManager.h"

enum GAME
{
	GAME_START = 1,
	GAME_OPTION,
	GAME_EXIT
};

enum COURSE
{
	COURSE_EASY = 1,
	COURSE_NORMAL,
	COURSE_HARD,
	COURSE_VERY_HARD
};

struct Cursor
{
	string strCursor;
	int ix;
	int iy;
};

class GameManager
{
private:
	MineManager m_MineManager;
	FlagManager m_FlagManager;
	Draw m_DrawManager;
	Cursor m_Cursor;
	string m_strMovetail;
	bool m_bGameOver;
	bool m_bExit;
	bool m_bMineCheck;
	int m_iWidth;
	int m_iHeight;
	int m_iMineList;
	int m_iDefault;
public:
	GameManager();
	void Setting();
	void GamePlay();
	void PlayingDraw();
	void SelectCourse();
	void SetCursor();
	void SettingGame(int iSelect);
	void Playing();
	void CheckMine();
	void CheckWin();
	void Move(char ch, int Width, int Height);
	void NewGame();
	void SetCourse(int iSelect);
	void Delete();
	~GameManager();
};
