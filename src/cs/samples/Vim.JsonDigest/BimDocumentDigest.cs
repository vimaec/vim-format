using System.Collections.Generic;
using Vim.LinqArray;

namespace Vim.JsonDigest
{
    public class BimDocumentDigest
    {
        /// <summary>
        /// The index of the BIM document in the VIM scene.
        /// </summary>
        public int VimIndex { get; set; }

        /// <summary>
        /// The BIM document file path.
        /// </summary>
        public string PathName { get; set; }

        /// <summary>
        /// The BIM document name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The BIM document title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The BIM document guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The BIM document's number of times it was saved.
        /// </summary>
        public int NumSaves { get; set; }

        /// <summary>
        /// Determines whether the BIM document is linked from another document.
        /// </summary>
        public bool IsLinked { get; set; }

        /// <summary>
        /// Determines whether the BIM document is detached
        /// </summary>
        public bool IsDetached { get; set; }

        /// <summary>
        /// Determines whether the BIM document is workshared
        /// </summary>
        public bool IsWorkshared { get; set; }

        /// <summary>
        /// The BIM document's latitude
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The BIM document's longitude
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The BIM document's time zone
        /// </summary>
        public double TimeZone { get; set; }

        /// <summary>
        /// The BIM document's place name
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// The BIM document's weather station name
        /// </summary>
        public string WeatherStationName { get; set; }

        /// <summary>
        /// The BIM document's elevation
        /// </summary>
        public double Elevation { get; set; }

        /// <summary>
        /// The BIM document's project location
        /// </summary>
        public string ProjectLocation { get; set; }

        /// <summary>
        /// The BIM document's issue date
        /// </summary>
        public string IssueDate { get; set; }

        /// <summary>
        /// The BIM document's status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The BIM document's client name
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// The BIM document's address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The BIM document's number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The BIM document's author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The BIM document's building name
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// The BIM document's organization name
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// The BIM document's organization description
        /// </summary>
        public string OrganizationDescription { get; set; }

        /// <summary>
        /// The BIM document's product.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// The BIM document's version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The BIM document's user
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Determines whether the BIM document is metric or imperial.
        /// </summary>
        public bool IsMetric { get; set; }

        /// <summary>
        /// The reference to the element which contains the parameters for this BIM document.
        /// </summary>
        public int Ref_ElementDigest_VimIndex { get; set; }

        /// <summary>
        /// Returns a collection of BIM document digests for each BIM document in the given VIM scene
        /// </summary>
        public static IEnumerable<BimDocumentDigest> GetBimDocumentDigestCollection(VimScene vimScene)
            => vimScene.DocumentModel.BimDocumentList.Select(b =>
            {
                var bimDocumentElement = b.Element;

                return new BimDocumentDigest
                {
                    VimIndex = b.Index,
                    Name = b.Name,
                    Title = b.Title,
                    Guid = b.Guid,
                    IsMetric = b.IsMetric,
                    NumSaves = b.NumSaves,
                    IsLinked = b.IsLinked,
                    IsDetached = b.IsDetached,
                    IsWorkshared = b.IsWorkshared,
                    PathName = b.PathName,
                    Latitude = b.Latitude,
                    Longitude = b.Longitude,
                    TimeZone = b.TimeZone,
                    PlaceName = b.PlaceName,
                    WeatherStationName = b.WeatherStationName,
                    Elevation = b.Elevation,
                    ProjectLocation = b.ProjectLocation,
                    IssueDate = b.IssueDate,
                    Status = b.Status,
                    ClientName = b.ClientName,
                    Address = b.Address,
                    Number = b.Number,
                    Author = b.Author,
                    BuildingName = b.BuildingName,
                    OrganizationName = b.OrganizationName,
                    OrganizationDescription = b.OrganizationDescription,
                    Product = b.Product,
                    Version = b.Version,
                    User = b.User,
                    Ref_ElementDigest_VimIndex = bimDocumentElement.Index,
                };
            }).ToEnumerable();
    }
}
