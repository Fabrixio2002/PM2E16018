using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E16018.Controllers
{
    public class SitiosControllers
    {
        SQLiteAsyncConnection _connection;

        //constructor Vacio
        // public PersonasControllers() { }

        //Conexion a Base de Datos
        async Task Init()
        {
            if (_connection is not null)
            {
                return;
            }

            SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite |
                                                 SQLite.SQLiteOpenFlags.Create |
                                                 SQLite.SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBsitios.db3"), extensiones);

            var creacion = await _connection.CreateTableAsync<Models.Sitios>();
       


        }

        //Crear metodos CRUD

        //CREATE
        public async Task<int> StorePerson(Models.Sitios sitios)
        {
            await Init();
            if (sitios.Id == 0)
            {
                return await _connection.InsertAsync(sitios);
            }
            else
            {
                return await _connection.UpdateAsync(sitios);
            }
        }

        //READ
        public async Task<List<Models.Sitios>> GetListPersons()
        {
            await Init();
            return await _connection.Table<Models.Sitios>().ToListAsync();
        }

        //READ elemt
        public async Task<Models.Sitios> GePerson(int pid)
        {
            await Init();
            return await _connection.Table<Models.Sitios>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

        //DELETE
        public async Task<int> DeletePerson(Models.Sitios sitios)
        {
            await Init();
            return await _connection.DeleteAsync(sitios);
        }

        public async Task<int> UpdatePerson(Models.Sitios sitios)
        {
            await Init();
            return await _connection.UpdateAsync(sitios);
        }

        //UPDATE
        public async Task<int> UpdateAsync(Models.Sitios sitios)
        {
            await Init();
            return await _connection.UpdateAsync(sitios);
        }



    }




}
