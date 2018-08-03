using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCal.Models
{
    public class MasterProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  Boolean Actif { get; set; }
        public DateTime  CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class MasterCustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contract { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? ProjectId { get; set; }
        public IEnumerable<MasterProjectViewModel> ProjectInfo { get; set; }
        public Boolean Actif { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class MasterTypeDayViewModel
    {
        public int Id { get; set; }
        public string Type { get; set;}
        public int? Number { get; set; }
        public string Color { get; set; }
    }

    public class MasterContractorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public string Company { get; set; }

        public string Contract { get; set; }

        public int? CustomerId { get; set; }

        public int? ProjectId { get; set; }
    }

    public class MasterEntriesViewModel
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int ContractorId { get; set; }
        public IEnumerable<MasterContractorViewModel> ContractorInfo { get; set; }
        public int TypedayId { get; set; }
        public IEnumerable<MasterTypeDayViewModel> TypedayInfo { get; set; }
        public long? DatetimeMs { get; set; }

    }
    public class MasterData
    {
        public IEnumerable<MasterProjectViewModel> ProjectList { get; set; }

        public IEnumerable<MasterCustomerViewModel> CustomerList { get; set; }

        public IEnumerable<MasterTypeDayViewModel> TypeDayList { get; set; }

        public IEnumerable<MasterContractorViewModel> ContractorList { get; set; }
        
    }
}