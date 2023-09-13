﻿using DB;
using ConsoleFrontend;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting prototype...");
        var db = new NotenDB("test.db", "src/infrastructure/database/sqlite/migrations");
        var sessionManager = new SessionManager(db);
        var operations = new OperationsController(db);
        var entrypoint = new ConsoleEntrypoint(sessionManager, operations);
        entrypoint.Start();
    }
}