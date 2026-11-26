using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    /// <summary>
    /// The light entities (LightType, LightSource, SunAndShadowSettings) round-trip through a VIM file and merge like the
    /// other entity tables: their relations are offset by the merge.
    /// </summary>
    [TestFixture]
    public static class LightEntityTests
    {
        private const string IesBufferName = "light/a1137_1300lm.ies";

        /// <summary>
        /// Writes a VIM with one view, one lighting family type, two fixtures (each with a light source), one IES asset and a sun.
        /// </summary>
        private static string WriteLightVim(string dir, string fileName)
        {
            var store = new ObjectModelStore();
            var omb = store.ObjectModelBuilder;

            var bimDocument = new BimDocument { Name = "Lights", Title = "Lights" };
            var bimDocumentElement = omb.ElementBuilder.Add(bimDocument.CreateParameterHolderElement()).Entity;
            bimDocument._Element.Index = bimDocumentElement.Index;
            omb.BimDocumentBuilder.Add(bimDocument);

            var viewElement = omb.ElementBuilder.Add(new Element { Id = 10, Name = "{3D}", UniqueId = "view" }).Entity;
            var view = new Format.ObjectModel.View { Title = "{3D}", ViewType = "ThreeD" };
            view._Element.Index = viewElement.Index;
            omb.ViewBuilder.Add(view);

            var typeElement = omb.ElementBuilder.Add(new Element { Id = 20, Name = "Downlight", UniqueId = "type" }).Entity;
            var familyType = new FamilyType();
            familyType._Element.Index = typeElement.Index;
            omb.FamilyTypeBuilder.Add(familyType);

            var fixture1 = omb.ElementBuilder.Add(new Element { Id = 30, Name = "Downlight", UniqueId = "fixture1" }).Entity;
            var fixture2 = omb.ElementBuilder.Add(new Element { Id = 31, Name = "Downlight", UniqueId = "fixture2" }).Entity;

            var asset = omb.AssetBuilder.Add(new Asset { BufferName = IesBufferName }).Entity;

            var lightType = new LightType
            {
                LightShapeStyle = (int) LightShapeStyle.Circle,
                EmitDiameter = 0.5,
                LightDistributionStyle = (int) LightDistributionStyle.PhotometricWeb,
                TiltAngle = 1.5708,
                PhotometricWebFileName = "A1137 1300Lm.IES",
                InitialIntensityType = (int) InitialIntensityType.Flux,
                Flux = 1300,
                InitialColorTemperature = 3000,
                ColorFilter = new DVector3(1, 0.9, 0.8),
                LossFactor = 0.85,
                DimmingColor = (int) LightDimmingColor.Incandescent,
            };
            lightType._PhotometricWebFile.Index = asset.Index;
            lightType._FamilyType.Index = familyType.Index;
            omb.LightTypeBuilder.Add(lightType);

            foreach (var (fixture, x) in new[] { (fixture1, 1.0), (fixture2, 2.0) })
            {
                var lightSource = new LightSource
                {
                    Origin = new DVector3(x, 2, 9),
                    BasisX = new DVector3(1, 0, 0),
                    BasisY = new DVector3(0, 1, 0),
                    BasisZ = new DVector3(0, 0, 1),
                    HasLightSourceTransform = true,
                    IsOn = x < 1.5,
                };
                lightSource._Element.Index = fixture.Index;
                lightSource._LightType.Index = lightType.Index;
                omb.LightSourceBuilder.Add(lightSource);
            }

            var sun = new SunAndShadowSettings
            {
                SunAndShadowType = "StillImage",
                UsesDST = true,
                ActiveFrameTime = "2026-06-21T12:00:00",
                StartDateAndTime = "2026-06-21T12:00:00",
                EndDateAndTime = "2026-06-21T12:00:00",
                ActiveFrame = 1,
                NumberOfFrames = 1,
                Altitude = 1.2,
                Azimuth = 3.1,
            };
            sun._View.Index = view.Index;
            omb.SunAndShadowSettingsBuilder.Add(sun);

            var documentBuilder = store.ToDocumentBuilder(nameof(LightEntityTests), "0.0.0");
            documentBuilder.Assets[IesBufferName] = Encoding.UTF8.GetBytes("IESNA:LM-63-2002\n");

            var path = Path.Combine(dir, fileName);
            documentBuilder.Write(path);
            return path;
        }

        [Test]
        public static void TestLightEntitiesRoundTrip()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();
            var path = WriteLightVim(dir, "lights.vim");

            var vim = VimScene.LoadVim(path);
            vim.Validate();
            var dm = vim.DocumentModel;

            Assert.AreEqual(SchemaVersion.Current.ToString(), vim.Document.Header.Schema.ToString());
            Assert.AreEqual(1, dm.NumLightType);
            Assert.AreEqual(2, dm.NumLightSource);
            Assert.AreEqual(1, dm.NumSunAndShadowSettings);

            var lightType = dm.GetLightType(0);
            Assert.AreEqual(LightShapeStyle.Circle, lightType.GetLightShapeStyle());
            Assert.AreEqual(LightDistributionStyle.PhotometricWeb, lightType.GetLightDistributionStyle());
            Assert.AreEqual(InitialIntensityType.Flux, lightType.GetInitialIntensityType());
            Assert.AreEqual(LightDimmingColor.Incandescent, lightType.GetDimmingColor());
            Assert.AreEqual(1300, lightType.Flux);
            Assert.AreEqual(1.5708, lightType.TiltAngle);
            Assert.AreEqual(new DVector3(1, 0.9, 0.8), lightType.ColorFilter);
            Assert.AreEqual("A1137 1300Lm.IES", lightType.PhotometricWebFileName);
            Assert.AreEqual(IesBufferName, lightType.PhotometricWebFile.BufferName);
            Assert.AreEqual("Downlight", lightType.FamilyType.Element.Name);
            Assert.IsTrue(vim.Document.Assets.Keys.ToEnumerable().Contains(IesBufferName));

            var lightSource = dm.GetLightSource(1);
            Assert.AreEqual(new DVector3(2, 2, 9), lightSource.Origin);
            Assert.AreEqual(new DVector3(0, 0, 1), lightSource.BasisZ);
            Assert.IsTrue(lightSource.HasLightSourceTransform);
            Assert.IsFalse(lightSource.IsOn);
            Assert.AreEqual("fixture2", lightSource.Element.UniqueId);
            Assert.AreEqual(0, lightSource.LightType.Index);

            var sun = dm.GetSunAndShadowSettings(0);
            Assert.AreEqual("StillImage", sun.SunAndShadowType);
            Assert.AreEqual(3.1, sun.Azimuth);
            Assert.AreEqual("{3D}", sun.View.Title);
        }

        [Test]
        public static void TestLightEntitiesMerge()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();
            var path1 = WriteLightVim(dir, "lights1.vim");
            var path2 = WriteLightVim(dir, "lights2.vim");
            var mergedPath = Path.Combine(dir, "merged.vim");

            MergeService.MergeVimFiles(
                new MergeConfigFiles(new[] { (path1, Matrix4x4.Identity), (path2, Matrix4x4.Identity) }, mergedPath),
                new MergeConfigOptions { GeneratorString = ctx.TestName, VersionString = "0.0.0", DeduplicateEntities = false, KeepBimData = true });

            var merged = VimScene.LoadVim(mergedPath);
            merged.Validate();
            var dm = merged.DocumentModel;
            var source = VimScene.LoadVim(path1).DocumentModel;
            var numElementsPerFile = source.NumElement;
            var fixture2ElementIndex = source.GetLightSource(1).Element.Index;

            Assert.AreEqual(2, dm.NumLightType);
            Assert.AreEqual(4, dm.NumLightSource);
            Assert.AreEqual(2, dm.NumSunAndShadowSettings);

            // The second file's rows point at the second file's entities.
            var lightSource = dm.GetLightSource(3);
            Assert.AreEqual(1, lightSource.LightType.Index);
            Assert.AreEqual(numElementsPerFile + fixture2ElementIndex, lightSource.Element.Index);
            Assert.AreEqual("fixture2", lightSource.Element.UniqueId);

            var lightType = dm.GetLightType(1);
            Assert.AreEqual(1, lightType.PhotometricWebFile.Index);
            Assert.AreEqual(IesBufferName, lightType.PhotometricWebFile.BufferName);
            Assert.AreEqual(1, lightType.FamilyType.Index);

            Assert.AreEqual(1, dm.GetSunAndShadowSettings(1).View.Index);

            // The two files carry the same IES file under the same name: the merged file keeps one buffer.
            Assert.AreEqual(1, merged.Document.Assets.Keys.ToEnumerable().Count(k => k == IesBufferName));

            // With deduplication (the merge verb's default), the light sources stay distinct: they point at different elements.
            var dedupPath = Path.Combine(dir, "merged_dedup.vim");
            MergeService.MergeVimFiles(
                new MergeConfigFiles(new[] { (path1, Matrix4x4.Identity), (path2, Matrix4x4.Identity) }, dedupPath),
                new MergeConfigOptions { GeneratorString = ctx.TestName, VersionString = "0.0.0", DeduplicateEntities = true, KeepBimData = true });
            var dedup = VimScene.LoadVim(dedupPath);
            dedup.Validate();
            Assert.AreEqual(4, dedup.DocumentModel.NumLightSource);
            Assert.IsTrue(dedup.DocumentModel.LightSourceList.ToEnumerable().All(ls => ls.LightType != null && ls.Element != null));
        }
    }
}
