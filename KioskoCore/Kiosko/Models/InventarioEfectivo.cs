using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static Kiosko.Models.InventarioCash;

namespace Kiosko.Models
{
    public class InventarioEfectivo
    {
        List<Efectivo> Inventario;

        public InventarioEfectivo()
        {
            LoadConfig();
        }
        public List<Efectivo> GetInventario()
        {
            return Inventario;
        }

        public void UpdateInventory(string location, int valor, TipoOperacion optype, int inventario)
        {

            switch (optype)
            {
                case TipoOperacion.reemplazar:
                    Inventario.Where(c => c.Location == location && c.Value == valor).FirstOrDefault();
                    break;

                case TipoOperacion.sumar:
                    Inventario.Where(c => c.Location == location && c.Value == valor).FirstOrDefault().Inventory += inventario;
                    break;
                case TipoOperacion.restar:
                    Inventario.Where(c => c.Location == location && c.Value == valor).FirstOrDefault().Inventory -= inventario;
                    break;
            }
            SaveInventory(Inventario);

        }


        private void LoadConfig()
        {
            var file = Properties.Settings.Default.INVENTORY_CONFIG_PATH;

            if (!File.Exists(file))
            {
                CreateInventoryFile();
            }
            Inventario = Helpers.Utilities.ReadFile<List<Efectivo>>(file);

        }

        private void CreateInventoryFile()
        {
            Inventario = new List<Efectivo>();
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Coin, Location = InventarioCash.Location.SMARTHOPPER, Inventory = 33, Value = 50 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Coin, Location = InventarioCash.Location.SMARTHOPPER, Inventory = 0, Value = 100 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Coin, Location = InventarioCash.Location.SMARTHOPPER, Inventory = 0, Value = 200 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Coin, Location = InventarioCash.Location.SMARTHOPPER, Inventory = 0, Value = 500 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Coin, Location = InventarioCash.Location.SMARTHOPPER, Inventory = 0, Value = 1000 });



            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.JCM, Inventory = 0, Value = 1000 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.JCM, Inventory = 0, Value = 2000 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.JCM, Inventory = 0, Value = 5000 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.JCM, Inventory = 0, Value = 10000 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.JCM, Inventory = 0, Value = 20000 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.JCM, Inventory = 0, Value = 50000 });


            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.F56, Inventory = 0, Value = 2000 });
            Inventario.Add(new Efectivo() { Type = InventarioCash.Type.Bill, Location = InventarioCash.Location.F56, Inventory = 0, Value = 5000 });


            SaveInventory(Inventario);
        }

        private void SaveInventory(List<Efectivo> Inventory)
        {
            //  Thread.Sleep(1000);
            Helpers.Utilities.WriteJson(Properties.Settings.Default.INVENTORY_CONFIG_PATH, Inventory);
            Services.CubiQManagerService.PostKioskoCashInventory(Inventory);
        }


        public void UpdateInventario(List<Efectivo> _inv)
        {
            Inventario = _inv;

        }


    }
}