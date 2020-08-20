#include "Mine.h"


Mine::Mine()
{
	m_ix = NULL;
	m_iy = NULL;
}

void Mine::SetMineXY(int x, int y)
{
	m_ix = x;
	m_iy = y;
}

bool Mine::CheckXY(int x, int y)
{
	if (m_ix == x && m_iy == y)
		return false;
	else
		return true;
}


Mine::~Mine()
{
}
