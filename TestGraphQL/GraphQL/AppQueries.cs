﻿using GraphQL;
using GraphQL.Types;

namespace TestGraphQL.GraphQL
{

    public partial class AppQueries : ObjectGraphType
    {
        List<User> users;
        public AppQueries()
        {

            users = new List<User>
            {
                new User { Id = 1, FirstName = "Test1", LastName = "Test1", CreatedAt = DateTime.UtcNow, Address = new Address { Street = "Street 123"} },
                new User { Id = 2, FirstName = "Test2", LastName = "Test2", CreatedAt = DateTime.UtcNow },
                new User { Id = 3, FirstName = "Test3", LastName = "Test3", CreatedAt = DateTime.UtcNow },
            };

            Name = "AppQueries";

            Field<StringGraphType>("exampleQuery1")
                .Resolve(context =>
                    {
                        return "exampleQuery Response";
                    });

            Field<StringGraphType>("exampleQueryException")
                .Resolve(context =>
                    {
                        context.Errors.Add(new ExecutionError("This is the message Exception"));

                        return null;
                    });

            Field<UserType>("getUser")
                .Arguments(new QueryArguments(
                        new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" })
                )
                .Resolve(context =>
                    {
                        var userId = context.GetArgument<int>("userId");
                        var user = users.Where(x => x.Id == userId).FirstOrDefault();

                        return user;
                    });

            Field<ListGraphType<UserType>>("GetAllUsers")
                .Resolve(context =>
                    {

                        return users;
                    });
        }

    }
}
