using Microsoft.VisualBasic;
using Tutorial3.Models;

namespace Tutorial3Tests;

public class AdvancedEmpDeptTests
{
    // 11. MAX salary
    // SQL: SELECT MAX(Sal) FROM Emp;
    [Fact]
    public void ShouldReturnMaxSalary()
    {
        var emps = Database.GetEmps();

        decimal? maxSalary = emps.Max(x => x.Sal);
        
        /*maxSalary = (from e in emps select e.Sal).Max();*/

        Assert.Equal(5000, maxSalary);
    }

    // 12. MIN salary in department 30
    // SQL: SELECT MIN(Sal) FROM Emp WHERE DeptNo = 30;
    [Fact]
    public void ShouldReturnMinSalaryInDept30()
    {
        var emps = Database.GetEmps();

        decimal? minSalary = emps.Where(emp => emp.DeptNo == 30).Min(x => x.Sal);

        /*minSalary = (from e in emps where e.DeptNo == 30 select e.Sal).Min();*/

        Assert.Equal(1250, minSalary);
    }

    // 13. Take first 2 employees ordered by hire date
    // SQL: SELECT * FROM Emp ORDER BY HireDate ASC FETCH FIRST 2 ROWS ONLY;
    [Fact]
    public void ShouldReturnFirstTwoHiredEmployees()
    {
        var emps = Database.GetEmps();

        var firstTwo = (from e in emps
                orderby e.HireDate descending
                    select e).Take(2).ToList(); 
        
        /*firstTwo = emps.OrderBy(e => e.HireDate).Take(2).ToList();*/
        
        Assert.Equal(2, firstTwo.Count);
        Assert.True(firstTwo[0].HireDate <= firstTwo[1].HireDate);
    }

    // 14. DISTINCT job titles
    // SQL: SELECT DISTINCT Job FROM Emp;
    [Fact]
    public void ShouldReturnDistinctJobTitles()
    {
        var emps = Database.GetEmps();

        var jobs = (from e in emps
                select e.Job).Distinct().ToList(); 
        
        /*jobs = emps.Select(e => e.Job).Distinct().ToList();*/
        
        Assert.Contains("PRESIDENT", jobs);
        Assert.Contains("SALESMAN", jobs);
    }

    // 15. Employees with managers (NOT NULL Mgr)
    // SQL: SELECT * FROM Emp WHERE Mgr IS NOT NULL;
    [Fact]
    public void ShouldReturnEmployeesWithManagers()
    {
        var emps = Database.GetEmps();

        var withMgr = from e in emps
                    where e.Mgr != null
                    select e; 
        
        /*withMgr = emps.Where(e => e.Mgr != null).ToList();*/
        
        Assert.All(withMgr, e => Assert.NotNull(e.Mgr));
    }

    // 16. All employees earn more than 500
    // SQL: SELECT * FROM Emp WHERE Sal > 500; (simulate all check)
    [Fact]
    public void AllEmployeesShouldEarnMoreThan500()
    {
        var emps = Database.GetEmps();

        var result = (from e in emps
                where e.Sal > 500
                    select e).All(e => e.Sal > 500);

        /*result = emps.All(e => e.Sal > 500);*/
        
        Assert.True(result);
    }

    // 17. Any employee with commission over 400
    // SQL: SELECT * FROM Emp WHERE Comm > 400;
    [Fact]
    public void ShouldFindAnyWithCommissionOver400()
    {
        var emps = Database.GetEmps();

        var result = (from e in emps
                select e).Any(e => e.Comm > 400); 
        
        result = emps.Any(e => e.Comm > 400);
        
        Assert.True(result);
    }

    // 18. Self-join to get employee-manager pairs
    // SQL: SELECT E1.EName AS Emp, E2.EName AS Manager FROM Emp E1 JOIN Emp E2 ON E1.Mgr = E2.EmpNo;
    [Fact]
    public void ShouldReturnEmployeeManagerPairs()
    {
        var emps = Database.GetEmps();

        var result = from emp in emps
                join mgr in emps on emp.Mgr equals mgr.EmpNo
                select new {Employee = emp.EName, Manager = mgr.EName};

        /*result = emps.Join(
            emps,
            emp => emp.Mgr,
            mgr => mgr.EmpNo,
            (emp, mgr) => new { Employee = mgr.EName, Manager = mgr.EName }
        );*/
        
        Assert.Contains(result, r => r.Employee == "SMITH" && r.Manager == "FORD");
    }

    // 19. Let clause usage (sal + comm)
    // SQL: SELECT EName, (Sal + COALESCE(Comm, 0)) AS TotalIncome FROM Emp;
    [Fact]
    public void ShouldReturnTotalIncomeIncludingCommission()
    {
        var emps = Database.GetEmps();

        var result = from e in emps
            let total = e.Sal + (e.Comm ?? 0)
            select new { EName = e.EName, Total = total };

        /*result = emps.Select(
            e => new { EName = e.EName, Total = e.Sal + (e.Comm ?? 0) });*/
        
        Assert.Contains(result, r => r.EName == "ALLEN" && r.Total == 1900);
    }

    // 20. Join all three: Emp → Dept → Salgrade
    // SQL: SELECT E.EName, D.DName, S.Grade FROM Emp E JOIN Dept D ON E.DeptNo = D.DeptNo JOIN Salgrade S ON E.Sal BETWEEN S.Losal AND S.Hisal;
    [Fact]
    public void ShouldJoinEmpDeptSalgrade()
    {
        var emps = Database.GetEmps();
        var depts = Database.GetDepts();
        var grades = Database.GetSalgrades();

        var result = 
                from e in emps
                join d in depts on e.DeptNo equals d.DeptNo
                from s in grades
                where e.Sal >= s.Losal && e.Sal <= s.Hisal
                select new {EName = e.EName, DName = d.DName, Grade = s.Grade}; 
        
        Assert.Contains(result, r => r.EName == "ALLEN" && r.DName == "SALES" && r.Grade == 3);
    }
}
