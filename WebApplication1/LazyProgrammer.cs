namespace WebApp
{
    public class LazyProgrammer
    {
        public DateOnly Date { get; set; }

        public int CurrentSalary { get; set; }

        public int ExpectedSalary => 30 + CurrentSalary;

        public string? Excuse { get; set; }
    }
}
