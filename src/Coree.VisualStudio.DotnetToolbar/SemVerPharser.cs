using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Coree.VisualStudio.DotnetToolbar
{
    public class SemVerFileInfo
    {
        public string Location { get; set; }

        [Display(Order = 0, Name = "Filename")]
        public string FileName { get; set; }

        public string Directory { get; set; }
        public bool IsValid { get; set; }
        public string PackageName { get; set; }

        [Display(Order = 1)]
        public string Version
        {
            get { return $@"{Major}.{Minor}.{Patch}"; }
        }

        public ulong Major { get; set; }
        public ulong Minor { get; set; }
        public ulong Patch { get; set; }
        public string PreRelease { get; set; }
        public string BuildMetadata { get; set; }
        public string Extension { get; set; }

        [Display(Order = 2, Name = "Date modified")]
        public DateTime LastWriteTime { get; set; }
    }

    public class SemVerPharser
    {
        public List<SemVerFileInfo> semVerFileInfos { get; set; } = new List<SemVerFileInfo>();

        public SemVerFileInfo[] SemVerFileInfosArray
        {
            get { return semVerFileInfos.ToArray(); }
        }

        public SemVerPharser()
        {
        }

        public SemVerPharser(string location)
        {
            Add(location);
        }

        public SemVerPharser(string[] locations)
        {
            AddRange(locations);
        }

        public SemVerPharser(List<string> locations)
        {
            AddRange(locations.ToArray());
        }

        internal void Add(string location)
        {
            var filename = System.IO.Path.GetFileName(location);
            var directory = System.IO.Path.GetDirectoryName(location);
            var fileInfo = new System.IO.FileInfo(location);

            string pattern = @"^(?<packagename>[a-zA-Z0-9.]+)\.(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?(?<extension>.nupkg)$";

            try
            {
                Match match = Regex.Match(filename, pattern);

                semVerFileInfos.Add(new SemVerFileInfo()
                {
                    Location = location,
                    FileName = filename,
                    Directory = directory,
                    IsValid = true,
                    PackageName = match.Groups["packagename"].Value,
                    Major = System.Convert.ToUInt64(match.Groups["major"].Value),
                    Minor = System.Convert.ToUInt64(match.Groups["minor"].Value),
                    Patch = System.Convert.ToUInt64(match.Groups["patch"].Value),
                    PreRelease = match.Groups["prerelease"].Value,
                    BuildMetadata = match.Groups["buildmetadata"].Value,
                    Extension = match.Groups["extension"].Value,
                    LastWriteTime = fileInfo.LastWriteTime,
                });
            }
            catch (Exception)
            {
                semVerFileInfos.Add(new SemVerFileInfo() { IsValid = false, Location = location, FileName = filename, Directory = directory, LastWriteTime = fileInfo.LastWriteTime });
            }
        }

        internal void AddRange(string[] locations)
        {
            foreach (var item in locations)
            {
                Add(item);
            }
        }

        internal void OrderMajorMinorPatchLastWriteTimeUtc()
        {
            semVerFileInfos = semVerFileInfos.OrderByDescending(e => e.Major).ThenByDescending(e => e.Minor).ThenByDescending(e => e.Patch).OrderByDescending(e => e.LastWriteTime).ToList();
        }
    }

    public enum PackageSource
    {
        none,
        @virtual,
        nugetConfig

    }

    public enum PackageTypes
    {
        none,
        remote,
        local,
    }

    public class PackageSources
    {
        [Display(Order = 0, Name = "Key")]
        public string Key { get; set; } = string.Empty;

        [Display(Order = 1, Name = "Value")]
        public string Value { get; set; } = string.Empty;

        [Display(Order = 3, Name = "Type")]
        public PackageTypes Type { get; set; } =  PackageTypes.none;

        [Display(Order = 2, Name = "Source")]
        public PackageSource Source { get; set; } = PackageSource.none;
    }
}