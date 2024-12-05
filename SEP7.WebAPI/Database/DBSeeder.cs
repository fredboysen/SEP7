using SEP7.WebAPI.Models;


namespace SEP7.Database.Data
{
public static class DbSeeder
{

public static void Seed(ApplicationDB context)
{
context.Materials.RemoveRange(context.Materials);
context.Routes.RemoveRange(context.Routes);



//In order to seed to DB
//Setup new object here to db based on models 


}

}

}