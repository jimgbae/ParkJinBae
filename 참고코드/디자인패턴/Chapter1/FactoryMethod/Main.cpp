#include "Game.h"

int main()
{
	MyGame* game = new MyGame();
	Option* option = new Option();


	cout << "1. ����  2.�ɼ�  3.��ü" << endl;
	int iSelect;
	cin >> iSelect;
	switch (iSelect)
	{
	case 1:
		game->Initialized();
		game->Update();
		game->Finished();
		break;
	case 2:
		option->Initialized();
		option->Update();
		option->Finished();
		break;
	case 3:
		game->Initialized();
		game->Update();
		game->Finished();
		option->Initialized();
		option->Update();
		option->Finished();
	}

	delete game;
}