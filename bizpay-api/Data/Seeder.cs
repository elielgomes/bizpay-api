using bizpay_api.Repository;
using Microsoft.EntityFrameworkCore;
using bizpay_api.Shared;

namespace bizpay_api.Data
{
    public static class Seeder
    {

        public static Role RoleAccontingManager { get; set; }
        public static Role RoleDeveloper { get; set; }
        public static Role RoleEngineer { get; set; }
        public static Role RoleSEOSpecialist { get; set; }
        public static Role RoleSocialMediaAnalyst { get; set; }

        public static Permition PermitionAdmin { get; set; }
        public static Permition PermitionUser { get; set; }

        public static Employee EmployeeMarcos { get; set; }
        public static Employee EmployeeMaria { get; set; }
        public static Employee EmployeeAdmin { get; set; }

        public static Payslip PayslipMarcos1 { get; set; }
        public static Payslip PayslipMarcos2 { get; set; }
        public static Payslip PayslipMarcos3 { get; set; }

        public static void SeedData(APIDbContext dbContext)
        {

            #region "ROLE"
            // CREATE ROLE
            if (!dbContext.Roles.Any())
            {

                RoleAccontingManager = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Gestor de contabilidade",
                    WeeklyWorkload = 40,
                };

                RoleDeveloper = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Desenvolvedor de software",
                    WeeklyWorkload = 40,
                };

                RoleEngineer = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Engenheiro de software",
                    WeeklyWorkload = 40,
                };

