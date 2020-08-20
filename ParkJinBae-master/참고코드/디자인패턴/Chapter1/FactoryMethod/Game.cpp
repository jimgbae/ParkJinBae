#include "Game.h"

void Game::Initialized()
{
	view = CreateView();
}

void Game::Update()
{
	view->Render();
}

void Game::Finished()
{
	delete view;
}

/* My Game */
View * MyGame::CreateView()
{
	return new MyView();
}

void option::Initialized()
{
	opview = CreateView();
}

void option::Update()
{
	opview->Render();
}

void option::Finished()
{
	delete opview;
}

/* Option */
View * Option::CreateView()
{
	return new OptionView();
}
