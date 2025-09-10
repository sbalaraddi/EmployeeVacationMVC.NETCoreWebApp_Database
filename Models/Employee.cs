using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeVacationDB.Models
{
    public abstract class Employee
    {
        [Key]
        public int Id {  get; set; }

        public string Name { get; set; }

        public int TotalDaysWorked { get; set; }
        public float VacationDaysUsed { get; set; }

        private const int WorkYearDays = 260;

        [NotMapped]
        public abstract float VacationDaysPerYear { get; }

        [NotMapped]
        public float VacationDaysAccumulated =>
            ((TotalDaysWorked / (float)WorkYearDays) * VacationDaysPerYear) - VacationDaysUsed;

        public void Work(int days)
        {
            if (days < 0 || TotalDaysWorked + days > WorkYearDays)
                throw new ArgumentException("Invalid number of work days");
            TotalDaysWorked += days;
        }

        public void TakeVacation(float days)
        {
            if (days < 0 || days > VacationDaysAccumulated)
                throw new ArgumentException("Not enough vacation days");
            VacationDaysUsed += days;
        }

        public void NewCalender()
        {
            TotalDaysWorked = 0;
            VacationDaysUsed = 0;
        }
    }
}
