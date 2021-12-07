namespace archi_company_mvc.Models
{
    public class Secretary : Employee
    {
        public override string GetDefaultAction()
        {
            return "Index";
        }

        public override string GetController()
        {
            return "Secretaries";
        }
    }
}