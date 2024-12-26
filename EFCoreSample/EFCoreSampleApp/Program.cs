using System.Diagnostics;
using System.Runtime.InteropServices;
using EFCoreSampleApp.Data;
using EFCoreSampleApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSampleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await CorrectUpdateOperations();
        }

        private static async Task CorrectUpdateOperations()
        {
            using (var dbContext = new BookStoreContext())
            {
                var client = await dbContext.Clients.FirstOrDefaultAsync(c => c.Name.Equals("John Doe 1"));
                if (client != null)
                {
                    client.Email = "alice@example.com";
                    await dbContext.SaveChangesAsync();
                }

                
                var client2 = new Client()
                {
                    Id = client.Id,
                    Email = "bob@example.com",
                    Name = client.Name
                };
                
                //client has been received as parameter
                //potentially bad practice
                dbContext.Clients.Update(client2);//add if not exist
                await dbContext.SaveChangesAsync();

            }
        }
        
        private static async Task ReadOnlyData()
        {
            //use only in readonly cases
            using (var dbContext = new BookStoreContext())
            {
                var johnsArray = await dbContext.Clients
                    .Include(client => client.Addresses)
                    .AsNoTracking()
                    .Where(client => client.Name.Contains("John"))
                    .ToArrayAsync();

                var emails = johnsArray.Select(client => client.Email).ToArray();

                foreach (var email in emails)
                {
                    Console.WriteLine(email);
                }
            }

        }

        private static async Task EntityStates()
        {
            //await IQueryableExecutionSample();

            using (var dbContext = new BookStoreContext())
            {
                var anyJohn = await dbContext.Clients
                    .FirstOrDefaultAsync(client
                        => client.Name.Contains("John"));
                var bob = new Client()
                {
                    Id = Guid.NewGuid(),
                    Email = "example-new-client@example.com",
                    Name = "Bob"
                };
                var entry = await dbContext.Clients.AddAsync(bob);

                var newEntry = dbContext.Remove(bob);

                //bob.Name = "Alice";
                //var x = dbContext.Entry(bob);


                //await dbContext.Clients.AddAsync(newClient2);


                //var state = EntityState.
                await dbContext.SaveChangesAsync();

            }
        }

        static async Task UserOperations()
        {
            //Create
            using (var dbContext = new BookStoreContext())
            {
                var clients = new List<Client>()
                {
                    new Client { Id = Guid.NewGuid(), Name = "John Doe 1", Email = "johndoe@example.com" },
                    new Client { Id = Guid.NewGuid(), Name = "Vasiliy Pupkin", Email = "vpupkin@example.com" },
                    new Client { Id = Guid.NewGuid(), Name = "John Doe 2", Email = "johndoe@example.com" },
                    new Client { Id = Guid.NewGuid(), Name = "John Doe 3", Email = "johndoe@example.com" },
                    new Client { Id = Guid.NewGuid(), Name = "John Doe 4", Email = "johndoe@example.com" },
                };

                // generate sql (not really)
                await dbContext.Clients.AddRangeAsync(clients);

                //execute sql(not really)
                await dbContext.SaveChangesAsync();
            }
            //Read
            using (var dbContext = new BookStoreContext())
            {
                var vpupkin = await dbContext.Clients.FirstOrDefaultAsync(cl => cl.Name.Contains("Pupkin"));
                if (vpupkin != null)
                {
                    Console.WriteLine($"{vpupkin.Id} - {vpupkin.Name}");
                }
            }

            //Update
            using (var dbContext = new BookStoreContext())
            {
                var vpupkin = await dbContext.Clients.FirstOrDefaultAsync(cl => cl.Name.Contains("Pupkin"));
                if (vpupkin != null)
                {
                    vpupkin.Name = "Peter Pupkin";

                    await dbContext.SaveChangesAsync();

                    var vPupkinUpdated = await dbContext.Clients.FirstOrDefaultAsync(cl => cl.Name.Contains("Pupkin"));

                    Console.WriteLine($"{vPupkinUpdated?.Id} - {vPupkinUpdated?.Name}");
                }
            }

            //Delete
            using (var dbContext = new BookStoreContext())
            {
                var vpupkin = await dbContext.Clients.FirstOrDefaultAsync(cl => cl.Name.Contains("Pupkin"));
                if (vpupkin != null)
                {
                    dbContext.Clients.Remove(vpupkin);
                    await dbContext.SaveChangesAsync();

                    var clients = await dbContext.Clients.ToListAsync();
                    foreach (var client in clients)
                    {
                        Console.WriteLine($"{client.Id} - {client.Name}");
                    }
                }
            }
        }

        static async Task AddressOperations()
        {
            using (var db = new BookStoreContext())
            {
                //add to every address element during init this client ID ED2534D0-8E98-4EDC-AD3E-4BC53488EFCD

                var addresses = new List<Address>()
                {
                    new Address
                    {
                        Id = Guid.NewGuid(),
                        Street = "123 Main St",
                        City = "New York",
                        ZipCode = "10001",
                        ClientId = new Guid("ED2534D0-8E98-4EDC-AD3E-4BC53488EFCD")

                    },
                    new Address
                    {
                        Id = Guid.NewGuid(),
                        Street = "456 Elm St",
                        City = "Los Angeles",
                        ZipCode = "90001",
                        ClientId = new Guid("ED2534D0-8E98-4EDC-AD3E-4BC53488EFCD")
                    },
                    new Address
                    {
                        Id = Guid.NewGuid(),
                        Street = "789 Oak St",
                        City = "Chicago",
                        ZipCode = "60007",
                        ClientId = new Guid("ED2534D0-8E98-4EDC-AD3E-4BC53488EFCD")
                    }
                };

                await db.Addresses.AddRangeAsync(addresses);
                await db.SaveChangesAsync();
            }

        }

        static async Task IQueryableExecutionSample()
        {
            using (var db = new BookStoreContext())
            {
                var neverExecuteQuery = db.Clients
                    .Where(cl
                        => !string.IsNullOrWhiteSpace(cl.Email)); // no 'future work' with data in any way

                var clients = db.Clients
                    .Where(cl
                        => !string.IsNullOrWhiteSpace(cl.Email));


                //IEnumerable.Next 
                foreach (var client in clients) //request real data -> sql real execution
                {
                    Console.WriteLine($"Client Name: {client.Name}, Email: {client.Email}");
                }

                //COUNTS 
                var clientCount = await db.Clients
                    .Where(cl
                        => !string.IsNullOrWhiteSpace(cl.Email))
                    .CountAsync(); //.Count()
                Console.WriteLine($"Total number of clients: {clientCount}");

                //CAST 
                var clientsList = await db.Clients
                    .Where(cl
                        => !string.IsNullOrWhiteSpace(cl.Email))
                    .ToArrayAsync(); //.To... .ToList() || .ToArray

                // Single 'entity' -> First, FirstOrDefault, Last, LoD, Single, SoD + Async
                var firstClient = await db.Clients
                    .FirstOrDefaultAsync(cl
                        => !string.IsNullOrWhiteSpace(cl.Email));

                //Bad practice
                var johnDoeClients = db.Clients.ToList().Where(cl => cl.Name.Contains("John Doe"));
                foreach (var client in johnDoeClients)
                {
                    Console.WriteLine($"John Doe Client: {client.Name}, Email: {client.Email}");
                }
            }
        }
    }
}
