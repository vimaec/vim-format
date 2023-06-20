#ifndef __VIM_TESTS_H__
#define __VIM_TESTS_H__

#include <string>

template<typename T>
void test(std::string message, T actual, T expected)
{
    if (actual != expected)
        std::cout << message << std::endl
                  << "Expected: " << expected << ". Actual: " << actual << std::endl;

    assert(actual == expected);
}

template<typename T>
void test_not_equal(std::string message, T actual, T expected)
{
    if (actual == expected)
        std::cout << message << std::endl
                  << "NOT expected: " << expected << ". Actual: " << actual << std::endl;

    assert(actual != expected);
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

// Function to print the
// index of an element
template<typename T>
int get_index(const std::vector<T>& v, const T item)
{
    auto it = std::find(v.begin(), v.end(), item);
    return it != v.end()
        ? it - v.begin()
        : -1;
}

constexpr char pathSeparator =
#if defined(__ANDROID__) || defined(__APPLE__) || defined(__linux__)
    '/';
#else
    '\\';
#endif

std::string normalize_path(const std::string& fileName)
{
    std::string ret = fileName;

#if defined(__ANDROID__) || defined(__APPLE__) || defined(__linux__)
    std::replace(ret.begin(), ret.end(), '\\', pathSeparator);
#else
    std::replace(ret.begin(), ret.end(), '/', pathSeparator);
#endif

    return ret;
}

#endif