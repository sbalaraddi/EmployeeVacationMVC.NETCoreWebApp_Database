namespace EmployeeVacationDB.Models
{
    public class SalariedEmployee : Employee
    {
        public override float VacationDaysPerYear => 15;
    }
}
