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

## Solution Name: AspDotNet.CRUD.LabExercise

### Project Folders
- `CustomerData`
- `CustomerWeb`

## Create `Models` folder under `CustomerData` project

### Create a `BaseEntity` class under `Models` folder
Import the following namespaces:
```
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
```
Under namespace `CustomerData.Models`, create a public abstract class `BaseEntity`
```
public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
```
### Create a `Customer` class under `Models` folder
Under namespace `CustomerData.Models`, create a public class `Customer`
```
public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
```

## Create `Context` folder under `CustomerData` project
### Create `CustomerDbContext` class under `Context` folder
Import the following namespaces:
```
using CustomerData.Models;
using Microsoft.EntityFrameworkCore;
```
Create a public class `CustomerDbContext` that derives from `DbContext` Class
```
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

```

## Generate Customer Database
Through the `Package Manager Console`, Enter `Add-Migration [Insert any name]` command to create `Migrations` folder.
Then, use the command `Update-Database` to update the Database.

## Create a `Repository` Folder that creates an abstraction layer between the data access layer and the business logic layer of an application.
Create a `GenericRepository` and a `CustomerRepository` class under the `Repository` folder
### Generic Repository
#### Import the following namespaces:
```
using CustomerData.Context;
using CustomerData.Models;
using Microsoft.EntityFrameworkCore;
```
#### Create a public interface `IBaseRepository<T>` where the entity derives from the `BaseEntity` class
```
public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindAll();
        T FindByPrimaryKey(int id);
        T Insert(T entity);
        T Update(T entity);
        T Delete(int id);
    }
```
#### Create public class `GenericRepository<T>` where the entity also derives from the `BaseEntity` class
```
public class GenericRepository<T> where T : BaseEntity
    {
    }
```
#### Under the `GenericRepository<T>` class, create the constructor with the parameter `CustomerDbContext context`
```
public GenericRepository(CustomerDbContext context)
        {
            this.Context = context;
        }
```
#### Create a property `Context` with type `CustomerDbContext`
`public CustomerDbContext Context { get; set; }`
##### Create a the following methods
1. `FindAll()`
Displays all data from the database.
```
public IEnumerable<T> FindAll()
        {
            IQueryable<T> query = this.Context.Set<T>();
            return query.Select(c => c).ToList();
        }
```
2. `FindByPrimaryKid(int id)`
Looks for a data in the database given an id.
```
 public T FindByPrimaryKey(int id)
        {
            var entity = this.Context.Find<T>(id);
            if (entity is object)
            {
                this.Context.Entry<T>(entity).State = EntityState.Detached;
                return entity;
            }
            throw new Exception($"Entity with ID {id} was not found.");
        }
```
3. `Insert(T entity)`
Inserts a data to the database.
```
public T Insert(T entity)
        {
            Context.Add<T>(entity);
            Context.SaveChanges();
            return entity;
        }
```
4. `Update(T entity)`
Updates a data from the database.
```
public T Update(T entity)
        {
            this.Context.Attach<T>(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.SaveChanges();
            return entity;
        }
```
5. `Delete(int id)`
Deletes data from the database.
```
public T Delete(int id)
        {
            var entity = this.FindByPrimaryKey(id);
            this.Context.Remove<T>(entity);
            this.Context.SaveChanges();
            return entity;
        }
```
### Customer Repository