                RoleSEOSpecialist = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Especialista em SEO",
                    WeeklyWorkload = 40,
                };

                RoleSocialMediaAnalyst = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Analista de Mídias Sociais",
                };

                dbContext.Roles.AddRange(
                    RoleDeveloper,
                    RoleEngineer,
                    RoleSEOSpecialist,
                    RoleSocialMediaAnalyst,
                    RoleAccontingManager);
                dbContext.SaveChanges();
            }
            else
            {
                RoleDeveloper = dbContext.Roles.Single(r => r.Name == "Desenvolvedor de software");
                RoleEngineer = dbContext.Roles.Single(r => r.Name == "Engenheiro de software");
                RoleSEOSpecialist = dbContext.Roles.Single(r => r.Name == "Especialista em SEO");
                RoleSocialMediaAnalyst = dbContext.Roles.Single(r => r.Name == "Analista de Mídias Sociais");
                RoleAccontingManager = dbContext.Roles.Single(r => r.Name == "Gestor de contabilidade");
            }
            #endregion

            #region "PERMITION"
            // CREATE PERMITION
            if (!dbContext.Permitions.Any())
            {
                PermitionAdmin = new Permition
                {
                    Id = EmployeePermitions.Admin,
                };

                PermitionUser = new Permition
                {
                    Id = EmployeePermitions.User,
                };

                dbContext.Permitions.AddRange(PermitionAdmin, PermitionUser);
                dbContext.SaveChanges();
            }
            else
            {
                PermitionAdmin = dbContext.Permitions.Single(p => p.Id == EmployeePermitions.Admin);
                PermitionUser = dbContext.Permitions.Single(p => p.Id == EmployeePermitions.User);
            }
            #endregion

            #region "EMPLOYEE"
            // CREATE EMPLOYEE
            if (!dbContext.Employees.Any())
            {
                EmployeeMarcos = new Employee
                {
                    Cpf = "123.456.789-00",
                    Name = "Marcos Silva",
                    Status = Status.Active,
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Email = "marcos.silva@example.com",
                    PhoneNumber = "1630000000",
                    CellNumber = "16990000000",
                    Address = "Rua alvaro de cabral, 123 - 14000000",
                    Sex = HumanSexCodes.Male,
                    Password = "password123",
                    HourlyPayment = 20m,
                    AdmissionDate = new DateTime(2010, 7, 1),
                    TerminationDate = null,
                    Rg = "405677892",
                    Nationality = "Brasileira(o)",
                    MaritalStatus = MaritalStatus.Married,
                    NumberOfChildren = 2,
                    BankName = "ABC Bank",
                    AccountNumber = "123456789",
                    AgencyNumber = "7890-1",
                    PixKey = "marcos.silva@pix.example.com",
                    PermitionId = PermitionUser.Id,
                    RoleId = RoleDeveloper.Id,
                };

                EmployeeMaria = new Employee
                {
                    Cpf = "987.654.321-00",
                    Name = "Maria Santos",
                    Status = Status.Active,
                    DateOfBirth = new DateTime(1990, 8, 20),
                    Email = "maria.santos@example.com",
                    PhoneNumber = "1630000000",
                    CellNumber = "16990000000",
                    Address = "Rua parque das andorinhas - 123",
                    Sex = HumanSexCodes.Female,
                    Password = "password123",
                    HourlyPayment = 30.75m,
                    AdmissionDate = new DateTime(2012, 4, 5),
                    TerminationDate = null,
                    Rg = "091003827",
                    Nationality = "Brasileira(o)",
                    MaritalStatus = MaritalStatus.Single,
                    NumberOfChildren = 0,
                    BankName = "XYZ Bank",
                    AccountNumber = "987654321",
                    AgencyNumber = "5432-1",
                    PixKey = "maria.santos@pix.example.com",
                    PermitionId = EmployeePermitions.User,
                    RoleId = RoleEngineer.Id,
                };

                EmployeeAdmin = new Employee
                {
                    Cpf = "421.864.121-00",
                    Name = "Admin",
                    Status = Status.Active,
                    DateOfBirth = new DateTime(1990, 8, 20),
                    Email = "admin@admin.com",
                    PhoneNumber = "1630000000",
                    CellNumber = "16990000000",
                    Address = "Av domingos vila lobos - 123",
                    Sex = HumanSexCodes.Male,
                    Password = "admin123",
                    HourlyPayment = 30.75m,
                    AdmissionDate = new DateTime(2012, 4, 5),
                    TerminationDate = null,
                    Rg = "091003827",
                    Nationality = "Brasileira(o)",
                    MaritalStatus = MaritalStatus.Single,
                    NumberOfChildren = 0,
                    BankName = "XYZ Bank",
                    AccountNumber = "987654321",
                    AgencyNumber = "5432-1",
                    PixKey = "admin@admin.com",
                    PermitionId = EmployeePermitions.Admin,
                    RoleId = RoleAccontingManager.Id,
                };

                dbContext.Employees.AddRange(EmployeeMarcos, EmployeeMaria, EmployeeAdmin);
                dbContext.SaveChanges();
            }
            else
            {
                EmployeeMarcos = dbContext.Employees.Single(e => e.Cpf == "123.456.789-00");
                EmployeeMaria = dbContext.Employees.Single(e => e.Cpf == "987.654.321-00");
                EmployeeAdmin = dbContext.Employees.Single(e => e.Cpf == "421.864.121-00"); ;
            }
            #endregion

            #region "PAYSLIP"
            //CREATE PAYSLIP  
            if (!dbContext.Payslips.Any())
            {
                PayslipMarcos3 = new Payslip
                {
                    Id = Guid.NewGuid(),
                    DateOfIssue = DateTime.Now.AddMonths(-3), // Data de emissão 3 meses atrás
                    GrossSalary = 4500.00m,
                    NetSalary = 3800.00m,
                    Discounts = 700.00m,
                    Bonus = 200.00m,
                    Inss = 350.00m,
                    Irrf = 50.00m,
                    EmployeeCpf = EmployeeMarcos.Cpf,
                };

                PayslipMarcos2 = new Payslip
                {
                    Id = Guid.NewGuid(),
                    DateOfIssue = DateTime.Now.AddMonths(-2),
                    GrossSalary = 4500.00m,
                    NetSalary = 3800.00m,
                    Discounts = 700.00m,
                    Bonus = 200.00m,
                    Inss = 350.00m,
                    Irrf = 50.00m,
                    EmployeeCpf = EmployeeMarcos.Cpf,
                };

                PayslipMarcos1 = new Payslip
                {
                    Id = Guid.NewGuid(),
                    DateOfIssue = DateTime.Now.AddMonths(-1),
                    GrossSalary = 4500.00m,
                    NetSalary = 3800.00m,
                    Discounts = 700.00m,
                    Bonus = 200.00m,
                    Inss = 350.00m,
                    Irrf = 50.00m,
                    EmployeeCpf = EmployeeMarcos.Cpf,
                };

                dbContext.Payslips.AddRange(PayslipMarcos3, PayslipMarcos2, PayslipMarcos1);
                dbContext.SaveChanges();
            }
            else
            {
                var listPayslip = dbContext.Payslips
                    .Where(e => e.EmployeeCpf == EmployeeMarcos.Cpf)
                    .OrderByDescending(d => d.DateOfIssue);

                PayslipMarcos1 = listPayslip.ToArray()[0];
                PayslipMarcos2 = listPayslip.ToArray()[1];
                PayslipMarcos3 = listPayslip.ToArray()[2];
            }
            #endregion
        }
    }

}
