using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace PedaleaAPI.Repository
{
    public class PedaleaESRepository : IPedaleaESRepository
    {
        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");
        private readonly JwtConfiguration _jwtConfig;

        //private readonly DbContextOptions<Context> _db = ContextDB.DB;

        public PedaleaESRepository(IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<int> CrearPersona(Personas entidad)
        {
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpInserPersona", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlParameter param = new();
                param = cmd.Parameters.Add("@PrimerNombre", SqlDbType.NVarChar, 50);
                param.Value = entidad.PrimerNombre;

                param = cmd.Parameters.Add("@SegundoNombre", SqlDbType.NVarChar, 50);
                param.Value = entidad.SegundoNombre;

                param = cmd.Parameters.Add("@PrimerApellido", SqlDbType.NVarChar, 50);
                param.Value = entidad.PrimerApellido; 
                
                param = cmd.Parameters.Add("@SegundoApellido", SqlDbType.NVarChar, 50);
                param.Value = entidad.SegundoApellido;   
                
                param = cmd.Parameters.Add("@EsCliente", SqlDbType.Bit);
                param.Value = entidad.EsCliente;

                param = cmd.Parameters.Add("@Identificacion", SqlDbType.Int);
                param.Value = entidad.Identificacion;

                //add the parameter to the SqlCommand object
                cmd.Parameters.Add(param);
                int rowsAffected =await cmd.ExecuteNonQueryAsync();
                con.Close();
                return (rowsAffected);
            }
        }

        public async Task<List<Personas>> GetPersonas()
        {
            List<Personas> personas = new List<Personas>();
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpGetPersonas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (await rdr.ReadAsync())
                {
                    var employee = new Personas()
                    {
                        PersonaID = Convert.ToInt32(rdr["PersonaID"]),
                        PrimerNombre = rdr["PrimerNombre"].ToString(),
                        SegundoNombre = rdr["SegundoNombre"].ToString(),
                        PrimerApellido = rdr["PrimerApellido"].ToString(),
                        SegundoApellido = rdr["SegundoApellido"].ToString(),
                        EsCliente = Convert.ToBoolean(rdr["EsCliente"].ToString()),

                    };
                    personas.Add(employee);
                }
                con.Close();
                return (personas);
            }
        }
    }
}
