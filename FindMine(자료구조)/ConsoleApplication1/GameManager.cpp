#include "GameManager.h"


GameManager::GameManager()
{
	m_bGameOver = false;
	m_iWidth = EASY_WIDTH;
	m_iHeight = EASY_HEIGHT;
	m_iMineList = NULL;
	m_iDefault = COURSE_EASY;
	m_bExit = false;
	m_bMineCheck = false;
}

void GameManager::GamePlay()
{
	while (!m_bExit)
	{
		Setting();
		system("cls");
		SettingGame(m_iDefault);
		Playing();
	}
}

void GameManager::SettingGame(int iSelect)
{
	SetCourse(iSelect);
	Delete();
	m_MineManager.SetMine(m_iWidth, m_iHeight);
	SetCursor();
	m_bGameOver = false;
}

void GameManager::Setting()
{
	char buf[256];
	sprintf(buf, "mode con: lines=%d cols=%d", m_iHeight *3, m_iWidth * 5);
	system(buf);
}

void GameManager::PlayingDraw()
{
	m_DrawManager.Erase(m_Cursor.ix, m_Cursor.iy);
	m_MineManager.DrawMap(m_Cursor.ix, m_Cursor.iy);
	m_FlagManager.DrawFlag();
}

void GameManager::Playing()
{
	m_MineManager.ShowSaveMine(m_iWidth, m_iHeight);
	while (!m_bGameOver)
	{
		m_DrawManager.DrawMidText("N : NewGame", m_iWidth, m_iHeight + 1);
		char ch = getch();
		if (ch == KEY_OPTION)
		{
			NewGame();
			Delete();
			return;
		}
	}
}

void GameManager::CheckWin()
{
	if (m_MineManager.CheckWin())
	{
		m_DrawManager.DrawMidText("게임 승리!", m_iWidth, m_iHeight+1);
		m_MineManager.DrawMine();
		getch();
		m_bGameOver = true;
	}
	else
		return;
}

void GameManager::CheckMine()
{
	switch (m_MineManager.SearchMine(m_Cursor.ix, m_Cursor.iy))
	{
	case PLAY_MINE:
		m_MineManager.DrawMine();
		m_DrawManager.DrawMidText("GAME OVER", m_iWidth, m_iHeight*0.5f);
		m_bGameOver = true;
		getch();
		break;
	case PLAY_NOMINE:
		m_MineManager.SetFindMine(m_Cursor.ix, m_Cursor.iy, m_iWidth, m_iHeight);
		break;
	}
}

void GameManager::NewGame()
{
	Sleep(1000);
	int iSelect;
	system("cls");
	m_DrawManager.DrawMidText("게임 종료", m_iWidth, m_iHeight*0.5f);
	m_DrawManager.DrawMidText("1.Yes 2.No", m_iWidth, (m_iHeight*0.5f) + 1);
	m_DrawManager.DrawMidText("입력 : ", m_iWidth, (m_iHeight*0.5f) + 2);
	cin >> iSelect;
	if (iSelect == 1)
	{
		Delete();
		m_bExit = true;
		return;
	}
	SelectCourse();
	m_bGameOver = false;
}

void GameManager::SelectCourse()
{
	int iSelect;
	system("cls");
	m_DrawManager.DrawMidText("Easy : 1", m_iWidth, m_iHeight*0.4f);
	m_DrawManager.DrawMidText("Normal : 2", m_iWidth, m_iHeight*0.4f + 1);
	m_DrawManager.DrawMidText("Hard : 3", m_iWidth, m_iHeight*0.4f + 2);
	m_DrawManager.DrawMidText("입력 : ", m_iWidth, m_iHeight*0.4f + 3);
	cin >> iSelect;
	switch (iSelect)
	{
	case COURSE_EASY:
	case COURSE_NORMAL:
	case COURSE_HARD:
		m_MineManager.DeleteMine();
		m_FlagManager.DeleteFlag();
		SetCourse(iSelect);
		break;
	}
}

void GameManager::SetCursor()
{
	m_Cursor.strCursor = "◆";
	m_Cursor.ix = m_iWidth / 2;
	m_Cursor.iy = m_iHeight / 2;
}

void GameManager::Move(char ch, int Width, int Height)
{
	switch (ch)
	{
	case KEY_LEFT:
		if (m_Cursor.ix - 1 >= 0)
			m_Cursor.ix--;
		break;
	case KEY_RIGHT:
		if (m_Cursor.ix + 1 < Width)
			m_Cursor.ix++;
		break;
	case KEY_UP:
		if (m_Cursor.iy - 1 >= 0)
			m_Cursor.iy--;
		break;
	case KEY_DOWN:
		if (m_Cursor.iy + 1 < Height)
			m_Cursor.iy++;
		break;
	}
}

void GameManager::SetCourse(int iSelect)
{
	switch (iSelect)
	{
	case COURSE_EASY:
		m_iWidth = EASY_WIDTH;
		m_iHeight = EASY_HEIGHT;
		m_iDefault = COURSE_EASY;
		m_MineManager.SettingMineList(EASY_MINE);
		m_FlagManager.SettingFlagList(EASY_MINE);
		break;
	case COURSE_NORMAL:
		m_iWidth = NORMAL_WIDTH;
		m_iHeight = NORMAL_HEIGHT;
		m_iDefault = COURSE_NORMAL;
		m_MineManager.SettingMineList(NORMAL_MINE);
		m_FlagManager.SettingFlagList(NORMAL_MINE);
		break;
	case COURSE_HARD:
		m_iWidth = HARD_WIDTH;
		m_iHeight = HARD_HEIGHT;
		m_iDefault = COURSE_HARD;
		m_MineManager.SettingMineList(HARD_MINE);
		m_FlagManager.SettingFlagList(HARD_MINE);
		break;
	}
}

void GameManager::Delete()
{
	m_MineManager.DeleteMine();
	m_FlagManager.DeleteFlag();
}


GameManager::~GameManager()
{
}
