using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

public class PhoneBookApp
{
    private const string FILE_NAME = "phonebook.xml";

    public static void Main(string[] args)
    {
        PhoneBookApp app = new PhoneBookApp();
        app.Start();
    }

    public void Start()
    {
        Console.WriteLine("Телефонный справочник:");
        Console.WriteLine("1. Поиск");
        Console.WriteLine("2. Добавление");
        Console.WriteLine("3. Изменение");
        Console.WriteLine("4. Удаление");
        Console.WriteLine("5. Выход");

        while (true)
        {
            Console.Write("Выберите действие: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Search();
                    break;
                case 2:
                    Add();
                    break;
                case 3:
                    Update();
                    break;
                case 4:
                    Delete();
                    break;
                case 5:
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    private void Search()
    {
        Console.WriteLine("\nПоиск по:");
        Console.WriteLine("1. Названию организации");
        Console.WriteLine("2. ИНН");
        Console.WriteLine("3. E-mail");
        Console.WriteLine("4. Телефону приемной");
        Console.WriteLine("5. Телефону отдела кадров");
        Console.WriteLine("6. Телефону бухгалтерии");

        Console.Write("Выберите поле для поиска: ");
        int choice = int.Parse(Console.ReadLine());

        Console.Write("Введите значение для поиска: ");
        string searchValue = Console.ReadLine();

        List<Organization> organizations = LoadOrganizations();
        List<Organization> foundOrganizations = organizations
            .Where(o =>
                (choice == 1 && o.Name == searchValue) ||
                (choice == 2 && o.Inn == searchValue) ||
                (choice == 3 && o.Email == searchValue) ||
                (choice == 4 && o.ReceptionPhone == searchValue) ||
                (choice == 5 && o.HrPhone == searchValue) ||
                (choice == 6 && o.AccountingPhone == searchValue)
            )
            .ToList();

        if (foundOrganizations.Count == 0)
        {
            Console.WriteLine("Организация не найдена.");
        }
        else
        {
            foreach (Organization organization in foundOrganizations)
            {
                Console.WriteLine(organization);
            }
        }
    }

    private void Add()
    {
       
    }

    private void Update()
    {
        
    }

    private void Delete()
    {
        
    }

}

public class Organization
{
    public string Name { get; set; }
    public string Inn { get; set; }
    public string Email { get; set; }
    public string ReceptionPhone { get; set; }
    public string HrPhone { get; set; }
    public string AccountingPhone { get; set; }

    public Organization(string name, string inn, string email, string receptionPhone, string hrPhone, string accountingPhone)
    {
        Name = name;
        Inn = inn;
        Email = email;
        ReceptionPhone = receptionPhone;
        HrPhone = hrPhone;
        AccountingPhone = accountingPhone;
    }

    public override string ToString()
    {
        return $"Название: {Name}\nИНН: {Inn}\nE-mail: {Email}\nТелефон приемной: {ReceptionPhone}\nТелефон отдела кадров: {HrPhone}\nТелефон бухгалтерии: {AccountingPhone}";
    }
}