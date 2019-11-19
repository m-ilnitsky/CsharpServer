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
                var thread1 = context.Threads.Single(p => p.Id == 1);
                var newMessage = new Message
                {
                    Text = "Вот!",
                    IsIncoming = true,
                    Thread = thread1
                };
                var newFile1 = new File
                {
                    Name = "wau.ch",
                    Message = newMessage
                };
                var newFile2 = new File
                {
                    Name = "wau.js",
                    Message = newMessage
                };

                context.Messages.Add(newMessage);
                context.Files.Add(newFile1);
                context.Files.Add(newFile2);
                thread1.Status = 1;


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
                                var thread = (Thread)entry.Entity;

                                foreach (var message in thread.Messages)
                                {
                                    foreach (var file in message.Files)
                                    {
                                        context.Entry(file).State = EntityState.Detached;
                                    }

                                    context.Entry(message).State = EntityState.Detached;
                                }

                                entry.State = EntityState.Detached;

                                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
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
