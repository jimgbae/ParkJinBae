#include "Draw.h"


Draw::Draw()
{
}

void Draw::DrawMidText(string str, int x, int y)
{
	if (x > str.size() / 2)
		x -= str.size() / 2;
	gotoxy(x, y);
	cout << str;
	return;
}

void Draw::MapDraw(int Width, int Height)
{
	gotoxy(0, 0);
	for (int y = 0; y < Height; y++)
	{
		for (int x = 0; x < Width; x++)
			cout << "��";
	}
	DrawMidText("���۹�:w,a,s,d", Width, Height + 2);
	DrawMidText("Ŭ�� : ENTER", Width, Height + 3);
	DrawMidText("��� : Z ������ : n", Width, Height + 4);
}
void Draw::Erase(int x, int y)
{
	gotoxy(x * 2, y);
	cout << "��";
}


void Draw::DrawPoint(string str, int x, int y)
{
	gotoxy(x * 2, y);
	cout << str;
	gotoxy(-1, -1);
	return;
}

void Draw::DrawFlag(int x, int y)
{
	gotoxy(x * 2, y);
	cout << "��";
	gotoxy(-1, -1);
	return;
}

Draw::~Draw()
{
}
