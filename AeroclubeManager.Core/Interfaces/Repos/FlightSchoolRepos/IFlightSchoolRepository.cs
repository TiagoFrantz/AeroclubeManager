using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Interfaces.Repos.FlightSchoolRepos
{
    public interface IFlightSchoolRepository
    {

        /// <summary>
        /// Criar uma escola de aviação e adiciona ao banco de dados.
        /// </summary>
        /// <param name="flightSchool"></param>
        /// <returns>FlightSchool ou nulo caso falhado</returns>
        public Task<FlightSchool?> CreateFlightSchool(FlightSchool flightSchool);


        /// <summary>
        /// Faz uma atualização em um FlightSchool com base no ID disponivel no objeto.
        /// </summary>
        /// <param name="flightSchool">Objeto com o id da FlightSchool e com os parametros corretos.</param>
        /// <returns>FlightSchool ou nulo caso falhado</returns>
        public Task<FlightSchool?> UpdateFlightSchool(FlightSchool flightSchool);

        /// <summary>
        /// Deleta uma escola de aviação.
        /// </summary>
        /// <param name="flightSchoolId"></param>
        /// <returns>True se deletado com sucesso, False caso não deletado</returns>
        public Task<bool> DeleteFlightSchool(Guid flightSchoolId);

        public Task<List<FlightSchool>?> GetAllFlightSchools();

        public Task<FlightSchool?> GetFlightSchoolById(Guid flightSchoolId);

        public Task<List<FlightSchool>> GetFlightSchoolsByUserId(string userId);

        public Task<UserFlightSchool?> GetUserFlightSchoolByUserId(Guid flightSchoolId, string userId);

    }
}
