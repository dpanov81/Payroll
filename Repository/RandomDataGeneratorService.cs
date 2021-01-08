using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services
{
    /// <summary>
    /// Сервис создания рандомных данных для файлов.
    /// (Класс создан для удобства тестирования приложения и в ТЗ не входит)
    /// </summary>
    class RandomDataGeneratorService
    {           
        // Список строк для записи в файл.
        private List<string> _listOfFileLines;

        private SortingService _sortSrv; 

        public RandomDataGeneratorService()
        {            
            _listOfFileLines = new List<string>();  
            _sortSrv = new SortingService();
        }

        /// <summary>
        /// Создает список строк сотрудников для файла "Список сотрудников.csv".
        /// </summary>
        /// <returns>Список строк.</returns>
        public List<string> ListOfEmployees()
        {
            _listOfFileLines.Add("Иван,Петров,руководитель");
            _listOfFileLines.Add("Дмитрий,Медведев,руководитель");            
            _listOfFileLines.Add("Иван,Харддисков,сотрудник");
            _listOfFileLines.Add("Андрей,Процессоров,сотрудник");
            _listOfFileLines.Add("Константин,Айбиэмов,сотрудник");
            _listOfFileLines.Add("Сергей,Великолобов,сотрудник");
            _listOfFileLines.Add("Денис,Денисенко,сотрудник");
            _listOfFileLines.Add("Ибрагим,Файулин,сотрудник");
            _listOfFileLines.Add("Акил,Акилов,сотрудник");
            _listOfFileLines.Add("Абхай,Чоудари,фрилансер");
            _listOfFileLines.Add("Нанда,Бурман,фрилансер");
            _listOfFileLines.Add("Прабху,Тагор,фрилансер");
            _listOfFileLines.Add("Санджи,Тхакур,фрилансер");

            return _listOfFileLines;
        }

        /// <summary>
        /// Создает рандомный список строк для файла "Список отработанных часов руководителей.csv"
        /// </summary>
        /// <returns>Список строк.</returns>
        public List<string> ListOfHoursWorkedByLeaders()
        {
            DateTime rndDate = new DateTime();
            RandomDate randomDate = new RandomDate();
            Random rnd = new Random();

            List<string> listLeader = new List<string>();
            listLeader.Add("Иван Петров");
            listLeader.Add("Дмитрий Медведев");

            int workingHours;

            List<string> listTask = new List<string>();

            listTask.Add("Проводил собеседование джуниор разработчиков");
            listTask.Add("Митинг");
            listTask.Add("Проводил собеседование");
            listTask.Add("Ревью кода");
            listTask.Add("Проводил собеседование C# разработчиков");
            listTask.Add("Совещание с архитектором");
            listTask.Add("Совещание в PM");
            listTask.Add("Общение с заказчиков");


            StringBuilder line = new StringBuilder();
            StringBuilder nameAndSurname = new StringBuilder();
            StringBuilder task = new StringBuilder();

            for (int i = 0; i < 100; i++)
            {
                // Рандомная дата (диапазон 1 год от текущей).
                rndDate = randomDate.Next();
                // Рандомное имя и фамилия руководителя из 2-х.
                nameAndSurname.Append(listLeader[rnd.Next(0, 1)]);
                // Рандомное количество отработанных часов за день (диапазон от 1 до 12).
                workingHours = rnd.Next(1, 12);
                // Рандомная задача.
                task.Append(listTask[rnd.Next(0, 7)]);

                line.Append(rndDate.ToShortDateString());
                line.Append(",");
                line.Append(nameAndSurname);
                line.Append(",");
                line.Append(workingHours);
                line.Append(",");
                line.Append(task);

                _listOfFileLines.Add(line.ToString());

                nameAndSurname.Clear();
                task.Clear();
                line.Clear();
            }

            // Сотрируем список по дате, удаляем дублирующие строки(по дате) в списке и возвращаем.
            return RemoveDuplicateLinesByDate(_sortSrv.SortingList(_listOfFileLines));
        }

        /// <summary>
        /// Создает рандомный список строк для файла "Список отработанных часов сотрудников.csv"
        /// </summary>
        /// <returns>Список строк</returns>
        public List<string> ListOfEmployeesWorkedHours()
        {
            DateTime rndDate = new DateTime();
            RandomDate randomDate = new RandomDate();
            Random rnd = new Random();            

            List<string> listEmployees = new List<string>();
            listEmployees.Add("Андрей Процессоров");
            listEmployees.Add("Константин Айбиэмов");
            listEmployees.Add("Сергей Великолобов");
            listEmployees.Add("Денис Денисенко");
            listEmployees.Add("Ибрагим Файулин");
            listEmployees.Add("Акил Акилов");
            listEmployees.Add("Иван Харддисков");

            int workingHours;

            List<string> listTask = new List<string>();

            listTask.Add("Разработка модуля № 1");
            listTask.Add("Разработка модуля № 2");
            listTask.Add("Разработка модуля № 3");
            listTask.Add("Разработка модуля № 4");
            listTask.Add("Разработка модуля № 5");
            listTask.Add("Разработка модуля № 6");
            listTask.Add("Разработка модуля № 7");
            listTask.Add("Разработка модуля № 8");
            listTask.Add("Тестирование модуля № 1");
            listTask.Add("Тестирование модуля № 2");
            listTask.Add("Тестирование модуля № 3");
            listTask.Add("Тестирование модуля № 4");
            listTask.Add("Тестирование модуля № 5");
            listTask.Add("Тестирование модуля № 6");
            listTask.Add("Тестирование модуля № 7");
            listTask.Add("Тестирование модуля № 8");
            listTask.Add("Рефакторинг кода");
            listTask.Add("Разработка UI");


            StringBuilder line = new StringBuilder();
            StringBuilder nameAndSurname = new StringBuilder();
            StringBuilder task = new StringBuilder();

            for (int i = 0; i < 200; i++)
            {
                // Рандомная дата (диапазон 1 год от текущей).
                rndDate = randomDate.Next();
                // Рандомное имя и фамилия сотрудника из семи.
                nameAndSurname.Append(listEmployees[rnd.Next(0, 6)]);
                // Рандомное количество отработанных часов за день (диапазон от 1 до 12).
                workingHours = rnd.Next(1, 12);
                // Рандомная задача.
                task.Append(listTask[rnd.Next(0, 17)]);

                line.Append(rndDate.ToShortDateString());
                line.Append(",");
                line.Append(nameAndSurname);
                line.Append(",");
                line.Append(workingHours);
                line.Append(",");
                line.Append(task);

                _listOfFileLines.Add(line.ToString());

                nameAndSurname.Clear();
                task.Clear();
                line.Clear();
            }

            // Сотрируем список по дате, удаляем дублирующие строки(по дате) в списке и возвращаем.
            return RemoveDuplicateLinesByDate(_sortSrv.SortingList(_listOfFileLines));
        }

        /// <summary>
        /// Создает рандомный список строк для файла "Список отработанных часов внештатных сотрудников.csv"
        /// </summary>
        /// <returns>Список строк</returns>
        public List<string> ListOfFreelancersWorkedHours()
        {
            DateTime rndDate = new DateTime();
            RandomDate randomDate = new RandomDate();
            Random rnd = new Random();           

            // Наши друзья из Индии с Upwork.
            List<string> listFreelancers = new List<string>();
            listFreelancers.Add("Абхай Чоудари");
            listFreelancers.Add("Нанда Бурман");
            listFreelancers.Add("Прабху Тагор");
            listFreelancers.Add("Санджи Тхакур");            

            int workingHours;

            List<string> listTask = new List<string>();

            listTask.Add("Разработка модуля № 9");
            listTask.Add("Разработка модуля № 10");
            listTask.Add("Разработка модуля № 11");
            listTask.Add("Разработка модуля № 12");
            listTask.Add("Разработка модуля № 13");
            listTask.Add("Разработка модуля № 14");            
            listTask.Add("Тестирование модуля № 9");
            listTask.Add("Тестирование модуля № 10");
            listTask.Add("Тестирование модуля № 11");
            listTask.Add("Тестирование модуля № 12");
            listTask.Add("Тестирование модуля № 13");
            listTask.Add("Тестирование модуля № 14");            
            listTask.Add("Рефакторинг кода");
            listTask.Add("Разработка UI");


            StringBuilder line = new StringBuilder();
            StringBuilder nameAndSurname = new StringBuilder();
            StringBuilder task = new StringBuilder();

            for (int i = 0; i < 150; i++)
            {
                // Рандомная дата (диапазон 1 год от текущей).
                rndDate = randomDate.Next();
                // Рандомное имя и фамилия сотрудника из четырех.
                nameAndSurname.Append(listFreelancers[rnd.Next(0, 3)]);
                // Рандомное количество отработанных часов за день (диапазон от 1 до 12).
                workingHours = rnd.Next(1, 12);
                // Рандомная задача.
                task.Append(listTask[rnd.Next(0, 13)]);

                line.Append(rndDate.ToShortDateString());
                line.Append(",");
                line.Append(nameAndSurname);
                line.Append(",");
                line.Append(workingHours);
                line.Append(",");
                line.Append(task);

                _listOfFileLines.Add(line.ToString());

                nameAndSurname.Clear();
                task.Clear();
                line.Clear();
            }

            // Сотрируем список по дате, удаляем дублирующие строки(по дате) в списке и возвращаем.
            return RemoveDuplicateLinesByDate(_sortSrv.SortingList(_listOfFileLines));
        }

        /// <summary>
        /// Удаление дублирующих строк в списке по дате.
        /// </summary>        
        /// <returns>Список строк.</returns>
        private List<string> RemoveDuplicateLinesByDate(List<string> listRandom)
        {
            int nextItem;
            DateTime date = new DateTime();
            DateTime nextDate = new DateTime();
            ReportLine repLine = new ReportLine();            

            for (int i = 0; i < listRandom.Count; i++)
            {
                nextItem = i + 1;

                if (nextItem < listRandom.Count)
                {
                    date = repLine.GetDateFromString(listRandom[i]);
                    nextDate = repLine.GetDateFromString(listRandom[nextItem]);

                    if (date == nextDate)
                    {
                        listRandom.RemoveAt(nextItem);
                        i--;                        
                    }
                }
            }
            return listRandom;
        }
    }
}
