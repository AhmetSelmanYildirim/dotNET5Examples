using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { Name = "Eric", Surname = "Ries", DateOfBirth = new System.DateTime(1978, 09, 22) },
                new Author { Name = "Charlotte", Surname = "Gilman", DateOfBirth = new System.DateTime(1860, 07, 03) },
                new Author { Name = "Frank", Surname = "Herbert", DateOfBirth = new System.DateTime(1920, 10, 08) }
            );
        }
    }
}