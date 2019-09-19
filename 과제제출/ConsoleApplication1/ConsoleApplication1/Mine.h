#pragma once
#include"Mine.h"
#include"Draw.h"
class Mine
{
private:
	int m_ix;
	int m_iy;
public:
	Mine();
	void SetMineXY(int x, int y);
	bool CheckXY(int x, int y);
	inline int GetX()
	{
		return m_ix;
	}
	inline int GetY()
	{
		return m_iy;
	}
	~Mine();
};

