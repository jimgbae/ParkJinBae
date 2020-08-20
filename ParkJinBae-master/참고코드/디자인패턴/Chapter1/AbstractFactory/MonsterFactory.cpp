#include "MonsterFactory.h"

MonsterFactory::~MonsterFactory()
{
	types.clear();
}

void MonsterFactory::AddType(TypeClass* _type)
{
	types.push_back(_type);
}

/* Goblrin */

TypeClass* GoblrinMonster::CreateTypeClass()
{
	return new Goblrin;
}

/* Reach */
TypeClass* ReachMonster::CreateTypeClass()
{
	return new Reach;
}