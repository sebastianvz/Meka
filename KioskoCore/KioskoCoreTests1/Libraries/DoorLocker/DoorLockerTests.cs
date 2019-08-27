using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kiosko.Libraries.DoorLocker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiosko.Models;
namespace Kiosko.Libraries.DoorLocker.Tests
{
    [TestClass()]
    public class DoorLockerTests
    {
        [TestMethod()]
        public void DoorLockerTest()
        {
            string COMPort = "COM9";
            KioskoHardware.DoorLocker doorLocker = Helpers.Utilities.GetDoorLockerByName("ContainerReceiver");
            DoorLocker door = new DoorLocker(doorLocker);
            door.IsOpen(0x01);

            door.Open(0x01);
            door.Open(0x05);
            door.Open(0x03);
            door.Open(0x04);
            door.Close();

        }
    }
}