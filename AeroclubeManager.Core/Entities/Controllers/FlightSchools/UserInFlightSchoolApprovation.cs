using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Entities.Controllers.FlightSchools
{
	public class UserInFlightSchoolApprovation
	{
		public UserInFlightSchoolStatus Status { get; set; }
		public string Message { get; set; }
		public UserFlightSchool? User { get; set; } = null;
	}
}
