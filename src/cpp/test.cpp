#include <iostream>
#include "object-model.h"

bool inline operator==(const Vim::Vector3& a, const Vim::Vector3& b)
{
    return abs(a.x - b.x) < 1e-10 &&
           abs(a.y - b.y) < 1e-10 &&
           abs(a.z - b.z) < 1e-10;
}

bool inline operator!=(const Vim::Vector3& a, const Vim::Vector3& b)
{
    return !(a == b);
}

std::ostream& operator<< (std::ostream& stream, const Vim::Vector3& vector)
{
    stream << "(X: " << vector.x
           << ", Y: " << vector.y
           << ", Z: " << vector.z << ")";
    return stream;
}

template<typename T>
void test(std::string message, T actual, T expected)
{
    if (actual != expected)
        std::cout << message << std::endl
                  << "Expected: " << expected << ". Actual: " << actual << std::endl;

    assert(actual == expected);
}

template<typename T>
void test(std::string message, const std::vector<T>& actual, const std::vector<T>& expected)
{
    assert(actual.size() == expected.size());

    for (int i = 0; i < actual.size(); ++i)
    {
        if (actual[i] != expected[i])
            std::cout << message << std::endl
                      << "Expected[" << i << "]: " << expected[i]
                      << ". Actual[" << i << "]: " << actual[i] << std::endl;

        assert(actual[i] == expected[i]);
    }
}

void testElement(const Vim::DocumentModel& model)
{
    assert(model.mElement);

    test("Element count", model.mElement->GetCount(), 4464);
    test("Element 0 ID", model.mElement->GetId(0), -1);
    test("Element 1 ID", model.mElement->GetId(1), 1222722);
    test("Element 2 ID", model.mElement->GetId(2), 32440);
    test("Element 3 ID", model.mElement->GetId(3), 118390);

    test("Element 30 Index", model.mElement->Get(30)->mIndex, 30);
    test("Element 30 ID", model.mElement->Get(30)->mId, 374011);
    test("Element 30 Name", *model.mElement->Get(30)->mName, std::string("GWB on Mtl. Stud"));
    test("Element 30 UniqueID", *model.mElement->Get(30)->mUniqueId, std::string("3ae43fb5-6797-479b-ac14-3436f35a7178-0005b4fb"));
    test("Element 30 FamilyNome", *model.mElement->Get(30)->mFamilyName, std::string("Compound Ceiling"));
    test("Element 30 IsPinned", model.mElement->Get(30)->mIsPinned, false);
    test("Element 30 LevelIndex", model.mElement->Get(30)->mLevelIndex, 6);
    test("Element 30 PhaseCreatedIndex", model.mElement->Get(30)->mPhaseCreatedIndex, 1);
    test("Element 30 PhaseDemolishedIndex", model.mElement->Get(30)->mPhaseDemolishedIndex, -1);
    test("Element 30 CategoryIndex", model.mElement->Get(30)->mCategoryIndex, 5);
    test("Element 30 WorksetIndex", model.mElement->Get(30)->mWorksetIndex, 0);
    test("Element 30 DesignOptionIndex", model.mElement->Get(30)->mDesignOptionIndex, -1);
    test("Element 30 OwnerViewIndex", model.mElement->Get(30)->mOwnerViewIndex, -1);
    test("Element 30 GroupIndex", model.mElement->Get(30)->mGroupIndex, -1);
    test("Element 30 AssemblyInstanceIndex", model.mElement->Get(30)->mAssemblyInstanceIndex, -1);
    test("Element 30 BimDocumentIndex", model.mElement->Get(30)->mBimDocumentIndex, 0);
    test("Element 30 RoomIndex", model.mElement->Get(30)->mRoomIndex, -1);
    test("Element 30 RoomIndex", model.mElement->Get(30)->mLocation, Vim::Vector3{0, 0, 0});

    std::cout << "Get element test: OK" << std::endl;
}

void testElementIds(const Vim::DocumentModel& model)
{
    assert(model.mElement);

    auto ids = model.mElement->GetAllId();

    std::vector<int> expectedIds = { -1, 1222722, 32440, 118390, 174750, 18438, 355500, 185913, 9946, 182664 };
    std::vector<int> actualIds = *ids;
    actualIds.resize(10);

    test("Element IDs count", (int) ids->size(), 4464);
    test("Element IDs", actualIds, expectedIds);

    std::cout << "Get element IDs test: OK" << std::endl;
}

void testGetAll(const Vim::DocumentModel& model)
{
    assert(model.mLevel);

    auto levels = model.mLevel->GetAll();

    test("Levels count", (int) levels->size(), 12);

    std::cout << "Get-all test: OK" << std::endl;
}

int main(int num, char** args)
{
    auto path = std::string(args[0]);
    path = path.erase(path.find_last_of('/')) + "/data/Wolford_Residence.r2023.vim";

    std::cout << "Testing VIM file: " << path << std::endl;

    Vim::VimScene scene;
    scene.ReadFile(path);

    Vim::DocumentModel model(scene);

    testElement(model);
    testElementIds(model);
    testGetAll(model);

    return 0;
}