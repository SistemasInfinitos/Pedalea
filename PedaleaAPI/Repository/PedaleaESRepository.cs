﻿using CanonicalModel.Model.Configuration;
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

        public Task<Personas> CrearPersona(Personas entidad)
        {
            throw new NotImplementedException();
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