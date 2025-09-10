namespace EmployeeVacationDB.Models
{
    public class Manager : SalariedEmployee
    {
        public override float VacationDaysPerYear => 30;
    }
}
