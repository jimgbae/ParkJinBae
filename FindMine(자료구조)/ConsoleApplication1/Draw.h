#pragma once
#include"Main.h"
class Draw
{
public:
	Draw();
	void DrawMidText(string str, int x, int y);
	void DrawPoint(string str, int x, int y);
	void MapDraw(int Width, int Height);
	void Erase(int x, int y);
	void DrawFlag(int x, int y);
	~Draw();
	inline void gotoxy(int x, int y)
	{
		COORD Pos = { x, y };
		SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), Pos);
	}
};

