using lab1_ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4_igi.Models
{
    public class ClientComparer : IComparer<Client>
    {
        public int Compare(Client x, Client y)
        {
            if (String.Compare(x.Name, y.Name) == 1)
                return 1;
            else if (String.Compare(x.Name, y.Name) == -1)
                return -1;
            else
                return 0;
        }
    }
    public class EmployeeComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            if (String.Compare(x.Name, y.Name) == 1)
                return 1;
            else if (String.Compare(x.Name, y.Name) == -1)
                return -1;
            else
                return 0;
        }
    }
    public class RoomsComparer : IComparer<Room>
    {
        public int Compare(Room x, Room y)
        {
            return String.Compare(x.RoomNo, y.RoomNo);
        }
    }
    public class RoomTypesComparer : IComparer<RoomType>
    {
        public int Compare(RoomType x, RoomType y)
        {
            return String.Compare(x.Name, y.Name);
        }
    }
    public class ServiceTypesComparer : IComparer<ServiceType>
    {
        public int Compare(ServiceType x, ServiceType y)
        {
            if (String.Compare(x.Name, y.Name) == 1)
                return 1;
            else if (String.Compare(x.Name, y.Name) == -1)
                return -1;
            else
                return x.Cost.CompareTo(y.Cost);
        }
    }    
    public class ServiceComparer : IComparer<Service>
    {
        public int Compare(Service x, Service y)
        {
            if (x.ClientId > y.ClientId)
                return 1;
            else if (x.ClientId < y.ClientId)
                return -1;
            else if(x.EmployeeId > y.EmployeeId)
                return 1;
            else if (x.EmployeeId < y.EmployeeId)
                return -1;
            else if (x.ServiceTypeId > y.ServiceTypeId)
                return 1;
            else if (x.ServiceTypeId < y.ServiceTypeId)
                return -1;
            return 0;
        }
    }
}
