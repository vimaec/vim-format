using CsvHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Vim.LinqArray;
using Vim.Format.ObjectModel;
using Vim.Util;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class TrainingSetTests
{
    public record QuestionRecord(string Question, string Answer);

    public static void WriteQuestions(string filePath, IEnumerable<QuestionRecord> questions)
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(questions);
    }

    [Test, Explicit("Local")]
    public static void GenerateTrainingSet()
    {
        var vimFilePath = @"path/to/file.vim"; // TODO: Replace with your VIM file
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimScene = VimScene.LoadVim(vimFilePath);

        var dm = vimScene.DocumentModel;
        var elements = dm.ElementList.Select(e => dm.GetElementInfo(e)).ToArray();

        var questions = new List<QuestionRecord>();

        void AddQ(string q, string a)
            => questions.Add(new(q, a));

        void BatchAddQ(string initialQuestion, Func<ElementInfo, string> groupFunc, Func<string, string> getQuestion)
        {
            var groups = elements
                .GroupBy(groupFunc)
                .Where(g => !string.IsNullOrWhiteSpace(g.Key))
                .ToArray();

            AddQ(initialQuestion, groups.Length.ToString());

            foreach (var g in groups)
            {
                var answer = g.Count().ToString();
                var groupName = g.Key;
                AddQ(getQuestion(groupName), answer);
            }
        }

        // ELEMENTS
        BatchAddQ("How many categories are there?", e => e.Category?.Name, x => $"How many elements are there in the {x} category?");
        BatchAddQ("How many families are there?", e => e.FamilyName, x => $"How many elements are in the {x} family?");
        BatchAddQ("How many family types are there?", e => e.FamilyTypeName, x => $"How many elements are in the {x} family type?");
        BatchAddQ("How many levels are there?", e => e.LevelName, x => $"How many elements are on level {x}?");

        // LEVELS
        var levels = dm.LevelList.ToArray();
        foreach (var level in levels)
        {
            AddQ($"What is the elevation of level {level}?", level.Elevation.ToString());
        }

        // ROOMS
        var rooms = dm.RoomList.Select(r => (r, dm.GetElementInfo(r))).ToArray();
        AddQ("How many rooms are there?", rooms.Length.ToString());

        var roomToElementMap = elements.Where(e => e.Element.Room != null).ToDictionaryOfLists(e => e.Element.Room.Number);
        foreach (var (room, roomElement) in rooms)
        {
            AddQ($"What is the area of room number {room.Number} in square feet?", room.Area.ToString());
            AddQ($"What is the volume of room number {room.Number} in cubic feet?", room.Volume.ToString());

            var roomElements = roomToElementMap.TryGetValue(room.Number, out var list) ? list : new List<ElementInfo>();
            var roomElementsExcludingSelf = roomElements.Where(e => e.ElementIndex != roomElement.ElementIndex).ToArray();
            AddQ($"How many elements are there in room {room.Number}?", roomElementsExcludingSelf.Length.ToString());

            var roomFurnitureElements = roomElementsExcludingSelf.Where(e => e.FamilyName == "Furniture");
            AddQ($"How many furniture elements are there in room {room.Number}?", roomFurnitureElements.Count().ToString());
        }

        var roomGroups = rooms
            .GroupBy(t => t.Item2.LevelName)
            .ToArray();
        foreach (var g in roomGroups)
        {
            var levelName = g.Key;
            var roomCount = g.Count().ToString();
            AddQ($"How many rooms are on level {levelName}?", roomCount);
            AddQ($"What is the total square footage of the rooms on level {levelName}?", g.Sum(t => t.r.Area).ToString());
        }

        // Write the training set out as a CSV file
        var csvFilePath = Path.Combine(dir, "Training_Set.csv");
        WriteQuestions(csvFilePath, questions);
    }
}
