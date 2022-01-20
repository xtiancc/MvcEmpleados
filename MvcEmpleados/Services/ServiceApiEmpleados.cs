using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using LibraryEmpleados;

namespace MvcEmpleados.Services
{
    public class ServiceApiEmpleados
    {
        private string Url;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiEmpleados(string url) {
            this.Url = url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        // Método generico para indicar el tipo que devuelve y la petición
        private async Task<T> CallApiAsync<T> (string request) {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode) {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    // Si es string devolverá "", si es número devolvera 0, si es objeto devolverá null, etc.
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "/api/empleados";
            List<Empleado> empleados = await CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<Empleado> GetEmpleadoAsync(int id)
        {
            string request = "/api/empleados/" + id;
            Empleado empleado = await CallApiAsync<Empleado>(request);
            return empleado;
        }

        public async Task<List<String>> GetOficiosAsync()
        {
            string request = "/api/empleados/oficios";
            List<String> oficios = await CallApiAsync<List<String>>(request);
            return oficios;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            string request = "/api/empleados/empleadosoficios/" + oficio;
            List<Empleado> empleados = await CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioSalarioAsync(string oficio, int salario)
        {
            string request = "/api/empleados/" + oficio + "/" + salario;
            List<Empleado> empleados = await CallApiAsync<List<Empleado>>(request);
            return empleados;
        }

    }
}
