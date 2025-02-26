using System;
using System.Windows;
using System.Text.RegularExpressions;

namespace PhoneCallCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Очистка сообщений об ошибках и результатах
            txtError.Text = "";
            txtResult.Text = "";

            // Проверка на заполнение полей
            if (string.IsNullOrEmpty(txtDuration.Text) || string.IsNullOrEmpty(txtCostPerMinute.Text))
            {
                txtError.Text = "Заполните все поля!";
                return;
            }

            // Проверка на ввод чисел
            if (!double.TryParse(txtDuration.Text, out double duration) || !double.TryParse(txtCostPerMinute.Text, out double costPerMinute))
            {
                txtError.Text = "Введите числовые значения!";
                return;
            }

            // Проверка выбора дня недели
            if (rbWeekday.IsChecked == false && rbWeekend.IsChecked == false)
            {
                txtError.Text = "Выберите день недели!";
                return;
            }

            // Расчет стоимости
            double totalCost = CalculateCallCost(duration, costPerMinute, rbWeekend.IsChecked == true);

            // Вывод результата
            txtResult.Text = $"Стоимость разговора: {totalCost:F2} руб.";
        }

        private double CalculateCallCost(double duration, double costPerMinute, bool isWeekend)
        {
            double totalCost = duration * costPerMinute;

            // Скидка 15% на выходные
            if (isWeekend)
            {
                totalCost *= 0.85;
            }

            // Скидка 30% на минуты после 30-й
            if (duration > 30)
            {
                double extraMinutes = duration - 30;
                totalCost -= extraMinutes * costPerMinute * 0.3;
            }

            return totalCost;
        }
    }
}