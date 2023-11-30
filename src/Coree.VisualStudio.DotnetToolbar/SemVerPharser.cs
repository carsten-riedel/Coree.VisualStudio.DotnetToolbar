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
            get
            {
                if (Revision > -1)
                {
                    return $@"{Major}.{Minor}.{Patch}.{Revision}";
                }
                else
                {
                    return $@"{Major}.{Minor}.{Patch}";
                }
            }
        }

        public ulong Major { get; set; }
        public ulong Minor { get; set; }
        public ulong Patch { get; set; }
        public long Revision { get; set; } = -1;
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

            string pattern = @"^(?<packagename>[a-zA-Z0-9_]+(?:\.[a-zA-Z0-9_]+)*?)\.(?<major>0|[1-9][0-9]{0,4})\.(?<minor>0|[1-9][0-9]{0,4})\.(?<patch>0|[1-9][0-9]{0,4})\.?(?<revision>0|[1-9][0-9]{0,4})?(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?<extension>.nupkg)$";

            try
            {
                Match match = Regex.Match(filename, pattern);

                var item = new SemVerFileInfo()
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
                };

                if (match.Groups["revision"].Value == String.Empty)
                {
                    item.Revision = -1;
                }
                else
                {
                    item.Revision = System.Convert.ToInt64(match.Groups["revision"].Value);
                }

                semVerFileInfos.Add(item);
            }
            catch (Exception ex)
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

        internal void OrderMajorMinorPatchRevisionLastWriteTime()
        {
            //semVerFileInfos = semVerFileInfos.OrderByDescending(e => e.Major).ThenByDescending(e => e.Minor).ThenByDescending(e => e.Patch).ThenByDescending(e => e.Revision).OrderByDescending(e => e.LastWriteTime).ToList();
            semVerFileInfos = semVerFileInfos.OrderByDescending(e => e.Major).ThenByDescending(e => e.Minor).ThenByDescending(e => e.Patch).ThenByDescending(e => e.Revision).ThenByDescending(e => e.LastWriteTime).ToList();
        }
    }

    public enum PackageSourceTypes
    {
        none,
        @virtual,
        nugetConfig
    }

    public enum PackageCategoryTypes
    {
        none,
        remote,
        local,
        localMissing,
    }

    public class PackageSources
    {
        [Display(Order = 0, Name = "Key")]
        public string Key { get; set; } = string.Empty;

        [Display(Order = 1, Name = "Value")]
        public string Value { get; set; } = string.Empty;

        [Display(Order = 2, Name = "Source")]
        public PackageSourceTypes SourceType { get; set; } = PackageSourceTypes.none;

        [Display(Order = 3, Name = "Type")]
        public PackageCategoryTypes Type { get; set; } = PackageCategoryTypes.none;

    }
}