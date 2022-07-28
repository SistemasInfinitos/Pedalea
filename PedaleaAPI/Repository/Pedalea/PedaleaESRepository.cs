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

        public PedaleaESRepository(IOptionsMonitor<JwtConfiguration> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<int> BorrarDocumentosById(int DocumentoID)
        {
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpDeleteDocumento", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    cmd.Parameters.Add("@DocumentoID", SqlDbType.Int).Value = DocumentoID;

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

        public async Task<int> CrearDocumento(Pedidos entidad)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpInserPersona", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    foreach (var item in entidad.LisPedidos) 
                    { 
                        cmd.Parameters.Add("@DocumentoID", SqlDbType.Int).Value = entidad.DocumentoID> 0? entidad.DocumentoID:rowsAffected;
                        cmd.Parameters.Add("@PersonaID", SqlDbType.Int).Value = entidad.PersonaID;
                        cmd.Parameters.Add("@ValorTotal", SqlDbType.Decimal).Value = entidad.ValorTotal;
                        cmd.Parameters.Add("@TipoDocumentoID", SqlDbType.Int).Value = entidad.TipoDocumentoID;
                        cmd.Parameters.Add("@PersonaIDCliente", SqlDbType.Int).Value = entidad.PersonaIDCliente;
                        cmd.Parameters.Add("@PersonaIDVendedor", SqlDbType.Int).Value = entidad.PersonaIDVendedor;
                        cmd.Parameters.Add("@PrimerNombre", SqlDbType.VarChar).Value = entidad.PrimerNombre;
                        cmd.Parameters.Add("@SegundoNombre", SqlDbType.VarChar).Value = entidad.SegundoNombre;
                        cmd.Parameters.Add("@PrimerApellido", SqlDbType.VarChar).Value = entidad.PrimerApellido;
                        cmd.Parameters.Add("@SegundoApellido", SqlDbType.VarChar).Value = entidad.SegundoApellido;
                        cmd.Parameters.Add("@Identificacion", SqlDbType.VarChar).Value = entidad.Identificacion;
                        cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = entidad.Direccion;
                        cmd.Parameters.Add("@EsCliente", SqlDbType.Bit).Value = entidad.EsCliente;
                        cmd.Parameters.Add("@EsProveedor", SqlDbType.Bit).Value = entidad.EsProveedor;


                        cmd.Parameters.Add("@ProductoID", SqlDbType.Int).Value = item.ProductoID;
                        cmd.Parameters.Add("@ValorUnitario", SqlDbType.Decimal).Value = item.ValorUnitario;
                        cmd.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = item.Cantidad;
                        cmd.Parameters.Add("@PorcentajeDescuento", SqlDbType.Decimal).Value = item.PorcentajeDescuento;

                        cmd.Parameters.Add("@IdOutPut", SqlDbType.Int).Value = rowsAffected;

                        rowsAffected = await cmd.ExecuteNonQueryAsync();
                    }


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
                    cmd.Parameters.Add("@DocumentoID", SqlDbType.Int).Value = DocumentoID;
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

        public async Task<List<Productos>> GetProductosByName(string name,int? ProductoID)
        {
            if (name=="0")
            {
                name = "";
            }
            if (ProductoID==null)
            {
                ProductoID = 0;
            }
            List<Productos> lista = new();
            using (SqlConnection con = new SqlConnection(_jwtConfig.ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpGetProductosByName", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@ProductoID", SqlDbType.Int).Value = ProductoID;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (await rdr.ReadAsync())
                    {
                        var data = new Productos()
                        {
                            ProductoID = Convert.ToInt32(rdr["ProductoID"]),
                            Producto = rdr["Producto"].ToString(),
                            Valor = Convert.ToDecimal(rdr["Valor"]),
                            Talla = rdr["Talla"].ToString(),
                            Color = rdr["Color"].ToString(),
                            FechaCreacion = Convert.ToDateTime(rdr["FechaCreacion"]),
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
    }
}
