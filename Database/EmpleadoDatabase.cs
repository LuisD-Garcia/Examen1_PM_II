using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen1_PM_II.Models;
using SQLite;

namespace Examen1_PM_II.Database
{
    public class EmpleadoDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public EmpleadoDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            // Crea la tabla Empleado si no existe
            _database.CreateTableAsync<Empleado>().Wait();
        }

        // Obtener toda la lista de Empleados
        public Task<List<Empleado>> GetListaEmpleado()
        {
            return _database.Table<Empleado>().ToListAsync();
        }

        // Obtener un Empleado por Id
        public Task<Empleado> GetEmpleado(int id)
        {
            return _database.Table<Empleado>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        // Guardar o Actualizar un Empleado
        public Task<int> SaveEmpleado(Empleado Empleado)
        {
            if (Empleado.Id != 0)
            {
                // Actualizar
                return _database.UpdateAsync(Empleado);
            }
            else
            {
                // Guardar nuevo
                return _database.InsertAsync(Empleado);
            }
        }
    }
}
