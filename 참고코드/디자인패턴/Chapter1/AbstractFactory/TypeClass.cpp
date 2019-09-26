#include "TypeClass.h"



TypeClass::~TypeClass()
{
}


/* Goblrin */
bool Goblrin::MonsterCheck()
{
	cout << "고블린 등장!" << endl << endl;
	return true;
}

/* Reach */
bool Reach::MonsterCheck()
{
	cout << "리치 등장!" << endl << endl;
	return true;
}