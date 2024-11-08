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
        Console.WriteLine("\nДобавление новой организации:");
        Console.Write("Введите название организации: ");
        string name = Console.ReadLine();
        Console.Write("Введите ИНН: ");
        string inn = Console.ReadLine();
        Console.Write("Введите E-mail: ");
        string email = Console.ReadLine();
        Console.Write("Введите телефон приемной: ");
        string receptionPhone = Console.ReadLine();
        Console.Write("Введите телефон отдела кадров: ");
        string hrPhone = Console.ReadLine();
        Console.Write("Введите телефон бухгалтерии: ");
        string accountingPhone = Console.ReadLine();

        Organization newOrganization = new Organization(name, inn, email, receptionPhone, hrPhone, accountingPhone);
        List<Organization> organizations = LoadOrganizations();
        organizations.Add(newOrganization);
        SaveOrganizations(organizations);
        Console.WriteLine("Организация добавлена.");
    }

    private void Update()
    {

        Console.WriteLine("\nИзменение информации об организации:");
        Console.Write("Введите ИНН организации для изменения: ");
        string inn = Console.ReadLine();

        List<Organization> organizations = LoadOrganizations();
        Organization organizationToUpdate = organizations.FirstOrDefault(o => o.Inn == inn);

        if (organizationToUpdate != null)
        {
            Console.WriteLine(organizationToUpdate);
            Console.WriteLine("\nВведите новое значение для изменения:");
            Console.WriteLine("1. Название организации");
            Console.WriteLine("2. E-mail");
            Console.WriteLine("3. Телефон приемной");
            Console.WriteLine("4. Телефон отдела кадров");
            Console.WriteLine("5. Телефон бухгалтерии");

            Console.Write("Выберите поле для изменения: ");
            int choice = int.Parse(Console.ReadLine());

            Console.Write("Введите новое значение: ");
            string newValue = Console.ReadLine();

            switch (choice)
            {
                case 1:
                    organizationToUpdate.Name = newValue;
                    break;
                case 2:
                    organizationToUpdate.Email = newValue;
                    break;
                case 3:
                    organizationToUpdate.ReceptionPhone = newValue;
                    break;
                case 4:
                    organizationToUpdate.HrPhone = newValue;
                    break;
                case 5:
                    organizationToUpdate.AccountingPhone = newValue;
                    break;
            }

            SaveOrganizations(organizations);
            Console.WriteLine("Информация об организации обновлена.");
        }
        else
        {
            Console.WriteLine("Организация не найдена.");
        }
    }

    private void Delete()
    {
        Console.WriteLine("\nУдаление организации:");
        Console.Write("Введите ИНН организации для удаления: ");
        string inn = Console.ReadLine();

        List<Organization> organizations = LoadOrganizations();
        List<Organization> updatedOrganizations = organizations.Where(o => o.Inn != inn).ToList();

        if (updatedOrganizations.Count < organizations.Count)
        {
            SaveOrganizations(updatedOrganizations);
            Console.WriteLine("Организация удалена.");
        }
        else
        {
            Console.WriteLine("Организация не найдена.");
        }
    }

    private List<Organization> LoadOrganizations()
    {
        List<Organization> organizations = new List<Organization>();

        if (!File.Exists(FILE_NAME))
        {
            return organizations;
        }

        XmlDocument doc = new XmlDocument();
        doc.Load(FILE_NAME);

        XmlNodeList organizationNodes = doc.SelectNodes("/phonebook/organization");

        foreach (XmlNode organizationNode in organizationNodes)
        {
            string name = organizationNode.SelectSingleNode("name").InnerText;
            string inn = organizationNode.SelectSingleNode("inn").InnerText;
            string email = organizationNode.SelectSingleNode("email").InnerText;
            string receptionPhone = organizationNode.SelectSingleNode("reception_phone").InnerText;
            string hrPhone = organizationNode.SelectSingleNode("hr_phone").InnerText;
            string accountingPhone = organizationNode.SelectSingleNode("accounting_phone").InnerText;

            organizations.Add(new Organization(name, inn, email, receptionPhone, hrPhone, accountingPhone));
        }

        return organizations;
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