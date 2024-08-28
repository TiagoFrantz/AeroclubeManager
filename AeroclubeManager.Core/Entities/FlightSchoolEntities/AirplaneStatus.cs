using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public enum AirplaneStatus
    {
        Available,
        InUse,
        UnderMaintenance, 
        Grounded,
        Reserved,
        Decommissioned
    }
}
