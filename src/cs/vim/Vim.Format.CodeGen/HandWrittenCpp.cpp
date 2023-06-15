struct Vector2 { float x, y; };
struct Vector3 { float x, y, z; };
struct Vector4 { float x, y, z, w; };
struct AABox { Vector3 min, max; };
struct AABox2D { Vector2 min, max; };
struct AABox4D { Vector4 min, max; };
struct DVector2 { double x, y; };
struct DVector3 { double x, y, z; };
struct DVector4 { double x, y, z, w; };
struct DAABox { DVector3 min, max; };
struct DAABox2D { DVector2 min, max; };
struct DAABox4D { DVector4 min, max; };
struct Matrix4x4 { float m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44; };

typedef bfast::ByteRange* ByteRangePtr;

template<class T>
class Converter
{
public:
    virtual int GetSize() = 0;
    virtual int GetBytes() = 0;
    virtual const std::string* GetColumns() = 0;

    T ConvertFromArray(const bfast::byte* bytes)
    {
        T result;
        memcpy(&result, bytes, GetSize() * GetBytes());
        return result;
    }

    void ConvertFromColumns(T* dest, const ByteRangePtr* bytesArrays, int index)
    {
        for (int i = 0; i < GetSize(); i++)
        {
            memcpy(reinterpret_cast<char*>(dest) + GetBytes() * i, bytesArrays[i]->begin() + index * GetBytes(), GetBytes());
        }
    }

    bfast::byte* ConvertToArray(const T& value)
    {
        bfast::byte* result = new bfast::byte[sizeof(T)];
        memcpy(result, &value, sizeof(T));
        return result;
    }
};

class Vector2Converter: public Converter<Vector2>
{
private:
    const std::string* const COLUMNS = new std::string[2] { ".X", ".Y" };
public:
    int GetSize() { return 2; }
    int GetBytes() { return sizeof(float); }
    const std::string* GetColumns() { return COLUMNS; }

    ~Vector2Converter()
    {
        delete[] COLUMNS;
    }
};

class DVector2Converter: public Converter<DVector2>
{
private:
    const std::string* const COLUMNS = new std::string[2] { ".X", ".Y" };
public:
    int GetSize() { return 2; }
    int GetBytes() { return sizeof(double); }
    const std::string* GetColumns() { return COLUMNS; }

    ~DVector2Converter()
    {
        delete[] COLUMNS;
    }
};

class Vector3Converter: public Converter<Vector3>
{
private:
    const std::string* const COLUMNS = new std::string[3] { ".X", ".Y", ".Z" };
public:
    int GetSize() { return 3; }
    int GetBytes() { return sizeof(float); }
    const std::string* GetColumns() { return COLUMNS; }

    ~Vector3Converter()
    {
        delete[] COLUMNS;
    }
};

class DVector3Converter: public Converter<DVector3>
{
private:
    const std::string* const COLUMNS = new std::string[3] { ".X", ".Y", ".Z" };
public:
    int GetSize() { return 3; }
    int GetBytes() { return sizeof(double); }
    const std::string* GetColumns() { return COLUMNS; }

    ~DVector3Converter()
    {
        delete[] COLUMNS;
    }
};

class DAABox2DConverter: public Converter<DAABox2D>
{
private:
    const std::string* const COLUMNS = new std::string[4] { ".Min.X", ".Min.Y", ".Max.X", ".Max.Y" };
public:
    int GetSize() { return 4; }
    int GetBytes() { return sizeof(double); }
    const std::string* GetColumns() { return COLUMNS; }

    ~DAABox2DConverter()
    {
        delete[] COLUMNS;
    }
};

class DAABoxConverter: public Converter<DAABox>
{
private:
    const std::string* const COLUMNS = new std::string[6] { ".Min.X", ".Min.Y", ".Min.Z", ".Max.X", ".Max.Y", ".Max.Z" };
public:
    int GetSize() { return 6; }
    int GetBytes() { return sizeof(double); }
    const std::string* GetColumns() { return COLUMNS; }

    ~DAABoxConverter()
    {
        delete[] COLUMNS;
    }
};

class AABoxConverter: public Converter<AABox>
{
private:
    const std::string* const COLUMNS = new std::string[6] { ".Min.X", ".Min.Y", ".Min.Z", ".Max.X", ".Max.Y", ".Max.Z" };
public:
    int GetSize() { return 6; }
    int GetBytes() { return sizeof(float); }
    const std::string* GetColumns() { return COLUMNS; }

    ~AABoxConverter()
    {
        delete[] COLUMNS;
    }
};