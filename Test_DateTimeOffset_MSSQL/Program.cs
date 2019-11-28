using System;

namespace Test_DateTimeOffset_MySQL
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var context = new SimpleEntityContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.SaveChanges();
            }

            using (var context = new SimpleEntityContext())
            {
                var date1 = DateTimeOffset.Now;
                var entity1 = new SimpleEntity
                {
                    Name = "First",
                    CreateDate = DateTime.Now,
                    CreateDateWithOffset = DateTimeOffset.Now
                };
                var entity2 = new SimpleEntity
                {
                    Name = "Second",
                    CreateDate = DateTime.Now,
                    CreateDateWithOffset = DateTimeOffset.Now
                };
                var entity3 = new SimpleEntity
                {
                    Name = "Third",
                    CreateDate = DateTime.Now,
                    CreateDateWithOffset = DateTimeOffset.Now
                };

                context.SimpleEntities.Add(entity1);
                context.SimpleEntities.Add(entity2);
                context.SimpleEntities.Add(entity3);
                context.SaveChanges();

                for (var i = 0; i < 3; i++)
                {
                    var date = DateTimeOffset.Now;

                    var entity = new SimpleEntity
                    {
                        Name = "Entity #" + i,
                        CreateDate = date.DateTime,
                        CreateDateWithOffset = date
                    };

                    context.SimpleEntities.Add(entity);
                    context.SaveChanges();
                }
            }
        }
    }
}
