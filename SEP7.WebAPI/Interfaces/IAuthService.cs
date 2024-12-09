using System.ComponentModel.DataAnnotations;
using SEP7.WebAPI.Models;

namespace SEP7.WebAPI.Services;

public  interface IAuthService
{
    Task<User> UserValidation(string username, string password);



}