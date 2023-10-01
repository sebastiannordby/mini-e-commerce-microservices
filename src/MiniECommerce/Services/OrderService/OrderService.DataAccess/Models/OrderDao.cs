using System.Net.Sockets;

namespace OrderService.DataAccess.Models
{
    public class OrderDao
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public string UserPrincipalName { get; private set; }

        public OrderDao(
            Guid id,
            int number,
            string userPrincipalName)
        {
            Id = id;
            Number = number;
            UserPrincipalName = userPrincipalName;
        }
    }
}