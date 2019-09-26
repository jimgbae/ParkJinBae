#pragma once
#include<vector>
#include"TypeClass.h"

class MonsterFactory
{
	vector<TypeClass*> types;

public:
	~MonsterFactory();

	virtual TypeClass* CreateTypeClass() = 0;


protected:
	void AddType(TypeClass* type);
};

class GoblrinMonster : public MonsterFactory
{
public:
	virtual TypeClass* CreateTypeClass();
};

class ReachMonster : public MonsterFactory
{
public:
	virtual TypeClass* CreateTypeClass();
};
