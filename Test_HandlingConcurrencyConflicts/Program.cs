using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Test_HandlingConcurrencyConflicts
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample.Run();

            using (var context = new PersonContext())
            {
                var thread2 = new Thread
                {
                    UserName = "User2",
                    UserEmail = "user2@email",
                    Subject = "Subject A",
                    Status = 0
                };
                var message3 = new Message
                {
                    Text = "Смотри файл!",
                    IsIncoming = true,
                    Thread = thread2
                };
                var file2 = new File
                {
                    Name = "wau.java",
                    Message = message3
                };

                context.Threads.Add(thread2);
                context.Messages.Add(message3);
                context.Files.Add(file2);

                context.SaveChanges();
            }

            using (var context = new PersonContext())
            {
                var thread = context.Threads.Single(p => p.Id == 1);
                var message = new Message
                {
                    Text = "Смотри!",
                    IsIncoming = true,
                    Thread = thread
                };
                var file = new File
                {
                    Name = "wau.ch",
                    Message = message
                };

                context.Messages.Add(message);
                context.Files.Add(file);
                thread.Status = 1;


                /*context.Database.ExecuteSqlCommand(
                    "INSERT INTO Messages (ThreadId, Text, IsIncoming) VALUES(2, 'Тоже круто! Вот ещё!', true)");
                context.Database.ExecuteSqlCommand(
                    "INSERT INTO Files (MessageId, Name) VALUES(4, 'wau.js')");*/
                context.Database.ExecuteSqlCommand("UPDATE Threads SET Status = 2 WHERE Id = 1");

                var saved = false;
                while (!saved)
                {
                    try
                    {
                        // Attempt to save changes to the database
                        context.SaveChanges();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is Thread)
                            {
                                entry.State = EntityState.Detached;

                                //TODO заигнорить новые Message и File

                                /*var proposedValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();

                                foreach (var property in proposedValues.Properties)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];

                                    // TODO: decide which value should be written to database

                                    proposedValues[property] = databaseValue;
                                }

                                // Refresh original values to bypass next concurrency check
                                entry.OriginalValues.SetValues(databaseValues);*/
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Don't know how to handle concurrency conflicts for "
                                    + entry.Metadata.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
