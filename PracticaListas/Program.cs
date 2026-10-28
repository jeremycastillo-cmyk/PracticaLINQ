using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main()
    {
        int opcion;
        do
        {
            Console.WriteLine(" PRÁCTICA DE LINQ ");
            Console.WriteLine("1. Sucursales con existencias suficientes");
            Console.WriteLine("2. Precios dentro del presupuesto");
            Console.WriteLine("3. Nombres de clientes para un reporte");
            Console.WriteLine("4. Tickets pendientes");
            Console.WriteLine("5. Búsqueda de un código de producto");
            Console.WriteLine("6. Preparar resultados para dos componentes");
            Console.WriteLine("7. Países por código");
            Console.WriteLine("8. Productos y categorías");
            Console.WriteLine("0. Salir");
            Console.Write("Elige una opción: ");

            // TryParse evita que el programa se caiga si escriben letras
            if (!int.TryParse(Console.ReadLine(), out opcion))
                opcion = -1;

            switch (opcion)
            {
                case 1: Problema1(); break;
                case 2: Problema2(); break;
                case 3: Problema3(); break;
                case 4: Problema4(); break;
                case 5: Problema5(); break;
                case 6: Problema6(); break;
                case 7: Problema7(); break;
                case 8: Problema8(); break;
                case 0: Console.WriteLine("Saliendo del programa..."); break;
                default: Console.WriteLine("Opción no válida. Intenta de nuevo."); break;
            }
        } while (opcion != 0);
    }

    
    static void Problema1()
    {
        List<int> existencias = new List<int> { 85, 60, 70, 95, 70, 40 };
        int[] pedidos = { 70, 100, 40 };

        foreach (int cantidadSolicitada in pedidos)
        {
            Console.WriteLine($"\nPedido de {cantidadSolicitada} unidades:");

            
            List<int> suficientes = existencias
                .Where(e => e >= cantidadSolicitada)
                .ToList();

            if (suficientes.Count == 0)
            {
                Console.WriteLine("Ninguna sucursal puede atender el pedido completo.");
            }
            else
            {
                Console.WriteLine("Existencias suficientes: " + string.Join(", ", suficientes));
                Console.WriteLine($"Sucursales que cumplen: {suficientes.Count}");
            }
        }
    }

    
    static void Problema2()
    {
        List<decimal> precios = new List<decimal> { 25m, 10m, 40m, 15m, 10m, 30m };
        decimal[] presupuestos = { 25m, 5m, 40m };

        foreach (decimal presupuesto in presupuestos)
        {
            Console.WriteLine($"\nPresupuesto de {presupuesto}:");

            List<decimal> dentro = precios
                .Where(p => p <= presupuesto)
                .OrderBy(p => p)
                .ToList();

            if (dentro.Count == 0)
            {
                Console.WriteLine("No hay precios dentro del presupuesto.");
            }
            else
            {
                Console.WriteLine("Precios: " + string.Join(", ", dentro));
                Console.WriteLine($"Cantidad: {dentro.Count}");
            }
        }
    }

    
    static void Problema3()
    {
        List<Cliente> clientes = new List<Cliente>
        {
            new Cliente(101, "Ana"),
            new Cliente(102, "Roberto"),
            new Cliente(103, "Luis"),
            new Cliente(104, "Ana")
        };

        
        void Reporte(List<Cliente> lista, string titulo)
        {
            Console.WriteLine($"\n{titulo}");
            List<string> nombres = lista.Select(c => c.Nombre).ToList();

            if (nombres.Count == 0)
                Console.WriteLine("No hay clientes para el reporte.");
            else
                Console.WriteLine("Nombres: " + string.Join(", ", nombres));
            Console.WriteLine($"Cantidad: {nombres.Count}");
        }

        Reporte(clientes, "Caso inicial:");

        clientes.Add(new Cliente(105, "María José"));
        Reporte(clientes, "Con el cliente 105 agregado:");

        Reporte(new List<Cliente>(), "Con la lista vacía:");
    }

  
    static void Problema4()
    {
        List<Ticket> tickets = new List<Ticket>
        {
            new Ticket("Instalar editor", "Pendiente", 1),
            new Ticket("Restablecer red", "Pendiente", 3),
            new Ticket("Revisar impresora", "Cerrado", 3),
            new Ticket("Configurar correo", "Pendiente", 2)
        };

        void Buscar(string estadoBuscado)
        {
            Console.WriteLine($"\nTickets con estado \"{estadoBuscado}\":");

            List<string> titulos = tickets
                .Where(t => t.Estado == estadoBuscado)
                .OrderByDescending(t => t.Prioridad)
                .Select(t => t.Titulo)
                .ToList();

            if (titulos.Count == 0)
            {
                Console.WriteLine("No hay tickets con ese estado.");
            }
            else
            {
                foreach (string titulo in titulos)
                    Console.WriteLine("- " + titulo);
                Console.WriteLine($"Cantidad: {titulos.Count}");
            }
        }

        Buscar("Pendiente");

        tickets.Add(new Ticket("Reparar servidor", "Pendiente", 3));
        tickets.Add(new Ticket("Actualizar antivirus", "Cerrado", 2));
        Buscar("Pendiente");

        Buscar("En proceso");
    }

    
    static void Problema5()
    {
        List<string> codigos = new List<string> { "P-100", "P-200", "P-300", "P-100" };

        void Buscar(List<string> lista, string codigoBuscado, string titulo)
        {
            Console.WriteLine($"\n{titulo} - buscando \"{codigoBuscado}\":");

            
            try
            {
                string encontrado = lista.First(c => c == codigoBuscado);
                Console.WriteLine($"  Versión 1 (First): encontrado {encontrado}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("  Versión 1 (First): no se encontró el código (excepción controlada).");
            }

           
            string resultado = lista.FirstOrDefault(c => c == codigoBuscado);
            if (resultado != null)
                Console.WriteLine($"  Versión 2 (FirstOrDefault): encontrado {resultado}");
            else
                Console.WriteLine("  Versión 2 (FirstOrDefault): no se encontró el código.");
        }

        Buscar(codigos, "P-100", "Colección normal");
        Buscar(codigos, "P-999", "Colección normal");
        Buscar(new List<string>(), "P-100", "Colección vacía");
        Buscar(new List<string>(), "P-999", "Colección vacía");
    }

    
    static void Problema6()
    {
        List<int> edades = new List<int> { 15, 22, 17, 30, 18 };

        void Procesar(List<int> fuente, string titulo)
        {
            Console.WriteLine($"\n{titulo}");

            List<int> lista = fuente.Where(e => e >= 18).ToList();
            int[] arreglo = fuente.Where(e => e >= 18).ToArray();

            Console.WriteLine("Lista:   " + string.Join(", ", lista) + $" (cantidad: {lista.Count})");
            Console.WriteLine("Arreglo: " + string.Join(", ", arreglo) + $" (cantidad: {arreglo.Length})");

            
            lista.Add(40);

            
            if (arreglo.Length > 0)
                arreglo[0] = 23;
            else
                Console.WriteLine("El arreglo está vacío, no se puede cambiar su primer elemento.");

            Console.WriteLine("Después de los cambios:");
            Console.WriteLine("Lista:    " + string.Join(", ", lista) + $" (cantidad: {lista.Count})");
            Console.WriteLine("Arreglo:  " + string.Join(", ", arreglo) + $" (cantidad: {arreglo.Length})");
            Console.WriteLine("Original: " + string.Join(", ", fuente));
        }

        Procesar(edades, "Caso inicial:");
        Procesar(new List<int> { 15, 17, 16 }, "Fuente sin personas mayores de edad:");
    }

   
    static void Problema7()
    {
        List<Pais> paises = new List<Pais>
        {
            new Pais("PA", "Panamá"),
            new Pais("PE", "Perú"),
            new Pais("CL", "Chile")
        };

        
        Dictionary<string, string> diccionario = paises.ToDictionary(p => p.Codigo, p => p.Nombre);

        string[] consultas = { "PA", "MX" };
        foreach (string codigo in consultas)
        {
            
            if (diccionario.TryGetValue(codigo, out string nombre))
                Console.WriteLine($"El código {codigo} corresponde a {nombre}.");
            else
                Console.WriteLine($"El código {codigo} no existe en el diccionario.");
        }
    }

    
    static void Problema8()
    {
        List<Categoria> categorias = new List<Categoria>
        {
            new Categoria(1, "Papelería"),
            new Categoria(2, "Tecnología"),
            new Categoria(3, "Hogar")
        };

        List<Producto> productos = new List<Producto>
        {
            new Producto("Cuaderno", 1),
            new Producto("Ratón", 2),
            new Producto("Lápiz", 1),
            new Producto("Artículo especial", 99)
        };

        void Relacionar(string titulo)
        {
            Console.WriteLine($"\n{titulo}");

            List<string> parejas = productos
                .Join(categorias,
                      p => p.CategoriaId,
                      c => c.Id,
                      (p, c) => $"{p.Nombre} - {c.Nombre}")
                .ToList();

            foreach (string pareja in parejas)
                Console.WriteLine("- " + pareja);
            Console.WriteLine($"Cantidad de parejas: {parejas.Count}");
        }

        Relacionar("Caso inicial (el artículo especial no sale: no existe la categoría 99):");

        categorias.Add(new Categoria(99, "Especiales"));
        Relacionar("Con la categoría 99 agregada:");

        categorias.Add(new Categoria(1, "Útiles"));
        Relacionar("Con otra categoría de id 1 (\"Útiles\"): Cuaderno y Lápiz salen dos veces:");
    }
}


public class Cliente
{
    public int Id { get; }
    public string Nombre { get; }
    public Cliente(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}

public class Ticket
{
    public string Titulo { get; }
    public string Estado { get; }
    public int Prioridad { get; }
    public Ticket(string titulo, string estado, int prioridad)
    {
        Titulo = titulo;
        Estado = estado;
        Prioridad = prioridad;
    }
}

public class Pais
{
    public string Codigo { get; }
    public string Nombre { get; }
    public Pais(string codigo, string nombre)
    {
        Codigo = codigo;
        Nombre = nombre;
    }
}

public class Categoria
{
    public int Id { get; }
    public string Nombre { get; }
    public Categoria(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}

public class Producto
{
    public string Nombre { get; }
    public int CategoriaId { get; }
    public Producto(string nombre, int categoriaId)
    {
        Nombre = nombre;
        CategoriaId = categoriaId;
    }
}