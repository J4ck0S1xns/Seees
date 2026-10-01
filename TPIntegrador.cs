using System;
using System.Collections.Generic;
namespace TPIntegrador{
public class Producto
{
    public string Nombre { get; private set; }
    public decimal Precio { get; private set; }
    public int Stock { get; private set; }


    public Producto(string nombre, decimal precio, int stock)
    {
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }


    public bool DescontarStock(int cantidad)
    {
        if (Stock >= cantidad)
        {
            Stock -= cantidad;
            return true;
        }
        return false;
    }


public class Pedido
{
    private List<Producto> _productos = new List<Producto>();
    public string Estado { get; private set; } = "Pendiente";


    public void AgregarProducto(Producto p, int cant)
    {
        if (p.DescontarStock(cant))
            _productos.Add(p);
    }


    public decimal CalcularTotal(decimal impuesto)
    {
        decimal total = 0;


        foreach (Producto p in _productos)
            total += p.Precio;


        return total + total * impuesto;
    }


    public void Pagar()
    {
        Estado = "Completado";
    }
}




class Program
{
    static void Main()
    {
        List<Producto> inventario = new List<Producto>
        {
            new Producto("Teclado", 25000, 3),
            new Producto("Mouse", 15000, 5),
            new Producto("Monitor", 120000, 2)
        };


        Pedido pedido = new Pedido();
        bool ejecutando = true;


        while (ejecutando)
        {
            Console.WriteLine("\n1. Inventario\n2. Comprar\n3. Resumen\n4. Pagar\n5. Salir");
            int.TryParse(Console.ReadLine(), out int opcion);


            switch (opcion)
            {
                case 1:
                    MostrarInventario(inventario);
                    break;


                case 2:
                    MostrarInventario(inventario);
                    Console.Write("Producto: ");
                    int.TryParse(Console.ReadLine(), out int n);
                    Console.Write("Cantidad: ");
                    int.TryParse(Console.ReadLine(), out int cant);


                    if (n > 0 && n <= inventario.Count &&
                        inventario[n - 1].Stock >= cant)
                    {
                        pedido.AgregarProducto(inventario[n - 1], cant);
                        Console.WriteLine("Agregado.");
                    }
                    else
                        Console.WriteLine("Stock insuficiente.");
                    break;


                case 3:
                    MostrarResumen(pedido);
                    break;


                case 4:
                    pedido.Pagar();
                    Console.WriteLine("Pedido completado.");
                    break;


                case 5:
                    ejecutando = false;
                    break;
            }
        }
    }


    static void MostrarInventario(List<Producto> inventario)
    {
        for (int i = 0; i < inventario.Count; i++)
            Console.WriteLine($"{i + 1}. {inventario[i].Nombre} - ${inventario[i].Precio:F2} - Stock: {inventario[i].Stock}");
    }


    static void MostrarResumen(Pedido pedido)
    {
        Console.WriteLine($"Estado: {pedido.Estado}");
        Console.WriteLine($"Total: ${pedido.CalcularTotal(0.21m):F2}");
    }
}
}
}