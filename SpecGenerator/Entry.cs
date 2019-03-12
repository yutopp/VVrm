//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using VJson;
using VJson.Schema;

namespace SpecGenerator
{
    class Entry
    {
        static void Main(string[] args)
        {
            // Create all JsonSchemas related to VRM from the root class.
            var reg = new JsonSchemaRegistory();
            var _schama = JsonSchemaAttribute.CreateFromClass<VVrm.V0_x.Types.Vrm>(reg);

            if (args.Length != 1)
            {
                Console.Error.WriteLine("Error: Not enough arguments.");
                ShowUsage();
                System.Environment.Exit(1);
            }

            var outDir = args[0];
            if (!Directory.Exists(outDir))
            {
                Console.Error.WriteLine("Error: Directory not exists: Path = " + outDir);
                ShowUsage();
                System.Environment.Exit(1);
            }

            var indent = 4;

            var s = new JsonSerializer(typeof(JsonSchemaAttribute));
            foreach (var id in reg.GetRegisteredIDs())
            {
                var schema = reg.Resolve(id);
                if (string.IsNullOrEmpty(schema.Title))
                {
                    Console.Error.WriteLine("Schemas of VRM must have title: Id = " + id);
                }

                var fileName = string.Format("{0}.schema.json", schema.Title);
                var path = Path.Combine(outDir, fileName);

                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    s.Serialize(fs, schema, indent);
                }
            }
        }

        static void ShowUsage()
        {
            Console.Error.WriteLine("Usage: ");
            Console.Error.WriteLine("  {0} {1}",
                                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                                    "<OutDir>");
        }
    }
}
