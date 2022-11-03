// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

string possible_symbols = "abcdefghijklmnopqrstuvwxyz";
const string Digits = "0123456789";
const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
const string Symbols = "!~`@#$%^&*()_+-=[]{};'\\:\"|,./<>?";

Console.WriteLine("Введите хэш пароля, который хотите взломать");
string pass = Console.ReadLine();

Console.Write("Введите длину пароля, от скольки - до скольки символов, например 3 - 7: ");
string[] password_length = Console.ReadLine().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
int[] password_length_ = Array.ConvertAll(password_length, s => int.Parse(s));
Console.WriteLine(String.Join(",", password_length));

Console.WriteLine("Если пароль содержит только цифры, введите: 1\nЕсли пароль содержит только буквы, введите: 2\n" +
    " Если пароль содержит только цифры и буквы, введите: 3\nЕсли пароль содержит только цифры, буквы и спецсимволы введите: 4 ");

int choice = int.Parse(Console.ReadLine());
if (choice == 1) possible_symbols = Digits;
else if (choice == 2) possible_symbols = Alphabet;
else if (choice == 3) possible_symbols = Digits+ Alphabet;
else if (choice == 4) possible_symbols = Digits + Alphabet+Symbols;

IEnumerable<string> q = possible_symbols.Select(x => x.ToString()); 

for (int i = password_length_[0]; i <= password_length_[1]; i++)
{
    int size = i;
    for (int j = 0; j < size - 1; j++)
    {
         q =q.SelectMany(x => possible_symbols, (x, y) => x + y);
    }
    foreach(string item in q)
    {
        Console.WriteLine("Пробуем пароль: " + item);
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(item));
        if (Convert.ToBase64String(hash) == pass)
        {
            Console.WriteLine("Пароль подобран");
            Console.ReadLine();
            Environment.Exit(0);
        }
        else Console.WriteLine("Пароль не подошел\n");
    }
q = possible_symbols.Select(x => x.ToString());
    
}

