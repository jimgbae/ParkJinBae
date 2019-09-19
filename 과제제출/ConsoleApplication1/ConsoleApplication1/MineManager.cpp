#include "MineManager.h"


MineManager::MineManager()
{
	m_Mine = NULL;
	m_iMineList = NULL;
	m_MineMap = NULL;
	m_iDirection = DIRECTION_UP;
}

void MineManager::DrawMap(int x, int y)
{
	for (int i = 0; i < m_iNoMineList; i++)
	{
		if (m_MineMap[i].m_ix == x && m_MineMap[i].m_iy == y)
			m_DrawManager.DrawPoint(m_MineMap[i].m_str, m_MineMap[i].m_ix, m_MineMap[i].m_iy);
	}
}

void MineManager::SettingMineList(int MineList)
{
	m_iMineList = MineList;
}

void MineManager::SetMine(int Width, int Height)
{
	int x, y;
	int Num = 0, i = 0;
	m_iNoMineList = (Width*Height)+1 - m_iMineList;
	if (m_MineMap != NULL)
		m_MineMap = NULL;
	if (m_Mine != NULL)
		m_Mine = NULL;
	m_MineMap = new MineMap[m_iNoMineList+1];
	m_Mine = new Mine[m_iMineList];
	while (i < m_iMineList)
	{
		x = (rand() % Width);
		y = (rand() % Height);
		if (CheckMine(x, y, i))
			m_Mine[i++].SetMineXY(x, y);
	}
	for (y = 0; y < Height; y++)
	{
		for (x = 0; x < Width; x++)
		{
			if (CheckMine(x, y, m_iMineList))
			{
				m_MineMap[Num].m_str = "бр";
				m_MineMap[Num].m_ix = x;
				m_MineMap[Num].m_bCheck = true;
				m_MineMap[Num++].m_iy = y;
				if (Num == m_iNoMineList)
					return;
			}
		}
	}
}
int MineManager::SearchMine(int x, int y)
{
	int iStack;
	for (int i = 0; i < m_iMineList; i++)
	{
		if (m_Mine[i].GetX() == x && m_Mine[i].GetY() == y)
			return PLAY_MINE;
	}
	return false;
}

void MineManager::DrawMine()
{
	for (int i = 0; i < m_iMineList; i++)
	{
		m_DrawManager.DrawPoint("в├", m_Mine[i].GetX(), m_Mine[i].GetY());
	}
}

void MineManager::SetFindMine(int x, int y, int Width, int Height)
{
	int tmpx = x, tmpy = y;
	int iStack = 0;
	for (int i = 0; i < m_iNoMineList; i++)
	{
		if (m_MineMap[i].m_ix == x && m_MineMap[i].m_iy == y)
		{
			if (m_MineMap[i].m_bCheck == false)
				return;
		}
	}
	for (int i = 0; i < m_iMineList; i++)
		iStack += FindMine(m_Mine[i], tmpx, tmpy);
	if (iStack == 0)
	{
		if (CheckMap(x, y))
		{
			CheckMineMap(tmpx, tmpy, iStack);
			CheckMine(tmpx, tmpy, Width, Height);
		}
	}
	else
	{
		if (CheckMap(x, y))
			CheckMineMap(tmpx, tmpy, iStack);
		iStack = 0;
	}
	return;
}

