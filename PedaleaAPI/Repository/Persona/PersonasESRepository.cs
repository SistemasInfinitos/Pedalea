using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Persona;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace PedaleaAPI.Repository.Persona
{
    public class PersonasESRepository: IPersonasESRepository
    {
        private readonly CultureInfo culture = new CultureInfo("is-IS");
        private readonly CultureInfo cultureFecha = new CultureInfo("en-US");
        private readonly JwtConfiguration _jwtConfig;

        //private readonly DbContextOptions<Context> _db = ContextDB.DB;

        public PersonasESRepository(IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<int> CrearPersona(Personas entidad)
        {
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpInserPersona", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    cmd.Parameters.Add("@PersonaID", SqlDbType.Int).Value = entidad.PersonaID ?? 0;
                    cmd.Parameters.Add("@PrimerNombre", SqlDbType.VarChar, 50).Value = entidad.PrimerNombre;
                    cmd.Parameters.Add("@SegundoNombre", SqlDbType.VarChar, 50).Value = entidad.SegundoNombre;
                    cmd.Parameters.Add("@PrimerApellido", SqlDbType.VarChar, 50).Value = entidad.PrimerApellido;
                    cmd.Parameters.Add("@SegundoApellido", SqlDbType.VarChar, 50).Value = entidad.SegundoApellido;
                    cmd.Parameters.Add("@Identificacion", SqlDbType.VarChar, 15).Value = entidad.Identificacion;
                    cmd.Parameters.Add("@EsCliente", SqlDbType.Bit).Value = entidad.EsCliente;
                    cmd.Parameters.Add("@EsProveedor", SqlDbType.Bit).Value = entidad.EsProveedor;

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    con.Close();
                    return (rowsAffected);
                }
                catch (Exception e)
                {
                    var debugger = e.Message;
                    return e.GetHashCode();
                }
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
                        Identificacion = rdr["identificacion"].ToString(),
                        SegundoNombre = rdr["SegundoNombre"].ToString(),
                        PrimerApellido = rdr["PrimerApellido"].ToString(),
                        SegundoApellido = rdr["SegundoApellido"].ToString(),
                        EsCliente = Convert.ToBoolean(rdr["EsCliente"].ToString()),
                        EsProveedor = Convert.ToBoolean(((!string.IsNullOrWhiteSpace(rdr["EsProveedor"]?.ToString())? rdr["EsProveedor"]?.ToString() :false))),
                    };
                    personas.Add(employee);
                }
                con.Close();
                return (personas);
            }
        }

        public async Task<Personas> GetPersonasById(int PersonaID)
        {
            Personas personas = new Personas();
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpGetPersonasById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PersonaID", SqlDbType.Int).Value = PersonaID ;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                try
                {
                    await rdr.ReadAsync();
                    var employee = new Personas()
                    {
                        PersonaID = Convert.ToInt32(rdr["PersonaID"]),
                        PrimerNombre = rdr["PrimerNombre"].ToString(),
                        Identificacion = rdr["identificacion"].ToString(),
                        SegundoNombre = rdr["SegundoNombre"].ToString(),
                        PrimerApellido = rdr["PrimerApellido"].ToString(),
                        SegundoApellido = rdr["SegundoApellido"].ToString(),
                        EsCliente = Convert.ToBoolean(rdr["EsCliente"].ToString()),
                        EsProveedor = Convert.ToBoolean(((!string.IsNullOrWhiteSpace(rdr["EsProveedor"]?.ToString()) ? rdr["EsProveedor"]?.ToString() : false))),
                    };
                    personas = employee;

                    con.Close();
                    return (personas);
                }
                catch (Exception e)
                {
                    con.Close();
                    return (personas);
                }
            }
        }
    }
}
