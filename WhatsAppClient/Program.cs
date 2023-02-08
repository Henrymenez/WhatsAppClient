using WhatsAppDAL;
using WhatsAppDAL.Model;


namespace WhatsAppClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            using (IWhatsAppService whatsAppService = new WhatsAppService(new WhatsAppDbContext()))
            {
                var userData = new UserViewModel
                {
                    Name = "Henrynew",
                    Email = "ugonew@gmail.com",
                    PhoneNumber = "0909300292354",
                    ProfilePhoto = "henry8.png"
                };

                 var createdUserId =  await whatsAppService.CreateUser(userData);
               // await whatsAppService.UpdateUser(userData);
                // var deletedUser = await whatsAppService.DeleteUser(2);
                // var user = await whatsAppService.GetUser(3);
                /*var users = await whatsAppService.GetUsers();*/

               /* foreach (UserViewModel user in users)
                {
                    Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Phone: {user.PhoneNumber}, Photo: {user.ProfilePhoto} \n");
                }*/
                // Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Phone: {user.PhoneNumber}, Photo: {user.ProfilePhoto}");
                 Console.WriteLine(createdUserId);

            }



        }
    }
}