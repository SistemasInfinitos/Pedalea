using CanonicalModel.Model.Configuration;
using CanonicalModel.Model.Entity.Pedalea;
using CanonicalModel.Model.Entity.Persona;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace PedaleaAPI.Repository.Pedalea
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

        public async Task<int> CrearDocumento(Documentos entidad)
        {
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpInserPersona", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    cmd.Parameters.Add("@PersonaID", SqlDbType.Int).Value = entidad.DocumentoID ?? 0;


                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    con.Close();
                    return rowsAffected;
                }
                catch (Exception e)
                {
                    var debugger = e.Message;
                    return e.GetHashCode();
                }
            }
        }

        public async Task<List<Documentos>> GetDocumentos()
        {
            List<Documentos> lista = new List<Documentos>();
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpGetDocumentos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (await rdr.ReadAsync())
                    {
                        var data = new Documentos()
                        {
                            DocumentoID = Convert.ToInt32(rdr["DocumentoID"]),
                            PersonaIDCliente = Convert.ToInt32(rdr["PersonaIDCliente"]),
                            PersonaIDVendedor = Convert.ToInt32(rdr["PersonaIDVendedor"]),
                            TipoDocumentoID = Convert.ToInt32(rdr["TipoDocumentoID"]),
                            ValorTotal = Convert.ToDecimal(rdr["ValorTotal"]),
                            FechaCreacion = Convert.ToDateTime(rdr["FechaCreacion"]),
                            Direccion = rdr["Direccion"].ToString(),
                        };
                        lista.Add(data);
                    }
                    con.Close();
                }
                catch (Exception e)
                {
                    return lista;
                }
                return lista;
            }
        }

        public async Task<Documentos> GetDocumentosById(int DocumentoID)
        {
            Documentos lista = new Documentos();
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpGetDocumentosById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    await rdr.ReadAsync();

                    var data = new Documentos()
                    {
                        DocumentoID = Convert.ToInt32(rdr["DocumentoID"]),
                        PersonaIDCliente = Convert.ToInt32(rdr["PersonaIDCliente"]),
                        PersonaIDVendedor = Convert.ToInt32(rdr["PersonaIDVendedor"]),
                        TipoDocumentoID = Convert.ToInt32(rdr["TipoDocumentoID"]),
                        ValorTotal = Convert.ToDecimal(rdr["ValorTotal"]),
                        FechaCreacion = Convert.ToDateTime(rdr["FechaCreacion"]),
                        Direccion = rdr["Direccion"].ToString(),
                    };
                    lista= data;
                    con.Close();
                }
                catch (Exception e)
                {
                    return lista;
                }
                return lista;
            }
        }
    }
}
