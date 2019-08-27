using KioskoAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Kiosko.Models.Common;
using static Kiosko.Models.InventarioCash;

namespace Kiosko.Models
{
    public class ComClass
    {

        public function funciones { get; set; }
        public int Value { get; set; }

        public List<ServiceStatus> DeviceStatus { get; set; }
        public machine_status status
        {
            get; set;
        }
        public Efectivo Efect { get; set; }
        public status_cash result { get; set; }

        public List<Efectivo> Inventario { get; set; }

        public User user { get; set; }

        /// <summary>
        /// Operaciones de comunicacion entre KioskoCore y payment service, cada una de las opciones genera un resultado diferente.
        /// </summary>
        public enum function
        {
            cash_handling,
            operation_status,
            system_status,
            reset,
            get_devices_status,
            add_money,
            set_money, 
            get_money,
            remove_money

        }

        /// <summary>
        /// operaciones directamente ligadas a la base de datos (kiosko_transaction_type), segun el tipo de operacion
        /// En algunos casos, como son Recarga y retiro hay que enviar el usuario que genero el evento
        /// 
        /// </summary>
        public enum operation_types
        {
            purchase,
            withdrawal,
            load,
            boxremoval,
            setmoney,
            update


        }
        public enum machine_status
        {
            idle,
            receiving,
            dispensing,
            error

        }

        public enum status_cash
        {
            ok,
            time_out,
            operation_error,
            fail,
            waiting

        }
    }

}