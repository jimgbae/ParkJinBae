#include "Inventory.h"
#include<Windows.h>

void main()
{
	Inventory* Equip;
	Inventory* Noitem = new Item("����");
	Inventory* inventory = new Bag("Main Bag");

	Inventory* item1 = new Item("��");
	Inventory* item2 = new Item("Ȱ");
	Inventory* item3 = new Item("������");

	Inventory* bag1 = new Bag("�ֹ��� ����");
	Inventory* item4 = new Item("���̾");
	Inventory* item5 = new Item("�����Ʈ");

	Inventory* bag2 = new Bag("ȭ�� ����");
	Inventory* item6 = new Item("�� ȭ��");
	Inventory* item7 = new Item("�� ȭ��");

	Inventory* item8 = new Item("��");

	Inventory* bag3 = new Bag("���� ����");
	Inventory* item9 = new Item("������");
	Inventory* item10 = new Item("����");
	Inventory* item11 = new Item("������");

	Equip = Noitem;
	inventory->AddInventory(Noitem);
	inventory->AddInventory(item1);
	inventory->AddInventory(item2);
	inventory->AddInventory(item3);

	bag1->AddInventory(item4);
	bag1->AddInventory(item5);
	inventory->AddInventory(bag1);

	bag2->AddInventory(item6);
	bag2->AddInventory(item7);
	inventory->AddInventory(bag2);

	inventory->AddInventory(item8);

	inventory->AddInventory(bag3);
	bag3->AddInventory(item9);
	bag3->AddInventory(item10);
	bag3->AddInventory(item11);

	int iSelect, ichose, itemcount = 12;
	string str, stritem;
	stritem = "item" + to_string(itemcount++);
	char buf[256];
	bool bbool = false;
	while (!bbool)
	{
		system("cls");
		inventory->View();
		cout << "������ ������ : ";
		cout << setfill(' ') << Equip->GetName().c_str() << " - Item" << endl;
		cout << "1. ������ �߰� 2.������" << endl << endl;
		cin >> iSelect;
		switch (iSelect)
		{
		case 1:
			cin >> str;
			Inventory* stritem = new Item(str);
			break;
		case 2:
			bbool = true;
			break;
		}
	}

	delete inventory;
}