#include"Main.h"
#include"GameManager.h"


void main()
{
	_CrtSetDbgFlag(_CRTDBG_LEAK_CHECK_DF | _CRTDBG_ALLOC_MEM_DF);
	// _crtBreakAlloc = 979;
	srand(time(NULL));
	GameManager m_GameManager;
	m_GameManager.GamePlay();
}