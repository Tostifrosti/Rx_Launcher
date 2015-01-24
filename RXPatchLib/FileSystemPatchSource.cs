﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RXPatchLib
{
    public class FileSystemPatchSource : IPatchSource
    {
        string RootPath;

        public FileSystemPatchSource(string rootPath)
        {
            RootPath = rootPath;
        }

        public string GetSystemPath(string subPath)
        {
            return Path.Combine(RootPath, subPath);
        }

        public async Task Load(string subPath, string hash, CancellationToken cancellationToken)
        {
            if (hash != null && await SHA1.GetFileHashAsync(GetSystemPath(subPath)) != hash)
                throw new PatchSourceLoadException(subPath, hash);
        }
    }
}
