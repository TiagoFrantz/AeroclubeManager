using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Controllers.FlightSchools;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Interfaces.Services.FlightSchoolServices
{
    public interface IFlightSchoolService
    {
        public Task<FlightSchool?> GetFlightSchoolById(string id);
        public Task<FlightSchool?> CreateFlightSchool(FlightSchool flightSchool);


        public Task<FlightSchool?> UpdateFlightSchool(FlightSchool flightSchool);

        public Task<bool> DeleteFlightSchoolById(string id);

        public Task<List<FlightSchool>?> GetAllFlightSchools();
        public Task<List<FlightSchool>?> GetFlightSchoolsContaining(string name);
        
        /// <summary>
        /// Verifica se o usuário possui permissões. Você pode verificar apenas se o usuário faz parte do Aeroclube passando nulo no parametro.
        /// </summary>
        /// <param name="flightSchoolId"></param>
        /// <param name="userId"></param>
        /// <param name="requiredRole">Opcional. Nulo caso queira apenas verificar se faz parte do aeroclube.</param>
        /// <returns></returns>
        public Task<UserInFlightSchoolApprovation> UserApprovedInFlightSchool(Guid flightSchoolId, string userId, List<FlightSchoolRoleEnum>? requiredRole = null);

		public Task<List<FlightSchool>> GetFlightSchoolsByUserId(string userId);
        
        /// <summary>
        /// Pegar UserFlightSchool com base no id da escola e do usuário
        /// </summary>
        /// <param name="flightSchoolId"></param>
        /// <param name="userId"></param>
        /// <returns>UserFlightSchool ou null caso não achar</returns>
        public Task<UserFlightSchool?> GetUserFlightSchoolByUserId(Guid flightSchoolId, string userId);


    }
}
