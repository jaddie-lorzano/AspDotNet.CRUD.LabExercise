# AspDotNet.CRUD.LabExercise

## Task
Create an **ASP .NET** App that enables CRUD Operation for the **Customer Database**.
### `Customer` Entity Code
```
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}
```

### Solution Name: AspDotNet.CRUD.LabExercise

### Project Folders
- `CustomerData`
- `CustomerWeb`

### Create `Models` folder under `CustomerData` project
create a `Customer` class under `Models` folder
```
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerData.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}

```

### Create `Context` folder under `CustomerData` project
Create `CustomerDbContext` class under `Context` folder
```
using CustomerData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerData.Context
{
    public class CustomerDbContext : DbContext
    {
        private readonly string _connectionString;
        public CustomerDbContext() : this(
            "Server = DESKTOP-47CLFBT; " +
            "Database = CustomerDb; " +
            "User Id = sa; " +
            "Password = D@gisik@n1997*")
        {

        }
        public CustomerDbContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._connectionString);
            }
        }
    }
}

```

### Generate Customer Database
Through the `Package Manager Console`, Enter `Add-Migration [Insert any name]` command to create `Migrations` folder. Then, use the command `Update-Database` to update the Database.
