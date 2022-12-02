using System;
namespace rukzak
{
    class Article //товар
    {
        public string name; //название товара
        public int weight; //вес товара
        public int price; //цена товара
        public bool take; //берем ли товар

        public Article(string n, int w, int p)
        {
            name = n;
            weight = w;
            price = p;
        }
    }

    class Program
    {
        public static Article[] articles;
        public static int numsArticle; // Количество товаров
        public static bool incorrEnter; //Переменная, что проверяет корректность ввода
        public static string name; // Имя товара
        public static int weight, price, maxWeight; // Вес и цена товара, размер рюкзака
        public static int[,] func; //массив для хранения значений функции

        // Метод выводит на экран рюкзак
        static void Print()
        {
            Console.WriteLine("\nМаксимальная стоимость: " + func[maxWeight, numsArticle - 1]);
            Console.Write("Взяты следующие предметы: ");
            foreach (Article x in articles)
                if (x.take)
                    Console.Write(x.name + " ");
        }

        static void Main(string[] args)
        {
            int s, n, i; //просто переменные :)

            Console.WriteLine("Данная программа поможет Вам заполнить Ваш рюкзак максимально ценными товарами");

            //Введем количество товаров
            do
            {
                incorrEnter = false;
                Console.Write("\nКакое количество товаров вы рассматриваете для приобретения: ");
                try
                {
                    numsArticle = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    incorrEnter = true;
                    numsArticle = 0;
                }
            } while (incorrEnter);

            articles = new Article[numsArticle]; //Создаем массив заданного размера

            //Создадим объекты товаров
            for (i = 0; i < articles.Length; i++)
            {
                Console.Write("\n\nВведите название " + i + 1 + "-го товара: ");
                name = Console.ReadLine();
                do
                {
                    incorrEnter = false;
                    Console.Write("Введите вес товара: ");
                    try
                    {
                        weight = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        incorrEnter = true;
                        weight = 0;
                    }
                } while (incorrEnter);
                do
                {
                    incorrEnter = false;
                    Console.Write("Введите цену товара: ");
                    try
                    {
                        price = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        incorrEnter = true;
                        price = 0;
                    }
                } while (incorrEnter);
                articles[i] = new Article(name, weight, price); //Создаем объект
            }

            // Введем размер рюкзака
            do
            {
                incorrEnter = false;
                Console.Write("\nВведите размер вашего рюкзака (контейнера): ");
                try
                {
                    maxWeight = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    incorrEnter = true;
                    maxWeight = 0;
                }
            } while (incorrEnter);

            func = new int[maxWeight + 1, numsArticle]; //Реализуем массив функции

            //Реализуем алгоритм Беллмана
            for (weight = 1; weight <= maxWeight; weight++) // Загружаем рюкзак если его вместимость = Weight
                for (i = 1; i < numsArticle; i++) // берем предметы с 1 по numsArticle
                    //если вес предмета больше Weight, или предыдущий набор лучше выбираемого
                    if (articles[i].weight > weight)
                    {
                        func[weight, i] = func[weight, i - 1]; //тогда берем предыдущий набор
                        articles[i].take = false;
                    }
                    else if (func[weight, i - 1] >= (func[weight - articles[i].weight, i - 1] + articles[i].price))
                    {
                        func[weight, i] = func[weight, i - 1]; //тогда берем предыдущий набор
                        articles[i].take = false;
                    }
                    else
                    {
                        func[weight, i] = func[weight - articles[i].weight, i - 1] + articles[i].price; //иначе добавляем к предыдущему набору текущий предмет
                        articles[i].take = true;
                    }

            Print();

            Console.ReadKey();
        }
    }
}