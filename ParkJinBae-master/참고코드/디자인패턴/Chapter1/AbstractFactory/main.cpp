#include "CharacterFactory.h"
#include "MonsterFactory.h"

void AttackTest(JobClass* job);
void SohwanTest(TypeClass* type);

int main()
{
	KnightCharacter* knightFactory = new KnightCharacter();
	ArcherCharacter* archerFactory = new ArcherCharacter();
	WizardCharacter* wizardFactory = new WizardCharacter();
	ThiefCharacter* thiefFactory = new ThiefCharacter();

	GoblrinMonster* goblrinFactory = new GoblrinMonster();
	ReachMonster* reachFactory = new ReachMonster();

	JobClass* knight = knightFactory->CreateJobClass();
	JobClass* archer = archerFactory->CreateJobClass();
	JobClass* wizard = wizardFactory->CreateJobClass();
	JobClass* thief = thiefFactory->CreateJobClass();

	TypeClass* goblrin = goblrinFactory->CreateTypeClass();
	TypeClass* reach = reachFactory->CreateTypeClass();

	Weapon* sword = knightFactory->CreateWeapon();
	Weapon* bow = archerFactory->CreateWeapon();
	Weapon* staff = wizardFactory->CreateWeapon();
	thiefFactory->CreateWeapon();

	knight->SetWeapon(sword);
	archer->SetWeapon(bow);
	wizard->SetWeapon(staff);
	thief->SetWeapon(sword);

	SohwanTest(goblrin);
	SohwanTest(reach);

	AttackTest(knight);
	AttackTest(archer);
	AttackTest(wizard);
	AttackTest(thief);
}

void AttackTest(JobClass* job)
{
	if (!job->EquipCheck())
		cout << "무기 사용 실패!" << endl;
}

void SohwanTest(TypeClass* type)
{
	if (!type->MonsterCheck())
		cout << "소환 실패!" << endl;
}