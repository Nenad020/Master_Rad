﻿using Common.Interfaces.SCADA.Access;
using ScadaDbAccess.Access;
using ScadaDbAccess.Model;
using ScadaService;
using System;

namespace Scada
{
	class Program
	{
        private static IScadaDbAccess<CoilsAddress> coilAddressesAccess;

        private static IScadaDbAccess<HoldingRegistersAddress> holdingRegistersAddressAccess;

        private static ScadaModel scadaModel;

        private static Poller poller;

        public static void Main()
        {
            WriteServiceName();
            coilAddressesAccess = new CoilsAddressAccess();
            holdingRegistersAddressAccess = new HoldingRegistersAddressAccess();

            scadaModel = new ScadaModel(coilAddressesAccess, holdingRegistersAddressAccess);
            scadaModel.Initialize();

            poller = new Poller(scadaModel, 3000, coilAddressesAccess);
            scadaModel.UsedAddressesUpdated += (sender, args) => poller.UpdateAddressesToPoll();

            poller.StartPolling();
            Console.ReadLine();
        }

        private static void WriteServiceName()
        {
            var serviceName = "SCADA";

            Console.Title = serviceName;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(serviceName);
            Console.ResetColor();
        }
    }
}
