using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace lab1.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private DateTime? _birthDate;
        public DateTime? BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); }
        }

        private string _ageText;
        public string AgeText
        {
            get => _ageText;
            set { _ageText = value; OnPropertyChanged(); }
        }

        private string _westernZodiacText;
        public string WesternZodiacText
        {
            get => _westernZodiacText;
            set { _westernZodiacText = value; OnPropertyChanged(); }
        }

        private string _chineseZodiacText;
        public string ChineseZodiacText
        {
            get => _chineseZodiacText;
            set { _chineseZodiacText = value; OnPropertyChanged(); }
        }

        public ICommand CalculateCommand { get; }

        public MainWindowViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
        }

        private void Calculate(object parameter)
        {
            if (BirthDate == null)
            {
                MessageBox.Show("Будь ласка, введіть дату народження.");
                return;
            }

            DateTime today = DateTime.Today;
            DateTime birth = BirthDate.Value;

            if (birth > today)
            {
                MessageBox.Show("Дата народження не може бути в майбутньому.");
                return;
            }
            int age = today.Year - birth.Year;
            if (birth > today.AddYears(-age))
                age--;

            if (age < 0 || age > 135)
            {
                MessageBox.Show("Вік користувача вказано некоректно.");
                return;
            }

            AgeText = $"Ваш вік: {age} років";

            if (birth.Month == today.Month && birth.Day == today.Day)
            {
                MessageBox.Show("З Днем Народження! Бажаємо гарного настрою!");
            }

            WesternZodiacText = $"Західний знак: {GetWesternZodiac(birth)}";

            ChineseZodiacText = $"Китайський знак: {GetChineseZodiac(birth.Year)}";
        }

        private string GetWesternZodiac(DateTime birth)
        {
            int month = birth.Month;
            int day = birth.Day;

            if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
                return "Овен";
            if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
                return "Телець";
            if ((month == 5 && day >= 21) || (month == 6 && day <= 20))
                return "Близнюки";
            if ((month == 6 && day >= 21) || (month == 7 && day <= 22))
                return "Рак";
            if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
                return "Лев";
            if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
                return "Діва";
            if ((month == 9 && day >= 23) || (month == 10 && day <= 22))
                return "Терези";
            if ((month == 10 && day >= 23) || (month == 11 && day <= 21))
                return "Скорпіон";
            if ((month == 11 && day >= 22) || (month == 12 && day <= 21))
                return "Стрілець";
            if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
                return "Козеріг";
            if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
                return "Водолій";
            if ((month == 2 && day >= 19) || (month == 3 && day <= 20))
                return "Риби";

            return "Невідомо";
        }

        private string GetChineseZodiac(int year)
        {
            string[] zodiacAnimals = new string[]
            {
                "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія",
                "Кінь", "Коза", "Мавпа", "Півень", "Собака", "Свиня"
            };
            int index = (year - 4) % 12;
            return zodiacAnimals[index];
        }
    }
}
