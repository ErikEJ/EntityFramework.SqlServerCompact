﻿using System.IO;
using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;

namespace EntityFramework7.SqlServerCompact40.Design.FunctionalTest
{
    public class InMemoryFileService : IFileService
    {
        // maps directory name to a dictionary mapping file name to file contents
        private Dictionary<string, Dictionary<string, string>> _nameToContentMap
            = new Dictionary<string, Dictionary<string, string>>();

        public virtual bool DirectoryExists(string directoryName)
        {
            Dictionary<string, string> filesMap;
            return _nameToContentMap.TryGetValue(directoryName, out filesMap);
        }

        public virtual bool FileExists(string directoryName, string fileName)
        {
            Dictionary<string, string> filesMap;
            if (!_nameToContentMap.TryGetValue(directoryName, out filesMap))
            {
                return false;
            }

            string _;
            return filesMap.TryGetValue(fileName, out _);
        }

        public virtual bool IsFileReadOnly(string outputDirectoryName, string outputFileName)
        {
            return false;
        }

        public virtual string RetrieveFileContents(string directoryName, string fileName)
        {
            Dictionary<string, string> filesMap;
            if (!_nameToContentMap.TryGetValue(directoryName, out filesMap))
            {
                throw new DirectoryNotFoundException("Could not find directory " + directoryName);
            }

            string contents;
            if (!filesMap.TryGetValue(fileName, out contents))
            {
                throw new FileNotFoundException("Could not find file " + Path.Combine(directoryName, fileName));
            }

            return contents;
        }

        public virtual string OutputFile(string directoryName,
            string fileName, string contents)
        {
            Dictionary<string, string> filesMap;
            if (!_nameToContentMap.TryGetValue(directoryName, out filesMap))
            {
                filesMap = new Dictionary<string, string>();
                _nameToContentMap[directoryName] = filesMap;
            }

            filesMap[fileName] = contents;

            return Path.Combine(directoryName, fileName);
        }
    }
}