void MineManager::CheckMine(int x, int y, int Width, int Height)
{
	int iStack = 0;
	for (int i = 0; i < m_iMineList; i++)
		iStack += FindMine(m_Mine[i], x, y);
	if (iStack == 0)
	{
		if (CheckMap(x, y))
		{
			CheckMineMap(x, y, iStack);
			CheckMine(x, y, Width, Height);
		}
	}
	else
	{
		if (CheckMap(x, y))
			CheckMineMap(x, y, iStack);
		iStack = 0;
	}
	if (x - 1 >= 0 && y - 1 >= 0)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x - 1, y - 1);
		if (iStack == 0)
		{
			if (CheckMap(x - 1, y - 1))
			{
				CheckMineMap(x - 1, y - 1, iStack);
				CheckMine(x - 1, y - 1, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x - 1, y - 1))
				CheckMineMap(x - 1, y - 1, iStack);
			iStack = 0;
		}
	}
	if (x - 1 >= 0)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x - 1, y);
		if (iStack == 0)
		{
			if (CheckMap(x - 1, y))
			{
				CheckMineMap(x - 1, y, iStack);
				CheckMine(x - 1, y, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x - 1, y))
				CheckMineMap(x - 1, y, iStack);
			iStack = 0;
		}
	}
	if (x - 1 >= 0 && y + 1 <= Height)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x - 1, y + 1);
		if (iStack == 0)
		{
			if (CheckMap(x - 1, y + 1))
			{
				CheckMineMap(x - 1, y + 1, iStack);
				CheckMine(x - 1, y + 1, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x - 1, y + 1))
				CheckMineMap(x - 1, y + 1, iStack);
			iStack = 0;
		}
	}
	if (y - 1 >= 0)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x, y - 1);
		if (iStack == 0)
		{
			if (CheckMap(x, y - 1))
			{
				CheckMineMap(x, y - 1, iStack);
				CheckMine(x, y - 1, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x, y - 1))
				CheckMineMap(x, y - 1, iStack);
			iStack = 0;
		}
	}
	if (x + 1 <= Width && y - 1 >= 0)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x + 1, y - 1);
		if (iStack == 0)
		{
			if (CheckMap(x + 1, y - 1))
			{
				CheckMineMap(x + 1, y - 1, iStack);
				CheckMine(x + 1, y - 1, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x + 1, y - 1))
				CheckMineMap(x + 1, y - 1, iStack);
			iStack = 0;
		}
	}
	if (x + 1 <= Width)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x + 1, y);
		if (iStack == 0)
		{
			if (CheckMap(x + 1, y))
			{
				CheckMineMap(x + 1, y, iStack);
				CheckMine(x + 1, y, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x + 1, y))
				CheckMineMap(x + 1, y, iStack);
			iStack = 0;
		}
	}
	if (x + 1 <= Width && y + 1 <= Height)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x + 1, y + 1);
		if (iStack == 0)
		{
			if (CheckMap(x + 1, y + 1))
			{
				CheckMineMap(x + 1, y + 1, iStack);
				CheckMine(x + 1, y + 1, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x + 1, y + 1))
				CheckMineMap(x + 1, y + 1, iStack);
			iStack = 0;
		}
	}
	if (y + 1 <= Height)
	{
		for (int i = 0; i < m_iMineList; i++)
			iStack += FindMine(m_Mine[i], x, y + 1);
		if (iStack == 0)
		{
			if (CheckMap(x, y + 1))
			{
				CheckMineMap(x, y + 1, iStack);
				CheckMine(x, y + 1, Width, Height);
			}
		}
		else
		{
			if (CheckMap(x, y + 1))
				CheckMineMap(x, y + 1, iStack);
			iStack = 0;
		}
	}
}

bool MineManager::CheckMap(int x, int y)
{
	for (int i = 0; i < m_iNoMineList; i++)
	{
		if (m_MineMap[i].m_ix == x && m_MineMap[i].m_iy == y)
			return m_MineMap[i].m_bCheck;
	}
	return false;
}


void MineManager::CheckMineMap(int x, int y,int Stack)
{
	if (Stack == 0)
	{
		for (int i = 0; i < m_iNoMineList; i++)
		{
			if (m_MineMap[i].m_ix == x && m_MineMap[i].m_iy == y)
			{
				if (m_MineMap[i].m_bCheck == true)
				{
					m_MineMap[i].m_str = "бс";
					m_DrawManager.DrawPoint(m_MineMap[i].m_str, m_MineMap[i].m_ix, m_MineMap[i].m_iy);
					m_MineMap[i].m_bCheck = false;
				}
			}
		}
	}
	else
	{
		for (int i = 0; i < m_iNoMineList; i++)
		{
			if (m_MineMap[i].m_ix == x && m_MineMap[i].m_iy == y)
			{
				m_MineMap[i].m_str = to_string(Stack);
				m_MineMap[i].m_bCheck = false;
				m_DrawManager.DrawPoint(m_MineMap[i].m_str, m_MineMap[i].m_ix, m_MineMap[i].m_iy);
				return;
			}
		}
	}
}

int MineManager::FindMine(Mine m_Mine, int x, int y)
{
	int iStack = 0;
	while (1)
	{
		if (x - 1 == m_Mine.GetX() && y - 1 == m_Mine.GetY())
			iStack++;
		if (x == m_Mine.GetX() && y - 1 == m_Mine.GetY())
			iStack++;
		if (x + 1 == m_Mine.GetX() && y - 1 == m_Mine.GetY())
			iStack++;
		if (x - 1 == m_Mine.GetX() && y == m_Mine.GetY())
			iStack++;
		if (x + 1 == m_Mine.GetX() && y == m_Mine.GetY())
			iStack++;
		if (x - 1 == m_Mine.GetX() && y + 1 == m_Mine.GetY())
			iStack++;
		if (x == m_Mine.GetX() && y + 1 == m_Mine.GetY())
			iStack++;
		if (x + 1 == m_Mine.GetX() && y + 1 == m_Mine.GetY())
			iStack++;
		return iStack;
	}
}

bool MineManager::CheckMine(int x, int y, int i)
{
	bool bCheck;
	for (int j = 0; j < i; j++)
	{
		bCheck = m_Mine[j].CheckXY(x, y);
		if (bCheck == false)
			return false;
	}
	return true;
}

bool MineManager::CheckWin()
{
	for (int i = 0; i < m_iNoMineList; i++)
	{
		if (m_MineMap[i].m_bCheck == true)
			return false;
	}
	return true;
}

void MineManager::DeleteMine()
{
	if (m_Mine != NULL)
	{
		delete[] m_Mine;
		m_Mine = NULL;
	}
	if (m_MineMap != NULL)
	{
		delete[] m_MineMap;
		m_MineMap = NULL;
	}
}


MineManager::~MineManager()
{
}
