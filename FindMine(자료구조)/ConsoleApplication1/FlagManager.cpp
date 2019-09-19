#include "FlagManager.h"


FlagManager::FlagManager()
{
	m_pFlag = NULL;
	m_iFlagList = NULL;
	m_pSaveFlag = NULL;
	m_iPlayingFlag = 0;
	m_bCheckFlag = true;
}

void FlagManager::SettingFlagList(int FlagList)
{
	m_iFlagList = FlagList;
}

void FlagManager::DrawFlag()
{
	if (m_pFlag == NULL)
		return;
	else
		CheckFlag(&m_pFlag);
}

void FlagManager::CheckFlag(Flag** tmp)
{
	if ((*tmp)->Link != NULL)
		CheckFlag(&(*tmp)->Link);
	m_DrawManager.DrawFlag((*tmp)->ix, (*tmp)->iy);
}

void FlagManager::MakeFlag(int ix, int iy)
{
	if (m_iPlayingFlag < m_iFlagList)
	{
		Flag* tmp = new Flag;
		if (m_pFlag == NULL)
		{
			tmp->ix = ix;
			tmp->iy = iy;
			tmp->Link = NULL;
			m_pFlag = tmp;
			m_iPlayingFlag++;
		}
		else
		{
			Flag* Ctmp = m_pFlag;
			while (Ctmp->Link != NULL)
				Ctmp = Ctmp->Link;
			tmp->ix = ix;
			tmp->iy = iy;
			Ctmp->Link = tmp;
			m_iPlayingFlag++;
		}
	}
}

void FlagManager::SettingFlag(int ix, int iy)
{
	SearchFlag(&m_pFlag, ix, iy);
	if (!m_bCheckFlag)
		ResetFlag();
	else
		MakeFlag(ix, iy);
}

void FlagManager::SearchFlag(Flag** tmp, int x, int y)
{
	if (m_pFlag == NULL)
		return;
	else
	{
		if ((*tmp)->ix == x && (*tmp)->iy == y)
		{
			m_pSaveFlag = tmp;
			m_bCheckFlag = false;
			return;
		}
		if ((*tmp)->Link != NULL)
			SearchFlag(&(*tmp)->Link, x, y);
	}
}

void FlagManager::ResetFlag()
{
	if (m_pSaveFlag == NULL)
		return;
	else
	{
		Flag* tmp = *m_pSaveFlag;
		if ((*m_pSaveFlag)->Link == NULL)
		{
			*m_pSaveFlag = tmp->Link;
			delete tmp;
			tmp = NULL;
		}
		else
		{
			*m_pSaveFlag = tmp->Link;
			delete tmp;
			tmp = NULL;
		}
		m_iPlayingFlag--;
		m_bCheckFlag = true;
	}
}

void FlagManager::DeleteFlag()
{
	if (m_pFlag == NULL)
		return;
	else
		Delete(&m_pFlag);
	m_iPlayingFlag = 0;
}

void FlagManager::Delete(Flag** tmp)
{
	if ((*tmp)->Link != NULL)
		Delete(&(*tmp)->Link);
	delete *tmp;
	*tmp = NULL;
}


FlagManager::~FlagManager()
{
}
