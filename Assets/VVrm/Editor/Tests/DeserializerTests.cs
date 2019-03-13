//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using VJson.Schema;

namespace VVrm.UnitTests
{
    public class DeserializerTests
    {
        [Test]
        [TestCaseSource("VrmArgs")]
        public void FromVrmTest(string[] modelPath, Action<VGltf.ResourcesStore> assertVrm)
        {
            VVrm.ExtensionRegistrator.Register();

            var path = modelPath.Aggregate("SampleModels", (b, p) => Path.Combine(b, p));
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var c = VGltf.GltfContainer.FromGlb(fs);

                var schema = JsonSchemaAttribute.CreateFromClass<VGltf.Types.Gltf>();
                var ex = schema.Validate(c.Gltf);
                Assert.Null(ex);

                var store = new VGltf.ResourcesStore(c.Gltf, c.Buffer, new VGltf.ResourceLoaderFromStorage());
                assertVrm(store);
            }
        }

        public static object[] VrmArgs = {
            new object[] {
                new string[] {"Alicia", "VRM", "AliciaSolid.vrm"},
                new Action<VGltf.ResourcesStore>(
                    (store) => {
                        var vrm = store.Gltf.GetExtension<V0_x.Types.Vrm>(V0_x.Types.Vrm.ExtensionName);
                        Assert.NotNull(vrm);
                    })
            },

            new object[] {
                new string[] {"ShinchokuRobo", "shinchoku_robo.vrm"},
                new Action<VGltf.ResourcesStore>(
                    (store) => {
                        var vrm = store.Gltf.GetExtension<V0_x.Types.Vrm>(V0_x.Types.Vrm.ExtensionName);
                        Assert.NotNull(vrm);
                    })
            },
        };
    }
}
