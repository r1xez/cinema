using System;

namespace cinema
{
    // Клас Cashier представляє касира, який працює в кінотеатрі
    // Успадковує базові поля та методи від класу Staff
    public class Cashier : Staff
    {
        // Кількість оброблених транзакцій касиром
        public int TransactionsHandled { get; set; }

        // Кількість грошей у касі на даний момент
        public decimal CashInDrawer { get; set; }

        // ID терміналу, з яким працює касир
        public string TerminalId { get; set; }

        // Дозволено касиру робити повернення грошей чи ні
        public bool CanRefund { get; set; }

        // Лічильник кількості змін, які відпрацював касир
        public int ShiftCounter { get; set; }

        // Номер каси (тилла), на якій працює касир
        public string TillNumber { get; set; }

        // Кількість продажів за сьогодні
        public int SalesToday { get; set; }

        // Чи залогінений касир у систему
        public bool IsLoggedIn { get; set; }

        // Badge ID касира (ідентифікатор працівника)
        public string BadgeId { get; set; }

        // Добовий фінансовий план касира
        public decimal DailyTarget { get; set; }

        // Конструктор: встановлює роль працівника як "Cashier"
        public Cashier() { Role = "Cashier"; }

        // Відкриває касу із початковою сумою і починає зміну
        public void OpenTill(decimal startingCash)
        {
            CashInDrawer = startingCash; // Встановлюємо початковий залишок у касі
            IsLoggedIn = true; // Касир розпочинає роботу
            ShiftCounter++; // Зміна рахується як відпрацьована
        }

        // Закриває касу після завершення роботи
        public void CloseTill()
        {
            IsLoggedIn = false; // Касир виходить із системи
            CashInDrawer = 0; // Каса обнуляється
        }

        // Обробка платежу клієнта
        public bool ProcessPayment(Payment p)
        {
            if (p == null) return false; // Перевірка на null
            if (p.Process()) // Викликає метод Process() у платежі
            {
                TransactionsHandled++; // Збільшуємо кількість операцій
                CashInDrawer += p.Amount - p.CalculateFee(); // Додаємо в касу суму за мінусом комісії
                SalesToday++; // Додаємо одну продажу
                return true;
            }
            return false;
        }

        // Обробка повернення коштів
        public bool RefundPayment(Payment p)
        {
            if (!CanRefund || p == null) return false; // Якщо повернення заборонено або платежу нема
            if (p.Refund()) // Виконати повернення через метод Refund()
            {
                CashInDrawer -= p.Amount; // Зменшити гроші в касі
                return true;
            }
            return false;
        }

        // Додає певну кількість продажів
        public void IncrementSales(int count) { SalesToday += count; }
    }
}
