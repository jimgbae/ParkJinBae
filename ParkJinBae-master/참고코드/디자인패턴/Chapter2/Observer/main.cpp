#include "Alarm.h"

int main()
{
	BossDragon* dragon = new BossDragon();
	BossTwinHeadOrc* orc = new BossTwinHeadOrc();
	BossQueenCobra* cobra = new BossQueenCobra();

	Player* player1 = new Player("�ٷ�");
	Player* player2 = new Player("���");
	Player* player3 = new Player("�Ͼ�");

	//������ü�� ������ü�� ����Ѵ�.
	player1->SetBossAlarm(dragon);
	player2->SetBossAlarm(orc, false);
	player3->SetBossAlarm(orc);

	//������ü�� ������ü���� ���º�ȭ�� �˷��ش�.
	dragon->SpawnBoss();
	orc->SpawnBoss();

	//������ü�� ������ü���� ���¸� ��û�Ѵ�.
	player2->GetBossState();

	player1->SetBossAlarm(cobra);
	cobra->SpawnBoss();
	player1->SetBossAlarm(cobra, false);

	delete dragon;
	delete orc;
	delete cobra;

	delete player1;
	delete player2;
	delete player3;
}