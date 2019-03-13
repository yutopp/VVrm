//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

namespace VVrm
{
    public static class ExtensionRegistrator
    {
        static ExtensionRegistrator()
        {
            // Register VRM 0.x as glTF extensions
            VGltf.Types.GltfProperty.RegisterExtension(V0_x.Types.Vrm.ExtensionName, typeof(V0_x.Types.Vrm));
        }

        /// <summary>
        ///   VRM extensions are registered only once.
        /// </summary>
        public static void Register()
        {
            // Setup() will be called once
        }
    }
}
